using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using SME.SERAp.Prova.Item.Infra.Extensions;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static SME.SERAp.Prova.Item.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Item.Worker
{
    public class WorkerRabbit : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly RabbitOptions rabbitOptions;
        private readonly ConnectionFactory connectionFactory;
        private readonly ILogger<WorkerRabbit> logger;
        private readonly IServicoTelemetria servicoTelemetria;
        private readonly IServicoLog servicoLog;
        private readonly IServicoMensageria servicoMensageria;

        private readonly Dictionary<string, ComandoRabbit> comandos;

        public WorkerRabbit(
            IServiceScopeFactory serviceScopeFactory,
            RabbitOptions rabbitOptions,
            ConnectionFactory connectionFactory,
            ILogger<WorkerRabbit> logger,
            IServicoTelemetria servicoTelemetria,
            IServicoLog servicoLog,
            IServicoMensageria servicoMensageria)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            this.rabbitOptions = rabbitOptions ?? throw new ArgumentNullException(nameof(rabbitOptions));
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.servicoTelemetria = servicoTelemetria ?? throw new ArgumentNullException(nameof(servicoTelemetria));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            this.servicoMensageria = servicoMensageria ?? throw new ArgumentNullException(nameof(servicoMensageria));
            
            comandos = new Dictionary<string, ComandoRabbit>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var conexaoRabbit = connectionFactory.CreateConnection();
            using var channel = conexaoRabbit.CreateModel();

            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicQos(0, rabbitOptions.LimiteDeMensagensPorExecucao, false);

            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteItem, ExchangeType.Direct, true);
            channel.ExchangeDeclare(ExchangeRabbit.SerapEstudanteItemDeadLetter, ExchangeType.Direct, true);

            RegistrarUseCases();
            DeclararFilas(channel);

            await InicializaConsumer(channel, stoppingToken);
        }

        private void DeclararFilas(IModel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                var filaDeadLetter = $"{fila}.deadletter";
                var filaDeadLetterFinal = $"{fila}.deadletter.final";
                
                if (rabbitOptions.ForcarRecriarFilas)
                {
                    channel.QueueDelete(fila, ifEmpty: true);
                    channel.QueueDelete(filaDeadLetter, ifEmpty: true);
                    channel.QueueDelete(filaDeadLetterFinal, ifEmpty: true);
                }                
                
                var args = ObterArgumentoDaFila(fila);
                channel.QueueDeclare(fila, true, false, false, args);
                channel.QueueBind(fila, ExchangeRabbit.SerapEstudanteItem, fila, null);

                var argsDlq = ObterArgumentoDaFilaDeadLetter(fila);
                channel.QueueDeclare(filaDeadLetter, true, false, false, argsDlq);
                channel.QueueBind(filaDeadLetter, ExchangeRabbit.SerapEstudanteItemDeadLetter, fila, null);

                var argsFinal = new Dictionary<string, object> { { "x-queue-mode", "lazy" } };

                channel.QueueDeclare(
                    queue: filaDeadLetterFinal, 
                    durable: true, 
                    exclusive: false, 
                    autoDelete: false, 
                    arguments: argsFinal);

                channel.QueueBind(filaDeadLetterFinal, ExchangeRabbit.SerapEstudanteItemDeadLetter, filaDeadLetterFinal, null);
            }
        }
        
        private Dictionary<string, object> ObterArgumentoDaFila(string fila)
        {
            var args = new Dictionary<string, object>
                { { "x-dead-letter-exchange", ExchangeRabbit.SerapEstudanteItemDeadLetter } };

            if (comandos.ContainsKey(fila) && comandos[fila].ModeLazy)
                args.Add("x-queue-mode", "lazy");
            
            return args;
        }
        
        private Dictionary<string, object> ObterArgumentoDaFilaDeadLetter(string fila)
        {
            var argsDlq = new Dictionary<string, object>();
            var ttl = comandos.ContainsKey(fila) ? comandos[fila].Ttl : ExchangeRabbit.SerapDeadLetterTtl;

            argsDlq.Add("x-dead-letter-exchange", ExchangeRabbit.SerapEstudanteItem);
            argsDlq.Add("x-message-ttl", ttl);
            argsDlq.Add("x-queue-mode", "lazy");

            return argsDlq;
        }
        
        private ulong GetRetryCount(IBasicProperties properties)
        {
            if (properties.Headers == null || !properties.Headers.ContainsKey("x-death"))
                return 0;
            
            var deathProperties = (List<object>)properties.Headers["x-death"];
            var lastRetry = (Dictionary<string, object>)deathProperties[0];
            var count = lastRetry["count"];
            
            return (ulong) Convert.ToInt64(count);
        }        

        private void RegistrarUseCases()
        {
            comandos.Add(RotaRabbit.IniciarImportacoes, new ComandoRabbit("Iniciar os processos de importações", typeof(IIniciarImportacoesUseCase)));

            comandos.Add(RotaRabbit.AssuntoSync, new ComandoRabbit("Sync assuntos", typeof(IAssuntoSyncUseCase)));
            comandos.Add(RotaRabbit.AssuntoTratar, new ComandoRabbit("Tratar assunto", typeof(IAssuntoTratarUseCase)));
            comandos.Add(RotaRabbit.SubassuntoSync, new ComandoRabbit("Sync subassuntos", typeof(ISubassuntoSyncUseCase)));
            comandos.Add(RotaRabbit.SubassuntoTratar, new ComandoRabbit("Tratar subassunto", typeof(ISubassuntoTratarUseCase)));

            comandos.Add(RotaRabbit.QuantidadeAlternativaSync, new ComandoRabbit("Sync Quantidade alternativas", typeof(IQuantidadeAlternativaSyncUseCase)));
            comandos.Add(RotaRabbit.QuantidadeAlternativaTratar, new ComandoRabbit("Tratar Quantidade alternativas", typeof(IQuantidadeAlternativaTratarUseCase)));

            comandos.Add(RotaRabbit.AreaConhecimentoSync, new ComandoRabbit("Sync AreaConhecimento", typeof(IAreaConhecimentoSyncUsecase)));
            comandos.Add(RotaRabbit.AreaConhecimentoTratar, new ComandoRabbit("Tratar AreaConhecimento", typeof(IAreaConhecimentoUseCase)));

            comandos.Add(RotaRabbit.DisciplinaSync, new ComandoRabbit("Sync Disciplina", typeof(IDisciplinaSyncUseCase)));
            comandos.Add(RotaRabbit.DisciplinaTratar, new ComandoRabbit("Tratar Disciplina", typeof(IDisciplinaTratarUseCase)));

            comandos.Add(RotaRabbit.MatrizSync, new ComandoRabbit("Sync Matriz", typeof(ISyncMatrizUseCase)));
            comandos.Add(RotaRabbit.MatrizTratar, new ComandoRabbit("Tratar Matriz", typeof(IMatrizTratarUseCase)));

            comandos.Add(RotaRabbit.TipoGradeSync, new ComandoRabbit("Sync TipoGrade", typeof(ITipoGradeSyncUseCase)));
            comandos.Add(RotaRabbit.TipoGradeTratar, new ComandoRabbit("Tratar TipoGrade", typeof(ITipoGradeTratarUseCase)));
            comandos.Add(RotaRabbit.CompetenciaSync, new ComandoRabbit("Sync Competencia", typeof(ICompetenciaSyncUseCase)));
            comandos.Add(RotaRabbit.CompetenciaTratar, new ComandoRabbit("Tratar Competencia", typeof(ICompetenciaTratarUseCase)));

            comandos.Add(RotaRabbit.HabilidadeSync, new ComandoRabbit("Sync Habilidade", typeof(IHabilidadeSyncUseCase)));
            comandos.Add(RotaRabbit.HabilidadeTratar, new ComandoRabbit("Tratar Habilidade", typeof(IHabilidadeTratarUseCase)));
            
            //-> Permissão
            comandos.Add(RotaRabbit.GrupoSync, new ComandoRabbit("Sync Grupo", typeof(IGrupoSyncUseCase)));
            comandos.Add(RotaRabbit.GrupoTratar, new ComandoRabbit("Tratar Grupo", typeof(IGrupoTratarUseCase)));
            comandos.Add(RotaRabbit.UsuarioSync, new ComandoRabbit("Sync Usuario", typeof(IUsuarioSyncUseCase)));
            comandos.Add(RotaRabbit.UsuarioTratar, new ComandoRabbit("Tratar Usuario", typeof(IUsuarioTratarUseCase)));
            comandos.Add(RotaRabbit.UsuarioGrupoInserir, new ComandoRabbit("Inserir Usuario Grupo", typeof(IUsuarioGrupoInserirUseCase)));
            comandos.Add(RotaRabbit.UsuarioGrupoInativar, new ComandoRabbit("Inativar Usuario Grupo", typeof(IUsuarioGrupoInativarUseCase)));
        }

        private async Task InicializaConsumer(IModel channel, CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    await TratarMensagem(ea, channel);
                }
                catch (Exception ex)
                {
                    servicoLog.Registrar($"Erro ao tratar mensagem {ea.DeliveryTag}", ex);
                    channel.BasicReject(ea.DeliveryTag, false);
                }
            };

            RegistrarConsumer(consumer, channel);

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker ativo em: {Now}", DateTime.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task TratarMensagem(BasicDeliverEventArgs ea, IModel channel)
        {
            var mensagem = Encoding.UTF8.GetString(ea.Body.Span);
            var rota = ea.RoutingKey;

            if (comandos.ContainsKey(rota))
            {
                var transacao = servicoTelemetria.IniciarTransacao(rota);

                var mensagemRabbit = mensagem.ConverterObjectStringPraObjeto<MensagemRabbit>();
                var comandoRabbit = comandos[rota];

                try
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var casoDeUso = scope.ServiceProvider.GetService(comandoRabbit.TipoCasoUso);

                    if (casoDeUso == null) throw new ArgumentNullException(comandoRabbit.TipoCasoUso.Name);

                    await servicoTelemetria.RegistrarAsync(() =>
                        comandoRabbit.TipoCasoUso.ObterMetodo("Executar").InvokeAsync(casoDeUso, mensagemRabbit),
                        "RabbitMQ",
                        rota,
                        rota);

                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (NegocioException nex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, nex, LogNivel.Negocio, $"Erros: {nex.Message}");
                    servicoTelemetria.RegistrarExcecao(transacao, nex);
                }
                catch (ValidacaoException vex)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, vex, LogNivel.Negocio, $"Erros: {JsonSerializer.Serialize(vex.Mensagens())}");
                    servicoTelemetria.RegistrarExcecao(transacao, vex);
                }
                catch (Exception ex)
                {
                    servicoTelemetria.RegistrarExcecao(transacao, ex);
                    
                    var rejeicoes = GetRetryCount(ea.BasicProperties);

                    if (++rejeicoes >= comandoRabbit.QuantidadeReprocessamentoDeadLetter)
                    {
                        channel.BasicAck(ea.DeliveryTag, false);
                        
                        var filaFinal = $"{ea.RoutingKey}.deadletter.final";

                        await servicoMensageria.Publicar(mensagemRabbit, filaFinal,
                            ExchangeRabbit.SerapEstudanteItemDeadLetter,
                            "PublicarDeadLetter");
                    } else
                        channel.BasicReject(ea.DeliveryTag, false);
                    
                    RegistrarLog(ea, mensagemRabbit, ex, LogNivel.Critico, $"Erros: {ex.Message}");
                }
                finally
                {
                    servicoTelemetria.FinalizarTransacao(transacao);
                }
            }
            else
                channel.BasicReject(ea.DeliveryTag, false);
        }

        private static void RegistrarConsumer(EventingBasicConsumer consumer, IModel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
                channel.BasicConsume(fila, false, consumer);
        }

        private void RegistrarLog(BasicDeliverEventArgs ea, MensagemRabbit mensagemRabbit, Exception ex, LogNivel logNivel, string observacao)
        {
            var mensagem = $"Worker Serap: Rota -> {ea.RoutingKey}  Cod Correl -> {mensagemRabbit.CodigoCorrelacao.ToString()[..3]}";
            servicoLog.Registrar(new LogMensagem(mensagem, logNivel, observacao, ex?.StackTrace, ex.InnerException?.Message));
        }
    }
}
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
            var factory = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            await using var conexaoRabbit = await factory.CreateConnectionAsync();
            await using var channel = await conexaoRabbit.CreateChannelAsync();

            var props = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicQosAsync(0, rabbitOptions.LimiteDeMensagensPorExecucao, false);

            await channel.ExchangeDeclareAsync(ExchangeRabbit.SerapEstudanteItem, ExchangeType.Direct, true);
            await channel.ExchangeDeclareAsync(ExchangeRabbit.SerapEstudanteItemDeadLetter, ExchangeType.Direct, true);

            RegistrarUseCases();
            await DeclararFilasAsync(channel);

            await InicializaConsumerAsync(channel, stoppingToken);
        }

        private async Task DeclararFilasAsync(IChannel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                var filaDeadLetter = $"{fila}.deadletter";
                var filaDeadLetterFinal = $"{fila}.deadletter.final";

                if (rabbitOptions.ForcarRecriarFilas)
                {
                    await channel.QueueDeleteAsync(fila, ifEmpty: true);
                    await channel.QueueDeleteAsync(filaDeadLetter, ifEmpty: true);
                    await channel.QueueDeleteAsync(filaDeadLetterFinal, ifEmpty: true);
                }

                var args = ObterArgumentoDaFila(fila);
                await channel.QueueDeclareAsync(fila, true, false, false, args);
                await channel.QueueBindAsync(fila, ExchangeRabbit.SerapEstudanteItem, fila, null);

                var argsDlq = ObterArgumentoDaFilaDeadLetter(fila);
                await channel.QueueDeclareAsync(filaDeadLetter, true, false, false, argsDlq);
                await channel.QueueBindAsync(filaDeadLetter, ExchangeRabbit.SerapEstudanteItemDeadLetter, fila, null);

                var argsFinal = new Dictionary<string, object> { { "x-queue-mode", "lazy" } };

                await channel.QueueDeclareAsync(
                    queue: filaDeadLetterFinal,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: argsFinal);

                await channel.QueueBindAsync(filaDeadLetterFinal, ExchangeRabbit.SerapEstudanteItemDeadLetter, filaDeadLetterFinal, null);
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

        private ulong GetRetryCount(IReadOnlyBasicProperties properties)
        {
            if (properties.Headers == null || !properties.Headers.ContainsKey("x-death"))
                return 0;

            var deathProperties = (List<object>)properties.Headers["x-death"];
            if (deathProperties.Count == 0)
                return 0;

            var lastRetry = (Dictionary<string, object>)deathProperties[0];

            if (!lastRetry.ContainsKey("count"))
                return 0;

            var count = lastRetry["count"];

            return (ulong)Convert.ToInt64(count);
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

            comandos.Add(RotaRabbit.DificuldadeSync, new ComandoRabbit("Sync Dificuldade", typeof(IDificuldadeSyncUseCase)));
            comandos.Add(RotaRabbit.DificuldadeTratar, new ComandoRabbit("Tratar Dificuldade", typeof(IDificuldadeTratarUseCase)));
        }

        private async Task InicializaConsumerAsync(IChannel channel, CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    await TratarMensagem(ea, channel);
                }
                catch (Exception ex)
                {
                    servicoLog.Registrar($"Erro ao tratar mensagem {ea.DeliveryTag}", ex);
                    await channel.BasicRejectAsync(ea.DeliveryTag, false);
                }
            };

            await RegistrarConsumerAsync(consumer, channel);

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Worker ativo em: {Now}", DateTime.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task TratarMensagem(BasicDeliverEventArgs ea, IChannel channel)
        {
            var mensagem = Encoding.UTF8.GetString(ea.Body.ToArray());
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

                    if (casoDeUso == null)
                        throw new ArgumentNullException(comandoRabbit.TipoCasoUso.Name);

                    await servicoTelemetria.RegistrarAsync(() =>
                        comandoRabbit.TipoCasoUso.ObterMetodo("Executar").InvokeAsync(casoDeUso, mensagemRabbit),
                        "RabbitMQ",
                        rota,
                        rota);

                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (NegocioException nex)
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, nex, LogNivel.Negocio, $"Erros: {nex.Message}");
                    servicoTelemetria.RegistrarExcecao(transacao, nex);
                }
                catch (ValidacaoException vex)
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    RegistrarLog(ea, mensagemRabbit, vex, LogNivel.Negocio, $"Erros: {JsonSerializer.Serialize(vex.Mensagens())}");
                    servicoTelemetria.RegistrarExcecao(transacao, vex);
                }
                catch (Exception ex)
                {
                    servicoTelemetria.RegistrarExcecao(transacao, ex);

                    var rejeicoes = GetRetryCount(ea.BasicProperties);

                    if (++rejeicoes >= comandoRabbit.QuantidadeReprocessamentoDeadLetter)
                    {
                        await channel.BasicAckAsync(ea.DeliveryTag, false);

                        var filaFinal = $"{ea.RoutingKey}.deadletter.final";

                        await servicoMensageria.Publicar(mensagemRabbit, filaFinal,
                            ExchangeRabbit.SerapEstudanteItemDeadLetter,
                            "PublicarDeadLetter");
                    }
                    else
                    {
                        await channel.BasicRejectAsync(ea.DeliveryTag, false);
                    }

                    RegistrarLog(ea, mensagemRabbit, ex, LogNivel.Critico, $"Erros: {ex.Message}");
                }
                finally
                {
                    servicoTelemetria.FinalizarTransacao(transacao);
                }
            }
            else
            {
                await channel.BasicRejectAsync(ea.DeliveryTag, false);
            }
        }

        private static async Task RegistrarConsumerAsync(AsyncEventingBasicConsumer consumer, IChannel channel)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                // Usando BasicConsumeAsync e aguardando a execução da tarefa
                await channel.BasicConsumeAsync(fila, false, consumer);
            }
        }

        private void RegistrarLog(BasicDeliverEventArgs ea, MensagemRabbit mensagemRabbit, Exception ex, LogNivel logNivel, string observacao)
        {
            var mensagem = $"Worker Serap: Rota -> {ea.RoutingKey}  Cod Correl -> {mensagemRabbit.CodigoCorrelacao.ToString()[..3]}";

            // Cria a LogMensagem, mas não a passa diretamente para o servicoLog.Registrar
            var logMensagem = new LogMensagem(mensagem, logNivel, observacao, ex?.StackTrace, ex?.InnerException?.Message);

            // Cria uma exceção que vai ser passada para o serviço de log (se o servicoLog requer uma Exception)
            var exceptionToLog = new Exception(logMensagem.Mensagem, ex);

            servicoLog.Registrar(exceptionToLog);  // Registra a exceção com os dados das LogMensagens
        }
    }
}
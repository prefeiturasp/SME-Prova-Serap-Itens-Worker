using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class IniciarImportacoesUseCase : AbstractUseCase, IIniciarImportacoesUseCase
    {

        private readonly IServicoLog servicoLog;

        public IniciarImportacoesUseCase(IMediator mediator, IServicoLog servicoLog) : base(mediator)
        {
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            try
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AreaConhecimentoSync, ""));                
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.QuantidadeAlternativaSync, ""));

                return true;
            }
            catch (Exception ex)
            {
                const string msg = "Erro ao iniciar os processos de importações Worker Serap Itens.";
                servicoLog.Registrar(msg, ex);
                return false;
            }
        }
    }
}

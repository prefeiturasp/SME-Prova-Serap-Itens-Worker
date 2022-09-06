using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Extensions;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class TratarDeadletterSyncUseCase : ITratarDeadletterSyncUseCase
    {
        private readonly IMediator mediator;

        public TratarDeadletterSyncUseCase(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            await EnviarParaFilaSync(mensagemRabbit);
            return await Task.FromResult(true);
        }

        private async Task EnviarParaFilaSync(MensagemRabbit mensagem)
        {
            foreach (var fila in typeof(RotaRabbit).ObterConstantesPublicas<string>())
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DeadLetterTratar, fila));
            }
        }
    }

}

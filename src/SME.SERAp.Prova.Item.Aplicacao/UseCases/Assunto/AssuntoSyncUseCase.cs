using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AssuntoSyncUseCase : AbstractUseCase, IAssuntoSyncUseCase
    {
        public AssuntoSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var assuntosApi = await mediator.Send(new ObterAssuntosApiSerapQuery());
            if (assuntosApi == null || !assuntosApi.Any()) return false;

            foreach (var assunto in assuntosApi)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AssuntoSync, assunto));
            }
            return true;
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SubassuntoTratarUseCase : AbstractUseCase, ISubassuntoTratarUseCase
    {
        public SubassuntoTratarUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var subassunto = mensagemRabbit.ObterObjetoMensagem<SubassuntoDto>();

            if (subassunto == null) return false;
            if (!subassunto.Validacao()) return false;

            var subassuntoAtual = await mediator.Send(new ObterSubassuntoPorLegadoIdQuery(subassunto.Id));

            if (subassuntoAtual == null)
                return await Inserir(subassunto);

            return await Alterar(subassuntoAtual, subassunto);
        }

        private async Task<bool> Inserir(SubassuntoDto subassuntoApi)
        {
            var subassuntoInserir = new Subassunto(null, subassuntoApi.Id, subassuntoApi.AssuntoId, subassuntoApi.Descricao, StatusGeral.Ativo);
            await mediator.Send(new InserirSubassuntoCommand(subassuntoInserir));
            return true;
        }

        private async Task<bool> Alterar(Subassunto subassunto, SubassuntoDto subassuntoApi)
        {
            var subassuntoAlterar = new Subassunto(subassunto.Id, subassuntoApi.Id, subassunto.AssuntoId, subassuntoApi.Descricao, subassuntoApi.Status);
            subassuntoAlterar.CriadoEm = subassunto.CriadoEm;
            await mediator.Send(new AlterarSubassuntoCommand(subassuntoAlterar));
            return true;
        }
    }
}

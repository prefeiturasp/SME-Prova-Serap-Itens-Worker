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

            if (subassunto == null)
                return false;

            if (!subassunto.Validacao())
                return false;

            var subassuntoBase = await mediator.Send(new ObterSubassuntoPorLegadoIdQuery(subassunto.Id));

            if (subassuntoBase == null)
                return await Inserir(subassunto);

            return await Alterar(subassuntoBase, subassunto);
        }

        private async Task<bool> Inserir(SubassuntoDto subassunto)
        {
            var subassuntoInserir = new Subassunto(null, subassunto.Id, subassunto.AssuntoId, subassunto.Descricao,
                StatusGeral.Ativo);

            await mediator.Send(new InserirSubassuntoCommand(subassuntoInserir));

            return true;
        }

        private async Task<bool> Alterar(Subassunto subassuntoBase, SubassuntoDto subassunto)
        {
            if (!subassuntoBase.PossuiAlteracao(subassunto.AssuntoId, subassunto.Descricao, subassunto.Status))
                return true;

            var subassuntoAlterar = new Subassunto(subassuntoBase.Id, subassunto.Id, subassunto.AssuntoId,
                subassunto.Descricao, subassunto.Status)
            {
                CriadoEm = subassuntoBase.CriadoEm
            };

            await mediator.Send(new AlterarSubassuntoCommand(subassuntoAlterar));

            return true;
        }
    }
}

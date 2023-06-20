using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntoPorLegadoIdQuery : IRequest<Subassunto>
    {
        public ObterSubassuntoPorLegadoIdQuery(long legadoId)
        {
            LegadoId = legadoId;
        }

        public long LegadoId { get; }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaPorLegadoIdQuery : IRequest<AreaConhecimento>
    {
        public ObterAreaPorLegadoIdQuery(long legadoId)
        {
            LegadoId = legadoId;
        }

        public long LegadoId { get; set; }
    }
}
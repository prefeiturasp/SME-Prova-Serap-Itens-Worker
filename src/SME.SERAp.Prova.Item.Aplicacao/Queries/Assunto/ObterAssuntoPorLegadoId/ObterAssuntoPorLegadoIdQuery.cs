using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntoPorLegadoIdQuery : IRequest<Assunto>
    {
        public ObterAssuntoPorLegadoIdQuery(long legadoId)
        {
            LegadoId = legadoId;
        }

        public long LegadoId { get; set; }
    }
}

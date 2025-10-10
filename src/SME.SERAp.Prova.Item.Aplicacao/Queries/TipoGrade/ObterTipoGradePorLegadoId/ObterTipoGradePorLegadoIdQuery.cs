using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorLegadoIdQuery : IRequest<TipoGrade>
    {
        public ObterTipoGradePorLegadoIdQuery(long legadoId, long matrizId)
        {
            LegadoId = legadoId;
            MatrizId = matrizId;
        }

        public long LegadoId { get; set; }

        public long MatrizId { get; set; }
    }
}

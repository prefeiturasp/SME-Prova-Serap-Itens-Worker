using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadePorLegadoIdQuery : IRequest<Dominio.Entities.Habilidade>
    {
        public ObterHabilidadePorLegadoIdQuery(long legadoId)
        {
            LegadoId = legadoId;
        }

        public long LegadoId { get; set; }
    }
}

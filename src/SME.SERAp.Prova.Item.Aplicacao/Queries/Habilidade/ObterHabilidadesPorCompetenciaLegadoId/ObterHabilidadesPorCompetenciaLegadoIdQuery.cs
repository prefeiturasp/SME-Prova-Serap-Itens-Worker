using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaLegadoIdQuery : IRequest<IEnumerable<Dominio.Entities.Habilidade>>
    {
        public ObterHabilidadesPorCompetenciaLegadoIdQuery(long competenciaLegadoId)
        {
            CompetenciaLegadoId = competenciaLegadoId;
        }

        public long CompetenciaLegadoId { get; set; }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaLegadoIdQuery : IRequest<IEnumerable<Habilidade>>
    {
        public ObterHabilidadesPorCompetenciaLegadoIdQuery(long competenciaLegadoId)
        {
            CompetenciaLegadoId = competenciaLegadoId;
        }

        public long CompetenciaLegadoId { get; set; }
    }
}

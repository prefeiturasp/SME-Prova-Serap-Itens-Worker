using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadeApiSerapQuery : IRequest<IEnumerable<HabilidadeDto>>
    {
        public ObterHabilidadeApiSerapQuery(long competenciaId)
        {
            CompetenciaId = competenciaId;
        }

        public long CompetenciaId { get; set; }
    }
}

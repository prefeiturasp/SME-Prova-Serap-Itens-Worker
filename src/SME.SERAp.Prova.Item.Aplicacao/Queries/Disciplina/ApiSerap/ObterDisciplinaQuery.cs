using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Disciplina.ApiSerap
{
    public class ObterDisciplinaQuery : IRequest<IEnumerable<DisciplinaDto>>
    {
        public long AreaConhecimentoId { get; set; }
    }
}

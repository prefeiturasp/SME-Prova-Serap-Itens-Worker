using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Matriz.ApiSerap
{
    public class ObterMatrizQuery : IRequest<IEnumerable<MatrizDto>>
    {
        public long DisciplinaId { get; set; }
    }
}
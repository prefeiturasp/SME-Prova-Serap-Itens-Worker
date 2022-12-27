using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Matriz.ApiSerap
{
    public class ObterMatrizQuery : IRequest<IEnumerable<MatrizDto>>
    {
        public long DisciplinaId { get; set; }
    }
}
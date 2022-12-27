using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{

    public class ObterMatrizPorDisciplinaIdQuery : IRequest<IEnumerable<MatrizDto>>
    {

        public ObterMatrizPorDisciplinaIdQuery(long disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }

        public long DisciplinaId { get; set; }
    }
}

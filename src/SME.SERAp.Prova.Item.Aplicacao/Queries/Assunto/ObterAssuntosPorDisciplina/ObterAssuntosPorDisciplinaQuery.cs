using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosPorDisciplinaQuery : IRequest<IEnumerable<Assunto>>
    {
        public ObterAssuntosPorDisciplinaQuery(long disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }

        public long DisciplinaId { get; set; }

    }
}

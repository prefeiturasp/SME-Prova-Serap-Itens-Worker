using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntosPorAssuntoIdQuery : IRequest<IEnumerable<Subassunto>>
    {
        public ObterSubassuntosPorAssuntoIdQuery(long assuntoId)
        {
            AssuntoId = assuntoId;
        }

        public long AssuntoId { get; set; }

    }
}

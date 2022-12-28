using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubassuntosApiSerapQuery : IRequest<List<SubassuntoDto>>
    {
        public ObterSubassuntosApiSerapQuery(long assuntoLegadoId)
        {
            AssuntoLegadoId = assuntoLegadoId;
        }

        public long AssuntoLegadoId { get; set; }

    }
}

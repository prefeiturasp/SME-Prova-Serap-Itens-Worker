using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosApiSerapQuery : IRequest<List<AssuntoDto>>
    {
        public ObterAssuntosApiSerapQuery()
        {

        }
    }
}

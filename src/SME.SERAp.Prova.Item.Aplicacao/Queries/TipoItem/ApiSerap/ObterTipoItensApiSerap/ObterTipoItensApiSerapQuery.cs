using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoItensApiSerapQuery : IRequest<List<QuantidadeAlternativaDto>>
    {
        public ObterTipoItensApiSerapQuery()
        {

        }
    }
}

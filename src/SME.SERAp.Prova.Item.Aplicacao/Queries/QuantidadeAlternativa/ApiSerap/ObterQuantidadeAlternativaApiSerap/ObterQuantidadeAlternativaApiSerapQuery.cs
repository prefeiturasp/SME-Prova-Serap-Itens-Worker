using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQuantidadeAlternativaApiSerapQuery : IRequest<List<QuantidadeAlternativaDto>>
    {
        public ObterQuantidadeAlternativaApiSerapQuery()
        {

        }
    }
}

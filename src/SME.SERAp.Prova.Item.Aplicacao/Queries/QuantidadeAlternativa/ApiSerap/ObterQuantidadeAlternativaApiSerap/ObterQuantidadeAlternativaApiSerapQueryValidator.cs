using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQuantidadeAlternativaApiSerapQueryValidator : AbstractValidator<ObterQuantidadeAlternativaApiSerapQuery>
    {
        public ObterQuantidadeAlternativaApiSerapQueryValidator()
        {

        }
    }
}
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQuantidadeAlternativaPorLegadoIdQueryValidator : AbstractValidator<ObterQuantidadeAlternativaPorLegadoIdQuery>
    {
        public ObterQuantidadeAlternativaPorLegadoIdQueryValidator()
        {
            RuleFor(c => c.LegadoId)
           .NotEmpty()
           .WithMessage("O Id legado precisa ser informado.");
        }
    }
}

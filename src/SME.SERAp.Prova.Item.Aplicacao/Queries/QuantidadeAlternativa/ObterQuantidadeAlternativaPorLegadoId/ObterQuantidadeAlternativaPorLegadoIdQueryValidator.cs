using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQuantidadeAlternativaPorLegadoIdQueryValidator : AbstractValidator<ObterQuantidadeAlternativaPorLegadoIdQuery>
    {
        public ObterQuantidadeAlternativaPorLegadoIdQueryValidator()
        {
            RuleFor(c => c.LegadoId)
           .GreaterThan(0)
           .WithMessage("O Id legado precisa ser informado.");
        }
    }
}

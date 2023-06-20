using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinaPorLegadoIdQueryValidator : AbstractValidator<ObterDisciplinaPorLegadoIdQuery>
    {
        public ObterDisciplinaPorLegadoIdQueryValidator()
        {
            RuleFor(c => c.LegadoId)
                .GreaterThan(0)
                .WithMessage("LegadoId deve ser informado.");
        }
    }
}

using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntoPorLegadoIdQueryValidator : AbstractValidator<ObterAssuntoPorLegadoIdQuery>
    {
        public ObterAssuntoPorLegadoIdQueryValidator()
        {
            RuleFor(c => c.LegadoId)
                .GreaterThan(0)
                .WithMessage("LegadoId é obrigatório");
        }
    }
}

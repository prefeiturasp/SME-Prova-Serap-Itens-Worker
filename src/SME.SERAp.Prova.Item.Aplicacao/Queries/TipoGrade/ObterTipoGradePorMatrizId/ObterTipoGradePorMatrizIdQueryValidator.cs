using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoGradePorMatrizIdQueryValidator : AbstractValidator<ObterTipoGradePorMatrizIdQuery>
    {
        public ObterTipoGradePorMatrizIdQueryValidator()
        {
            RuleFor(c => c.MatrizId)
                .GreaterThan(0)
                .WithMessage("A matriz deve ser informada para obter os tipos de grades.");
        }
    }
}
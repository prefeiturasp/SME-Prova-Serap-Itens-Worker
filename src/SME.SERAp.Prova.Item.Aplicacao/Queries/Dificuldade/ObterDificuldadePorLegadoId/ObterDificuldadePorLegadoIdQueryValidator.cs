using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDificuldadePorLegadoIdQueryValidator : AbstractValidator<ObterDificuldadePorLegadoIdQuery>
    {
        public ObterDificuldadePorLegadoIdQueryValidator()
        {
            RuleFor(x => x.LegadoId)
                .NotEmpty()
                .WithMessage("Código legado da dificuldade deve ser informados");
        }
    }
}

using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosPorDisciplinaQueryValidator : AbstractValidator<ObterAssuntosPorDisciplinaQuery>
    {
        public ObterAssuntosPorDisciplinaQueryValidator()
        {
            RuleFor(c => c.DisciplinaId)
                .GreaterThan(0)
                .WithMessage("DisciplinaId é obrigatório");
        }
    }
}

using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosApiSerapQueryValidator : AbstractValidator<ObterAssuntosApiSerapQuery>
    {
        public ObterAssuntosApiSerapQueryValidator()
        {
            RuleFor(c => c.DisciplinaLegadoId)
                .GreaterThan(0)
                .WithMessage("A disciplina deve ser informada para obter os assuntos.");
        }
    }
}

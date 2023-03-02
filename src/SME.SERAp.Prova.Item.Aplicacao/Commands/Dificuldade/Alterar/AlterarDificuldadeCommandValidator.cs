using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarDificuldadeCommandValidator : AbstractValidator<AlterarDificuldadeCommand>
    {
        public AlterarDificuldadeCommandValidator()
        {
            RuleFor(x => x.Dificuldade)
                .NotEmpty()
                .WithMessage("Dificuldade a ser alterada deve ser informada");

            RuleFor(x => x.Dificuldade.Id)
                .NotEmpty()
                .WithMessage("Id da dificuldade deve ser informado para ser alterada");

            RuleFor(x => x.Dificuldade.Descricao)
                .NotEmpty()
                .WithMessage("Descrição da dificuldade deve ser informado");

            RuleFor(x => x.Dificuldade.Ordem)
                .NotEmpty()
                .WithMessage("Ordem da dificuldade deve ser informado");
        }
    }
}

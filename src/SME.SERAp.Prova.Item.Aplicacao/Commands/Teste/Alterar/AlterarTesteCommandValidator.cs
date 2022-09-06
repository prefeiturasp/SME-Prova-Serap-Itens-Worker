using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Teste.Inserir
{
    public class AlterarTesteCommandValidator : AbstractValidator<AlterarTesteCommand>
    {
        public AlterarTesteCommandValidator()
        {
            RuleFor(c => c.TesteDto.Id)
                .NotEqual(0)
                .WithMessage("O Id precisa ser informado");

            RuleFor(c => c.TesteDto.Descricao)
              .NotEmpty()
              .WithMessage("A Descrição precisa ser informado");
        }
    }
}

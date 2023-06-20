using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarQuantidadeAlternativaCommandValidator : AbstractValidator<AlterarQuantidadeAlternativaCommand>
    {
        public AlterarQuantidadeAlternativaCommandValidator()
        {
            RuleFor(c => c.QuantidadeAlternativa.Id)
                .GreaterThan(0)
                .WithMessage("O Id precisa ser informado");

            RuleFor(c => c.QuantidadeAlternativa.Descricao)
              .NotEmpty()
              .WithMessage("A Descrição precisa ser informada");

            RuleFor(c => c.QuantidadeAlternativa.LegadoId)
             .GreaterThan(0)
             .WithMessage("O Id legado precisa ser informado");

            RuleFor(c => c.QuantidadeAlternativa.QtdeAlternativa)
            .GreaterThan(0)
            .WithMessage("A quantidade de alternativas precisa ser informada e não pode ser zero");
        }
    }
}

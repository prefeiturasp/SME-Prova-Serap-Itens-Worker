using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirQuantidadeAlternativaCommandValidator : AbstractValidator<InserirQuantidadeAlternativaCommand>
    {
        public InserirQuantidadeAlternativaCommandValidator()
        {
               RuleFor(c => c.QuantidadeAlternativa.Descricao)
              .NotEmpty()
              .WithMessage("A Descrição precisa ser informada");

               RuleFor(c => c.QuantidadeAlternativa.LegadoId)
              .NotEqual(0)
              .WithMessage(" O Id legado precisa ser informado");

               RuleFor(c => c.QuantidadeAlternativa.QtdeAlternativa)
              .NotEqual(0)
              .WithMessage("A quantidade de alternativas precisa ser informada e não pode ser zero");
        }
    }
}


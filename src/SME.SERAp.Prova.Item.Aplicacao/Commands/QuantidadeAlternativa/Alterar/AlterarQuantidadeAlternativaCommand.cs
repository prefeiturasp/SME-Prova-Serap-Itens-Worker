using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarQuantidadeAlternativaCommand : IRequest<bool>
    {
        public AlterarQuantidadeAlternativaCommand(QuantidadeAlternativa quantidadeAlternativa)
        {
            QuantidadeAlternativa = quantidadeAlternativa;
        }

        public QuantidadeAlternativa QuantidadeAlternativa { get; set; }
    }
}

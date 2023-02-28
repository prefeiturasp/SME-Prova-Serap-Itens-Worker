using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirQuantidadeAlternativaCommand : IRequest<long>
    {
        public InserirQuantidadeAlternativaCommand(QuantidadeAlternativa quantidadeAlternativa)
        {
            QuantidadeAlternativa = quantidadeAlternativa;
        }

        public QuantidadeAlternativa QuantidadeAlternativa { get; set; }
    }
}

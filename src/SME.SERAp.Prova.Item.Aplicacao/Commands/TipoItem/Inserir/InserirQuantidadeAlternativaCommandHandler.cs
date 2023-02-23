using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirQuantidadeAlternativaCommandHandler : IRequestHandler<InserirQuantidadeAlternativaCommand, long>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa;

        public InserirQuantidadeAlternativaCommandHandler(IRepositorioQuantidadeAlternativa repositorioQuantidadeAlternativa)
        {
            this.repositorioQuantidadeAlternativa = repositorioQuantidadeAlternativa ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativa));
        }

        public async Task<long> Handle(InserirQuantidadeAlternativaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativa.IncluirAsync(request.QuantidadeAlternativa);
        }
    }
}

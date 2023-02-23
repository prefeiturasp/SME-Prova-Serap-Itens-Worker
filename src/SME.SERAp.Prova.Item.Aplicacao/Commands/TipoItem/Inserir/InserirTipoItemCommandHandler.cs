using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTipoItemCommandHandler : IRequestHandler<InserirQuantidadeAlternativaCommand, long>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioTipoItem;

        public InserirTipoItemCommandHandler(IRepositorioQuantidadeAlternativa repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<long> Handle(InserirQuantidadeAlternativaCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.IncluirAsync(request.QuantidadeAlternativa);
        }
    }
}

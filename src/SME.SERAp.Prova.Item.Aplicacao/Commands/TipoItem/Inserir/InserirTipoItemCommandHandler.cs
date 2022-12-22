using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirTipoItemCommandHandler : IRequestHandler<InserirTipoItemCommand, long>
    {

        private readonly IRepositorioTipoItem repositorioTipoItem;

        public InserirTipoItemCommandHandler(IRepositorioTipoItem repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<long> Handle(InserirTipoItemCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.IncluirAsync(request.TipoItem);
        }
    }
}

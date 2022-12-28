using MediatR;
using SME.SERAp.Prova.Item.Dados;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarTipoItemCommandHandler : IRequestHandler<AlterarTipoItemCommand, bool>
    {
        private readonly IRepositorioTipoItem repositorioTipoItem;

        public AlterarTipoItemCommandHandler(IRepositorioTipoItem repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<bool> Handle(AlterarTipoItemCommand request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.UpdateAsync(request.TipoItem) > 0;
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoItemPorLegadoIdQueryHandler : IRequestHandler<ObterTipoItemPorLegadoIdQuery, TipoItem>
    {

        private readonly IRepositorioTipoItem repositorioTipoItem;

        public ObterTipoItemPorLegadoIdQueryHandler(IRepositorioTipoItem repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<TipoItem> Handle(ObterTipoItemPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}

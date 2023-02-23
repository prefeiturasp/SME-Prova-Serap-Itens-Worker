using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTipoItemPorLegadoIdQueryHandler : IRequestHandler<ObterTipoItemPorLegadoIdQuery, QuantidadeAlternativa>
    {

        private readonly IRepositorioQuantidadeAlternativa repositorioTipoItem;

        public ObterTipoItemPorLegadoIdQueryHandler(IRepositorioQuantidadeAlternativa repositorioTipoItem)
        {
            this.repositorioTipoItem = repositorioTipoItem ?? throw new ArgumentNullException(nameof(repositorioTipoItem));
        }

        public async Task<QuantidadeAlternativa> Handle(ObterTipoItemPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoItem.ObterPorLegadoIdAsync(request.LegadoId);
        }
    }
}

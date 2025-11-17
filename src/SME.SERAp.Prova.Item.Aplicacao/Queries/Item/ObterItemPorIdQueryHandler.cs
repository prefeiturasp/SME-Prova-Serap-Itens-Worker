using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item
{
    public class ObterItemPorIdQueryHandler : IRequestHandler<ObterItemPorIdQuery, Dominio.Entities.Item>
    {
        private readonly IRepositorioItem repositorioItem;
        public ObterItemPorIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<Dominio.Entities.Item> Handle(ObterItemPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterPorId(request.ItemId);
        }
    }
}
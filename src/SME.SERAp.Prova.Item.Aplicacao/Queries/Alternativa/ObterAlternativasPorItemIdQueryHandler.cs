using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Alternativa
{
    public class ObterAlternativasPorItemIdQueryHandler : IRequestHandler<ObterAlternativasPorItemIdQuery, IEnumerable<Dominio.Entities.Alternativa>>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterAlternativasPorItemIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<IEnumerable<Dominio.Entities.Alternativa>> Handle(ObterAlternativasPorItemIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterAlternativasPorItemId(request.ItemId);
        }
    }
}
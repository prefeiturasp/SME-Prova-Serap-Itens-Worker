using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.ObterVideosItem
{
    public class ObterVideosItemQueryHandler : IRequestHandler<ObterVideosItemQuery, IEnumerable<ItemArquivoDto>>
    {
        private readonly IRepositorioArquivo repositorioArquivo;
        public ObterVideosItemQueryHandler(IRepositorioArquivo repositorioArquivo) => this.repositorioArquivo = repositorioArquivo;

        public async Task<IEnumerable<ItemArquivoDto>> Handle(ObterVideosItemQuery request, CancellationToken cancellationToken)
        {
            return await repositorioArquivo.ObterVideosPorItemIdAsync(request.ItemId);
        }
    }
}
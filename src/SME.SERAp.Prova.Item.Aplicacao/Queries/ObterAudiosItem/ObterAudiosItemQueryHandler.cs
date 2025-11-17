using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.ObterAudiosItem
{
    public class ObterAudiosItemQueryHandler : IRequestHandler<ObterAudiosItemQuery, IEnumerable<ItemArquivoDto>>
    {
        private readonly IRepositorioArquivo repositorioArquivo;
        public ObterAudiosItemQueryHandler(IRepositorioArquivo repositorioArquivo) => this.repositorioArquivo = repositorioArquivo;

        public async Task<IEnumerable<ItemArquivoDto>> Handle(ObterAudiosItemQuery request, CancellationToken cancellationToken)
        {
            return await repositorioArquivo.ObterAudiosPorItemIdAsync(request.ItemId);
        }
    }
}
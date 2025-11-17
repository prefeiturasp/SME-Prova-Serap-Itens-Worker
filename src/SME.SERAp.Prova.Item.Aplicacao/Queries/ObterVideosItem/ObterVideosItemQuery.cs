using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.ObterVideosItem
{
    public class ObterVideosItemQuery : IRequest<IEnumerable<ItemArquivoDto>>
    {
        public ObterVideosItemQuery(long itemId) { ItemId = itemId; }
        public long ItemId { get; }
    }
}
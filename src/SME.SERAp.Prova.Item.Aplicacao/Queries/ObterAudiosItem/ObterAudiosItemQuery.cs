using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.ObterAudiosItem
{
    public class ObterAudiosItemQuery : IRequest<IEnumerable<ItemArquivoDto>>
    {
        public ObterAudiosItemQuery(long itemId) { ItemId = itemId; }
        public long ItemId { get; }
    }
}
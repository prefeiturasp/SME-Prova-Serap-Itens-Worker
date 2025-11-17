using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item
{
    public class ObterItemPorIdQuery : IRequest<Dominio.Entities.Item>
    {
        public ObterItemPorIdQuery(long itemId)
        {
            ItemId = itemId;
        }

        public long ItemId { get; set; }
    }
}
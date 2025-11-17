using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Alternativa
{
    public class ObterAlternativasPorItemIdQuery : IRequest<IEnumerable<Dominio.Entities.Alternativa>>
    {
        public ObterAlternativasPorItemIdQuery(long itemId)
        {
            ItemId = itemId;
        }

        public long ItemId { get; set; }
    }
}
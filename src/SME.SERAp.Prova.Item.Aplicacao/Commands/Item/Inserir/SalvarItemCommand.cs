using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Item.Inserir
{
    public class SalvarItemCommand : IRequest<long>
    {
        public ItemDto Item { get; set; }

        public SalvarItemCommand(ItemDto item)
        {
            Item = item;
        }
    }
}
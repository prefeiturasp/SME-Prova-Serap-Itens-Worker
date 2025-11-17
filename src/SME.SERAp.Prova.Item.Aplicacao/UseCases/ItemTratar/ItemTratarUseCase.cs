using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Item.Inserir;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.ItemTratar
{
    public class ItemTratarUseCase : AbstractUseCase, IItemTratarUseCase
    {
        public ItemTratarUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var itemDto = mensagemRabbit.ObterObjetoMensagem<ItemDto>();

            if (itemDto == null)
                return false;

            long novoItemId = await mediator.Send(new SalvarItemCommand(itemDto));

            return novoItemId > 0;
        }
    }
}
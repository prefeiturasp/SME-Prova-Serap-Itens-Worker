using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class TipoItemTratarUseCase : AbstractUseCase, ITipoItemTratarUseCase
    {
        public TipoItemTratarUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var tipoItem = mensagemRabbit.ObterObjetoMensagem<TipoItemDto>();

            if (tipoItem == null) return false;
            if (!tipoItem.Validacao()) return false;

            var tipoItemAtual = await mediator.Send(new ObterTipoItemPorLegadoIdQuery(tipoItem.Id));

            if (tipoItemAtual == null)
                return await Inserir(tipoItem);

            return await Alterar(tipoItemAtual, tipoItem);
        }

        private async Task<bool> Inserir(TipoItemDto tipoItemApi)
        {
            var tipoItemInserir = new TipoItem(null, tipoItemApi.Id, tipoItemApi.EhPadrao, tipoItemApi.QtdeAlternativa, tipoItemApi.Descricao, (int)StatusGeral.Ativo);
            await mediator.Send(new InserirTipoItemCommand(tipoItemInserir));
            return true;
        }

        private async Task<bool> Alterar(TipoItem tipoItem, TipoItemDto tipoItemApi)
        {
            var tipoItemAlterar = new TipoItem(tipoItem.Id, tipoItemApi.Id, tipoItemApi.EhPadrao, tipoItemApi.QtdeAlternativa, tipoItemApi.Descricao, (int)tipoItemApi.Status);
            tipoItemAlterar.CriadoEm = tipoItem.CriadoEm;
            await mediator.Send(new AlterarTipoItemCommand(tipoItemAlterar));
            return true;
        }
    }
}

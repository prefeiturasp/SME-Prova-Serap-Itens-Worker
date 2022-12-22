using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class TipoItemSyncUseCase : AbstractUseCase, ITipoItemSyncUseCase
    {
        public TipoItemSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var tipoItensApi = await mediator.Send(new ObterTipoItensApiSerapQuery());
            if (tipoItensApi == null || !tipoItensApi.Any()) return false;

            await Tratar(tipoItensApi);

            return true;
        }

        private async Task Tratar(List<TipoItemDto> tipoItensApi)
        {
            var tipoItensTratar = tipoItensApi;

            var tipoItensItens = await mediator.Send(new ObterTodosTipoItensQuery());
            var tipoItensInativar = tipoItensItens.Where(a => !tipoItensApi.Any(api => api.Id == a.LegadoId));

            if (tipoItensInativar != null && tipoItensInativar.Any())
                tipoItensTratar.AddRange(tipoItensInativar.Select(a => new TipoItemDto(a.LegadoId, a.EhPadrao, a.QtdeAlternativa, a.Descricao, StatusGeral.Inativo)));

            foreach (var tipoItem in tipoItensTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TipoItemTratar, tipoItem));
        }
    }
}

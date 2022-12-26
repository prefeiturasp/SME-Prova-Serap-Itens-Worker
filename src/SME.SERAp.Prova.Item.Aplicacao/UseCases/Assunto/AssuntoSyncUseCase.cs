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
    public class AssuntoSyncUseCase : AbstractUseCase, IAssuntoSyncUseCase
    {
        public AssuntoSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var assuntosApi = await mediator.Send(new ObterAssuntosApiSerapQuery());
            if (assuntosApi == null || !assuntosApi.Any()) return false;

            await Tratar(assuntosApi);

            return true;
        }

        private async Task Tratar(List<AssuntoDto> assuntosApi)
        {
            var assuntosTratar = assuntosApi;

            var assuntosItens = await mediator.Send(new ObterTodosAssuntosQuery());
            var assuntosInativar = assuntosItens.Where(a => !assuntosApi.Any(api => api.Id == a.LegadoId));

            if (assuntosInativar != null && assuntosInativar.Any())
                assuntosTratar.AddRange(assuntosInativar.Select(a => new AssuntoDto(a.LegadoId, a.Descricao, StatusGeral.Inativo)));            

            foreach (var assunto in assuntosTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AssuntoTratar, assunto));
        }
    }
}

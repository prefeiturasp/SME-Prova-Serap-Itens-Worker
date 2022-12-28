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
    public class SubassuntoSyncUseCase : AbstractUseCase, ISubassuntoSyncUseCase
    {
        public SubassuntoSyncUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var assuntoLegadoId = mensagemRabbit.ObterStringMensagem();
            if (string.IsNullOrEmpty(assuntoLegadoId)) return false;

            var subassuntosApi = await mediator.Send(new ObterSubassuntosApiSerapQuery(long.Parse(assuntoLegadoId)));
            if (subassuntosApi == null || !subassuntosApi.Any()) return false;

            var assunto = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(long.Parse(assuntoLegadoId)));
            if (assunto != null && assunto.Id > 0)
                await Tratar(subassuntosApi, assunto.Id);

            return true;
        }

        private async Task Tratar(List<SubassuntoDto> subassuntosApi, long assuntoId)
        {
            var subassuntosTratar = subassuntosApi.Select(a => new SubassuntoDto(a.Id, assuntoId, a.Descricao, a.Status)).ToList();

            var subassuntosItens = await mediator.Send(new ObterSubassuntosPorAssuntoIdQuery(assuntoId));
            var subassuntosInativar = subassuntosItens.Where(a => !subassuntosTratar.Any(api => api.Id == a.LegadoId));

            if (subassuntosInativar != null && subassuntosInativar.Any())
                subassuntosTratar.AddRange(subassuntosInativar.Select(a => new SubassuntoDto(a.LegadoId, assuntoId, a.Descricao, StatusGeral.Inativo)));

            foreach (var subassunto in subassuntosTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.SubassuntoTratar, subassunto));
        }
    }
}

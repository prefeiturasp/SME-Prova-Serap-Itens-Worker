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
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var assuntoLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

            var subassuntosApi = await mediator.Send(new ObterSubassuntosApiSerapQuery(assuntoLegadoId));

            if (subassuntosApi == null || !subassuntosApi.Any())
                return false;

            var assuntoBase = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(assuntoLegadoId));

            foreach (var subassunto in subassuntosApi)
                subassunto.AtribuirAssuntoId(assuntoBase.Id);

            if (assuntoBase is { Id: > 0 })
                await Tratar(subassuntosApi);

            return true;
        }

        private async Task Tratar(List<SubassuntoDto> subassuntosApi)
        {
            var assuntoId = subassuntosApi.Select(c => c.AssuntoId).FirstOrDefault();

            var subassuntosBase = await mediator.Send(new ObterSubassuntosPorAssuntoIdQuery(assuntoId));
            var subassuntosInativar = subassuntosBase.Where(a => subassuntosApi.All(api => api.Id != a.LegadoId));

            if (subassuntosInativar.Any())
            {
                subassuntosApi.AddRange(subassuntosInativar.Select(a =>
                        new SubassuntoDto(a.LegadoId, a.AssuntoId, a.Descricao, StatusGeral.Inativo))
                    .Except(subassuntosApi));
            }

            foreach (var subassunto in subassuntosApi)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.SubassuntoTratar, subassunto));
        }
    }
}

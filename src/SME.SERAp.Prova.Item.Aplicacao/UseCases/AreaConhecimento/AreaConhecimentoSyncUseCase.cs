using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.AreaConhecimento
{
    public class AreaConhecimentoSyncUsecase : AbstractUseCase, IAreaConhecimentoSyncUsecase
    {
        public AreaConhecimentoSyncUsecase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var areaConhecimentoApi = await mediator.Send(new ObterAreaConhecimentoSerapApiQuery());
            if (areaConhecimentoApi == null || !areaConhecimentoApi.Any()) return false;

            await Tratar(areaConhecimentoApi);

            return true;
        }

        private async Task Tratar(IEnumerable<DisciplinaDto> listaAreaConhecimentoDto)
        {
            var areaConhecimentoTratar = listaAreaConhecimentoDto.ToList();

            var areaConhecimentosBase = await mediator.Send(new ObterTodasAreasConhecimentosQuery());
            var areasInativar = areaConhecimentosBase.Where(a => !listaAreaConhecimentoDto.Any(api => api.Id == a.LegadoId));

            if (areasInativar != null && areasInativar.Any())
                areaConhecimentoTratar.AddRange(areasInativar.Select(a => new DisciplinaDto(a.LegadoId, a.Descricao, StatusGeral.Inativo)));

            foreach (var area in areaConhecimentoTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.AreaConhecimentoTratar, area));
        }
    }
}
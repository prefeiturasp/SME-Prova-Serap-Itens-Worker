using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Disciplina.ApiSerap;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class DisciplinaSyncUseCase : AbstractUseCase, IDisciplinaSyncUseCase
    {
        public DisciplinaSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {

            var areaConhecimentoLegadoId = mensagemRabbit.ObterStringMensagem();

            if (string.IsNullOrEmpty(areaConhecimentoLegadoId)) return false;
            var disciplinaApi = await mediator.Send(new ObterDisciplinaPorAreaConhecimentoIdQuery(long.Parse(areaConhecimentoLegadoId)));

            if (disciplinaApi == null || !disciplinaApi.Any()) return false;

            var areaConhecimento = await mediator.Send(new ObterAreaPorLegadoIdQuery(long.Parse(areaConhecimentoLegadoId)));
            if (areaConhecimento != null && areaConhecimento.Id > 0)
                await Tratar(disciplinaApi, areaConhecimento.Id);

            return true;

        }

        private async Task Tratar(IEnumerable<DisciplinaDto> disciplinaApi, long areaConhecimentoId)
        {
            var disciplinaTratar = disciplinaApi.Select(d => new DisciplinaDto(d.Id, areaConhecimentoId, d.Descricao, d.NivelEnsino, d.Status)).ToList();

            var disciplinasBase = await mediator.Send(new ObterTodasDisciplinasQuery());
            var disciplinasInativar = disciplinasBase.Where(a => !disciplinaTratar.Any(api => api.Id == a.LegadoId));

            if (disciplinasInativar != null && disciplinasInativar.Any())
                disciplinaTratar.AddRange(disciplinasInativar.Select(a => new DisciplinaDto(a.LegadoId, areaConhecimentoId, a.Descricao, a.NivelEnsino, StatusGeral.Inativo)));

            foreach (var disciplina in disciplinaTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DisciplinaTratar, disciplina));

        }
    }
}
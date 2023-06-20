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
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var areaConhecimentoLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

            var disciplinasApi = await mediator.Send(new ObterDisciplinaPorAreaConhecimentoIdQuery(areaConhecimentoLegadoId));

            if (disciplinasApi == null || !disciplinasApi.Any())
                return false;

            var areaConhecimentoBase = await mediator.Send(new ObterAreaPorLegadoIdQuery(areaConhecimentoLegadoId));

            if (areaConhecimentoBase == null)
                return false;

            foreach (var disciplina in disciplinasApi)
                disciplina.AtribuirAreaConhecimentoId(areaConhecimentoBase.Id);

            await Tratar(disciplinasApi.ToList());

            return true;
        }

        private async Task Tratar(List<DisciplinaDto> disciplinasApi)
        {
            var areaConhecimentoId = disciplinasApi.Select(c => c.AreaConhecimentoId).FirstOrDefault();

            if (areaConhecimentoId <= 0)
                return;

            var disciplinasBase = (await mediator.Send(new ObterTodasDisciplinasQuery()))
                .Where(c => c.AreaConhecimentoId == areaConhecimentoId);

            var disciplinasInativar = disciplinasBase.Where(a => disciplinasApi.All(api => api.Id != a.LegadoId));

            if (disciplinasInativar.Any())
            {
                disciplinasApi.AddRange(disciplinasInativar.Select(a =>
                        new DisciplinaDto(a.LegadoId, a.AreaConhecimentoId, a.Descricao, a.NivelEnsino,
                            StatusGeral.Inativo))
                    .Except(disciplinasApi));
            }

            foreach (var disciplina in disciplinasApi)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.DisciplinaTratar, disciplina));
        }
    }
}
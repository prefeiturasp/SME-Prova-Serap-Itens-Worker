using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class MatrizSyncUseCase : AbstractUseCase, ISyncMatrizUseCase
    {
        public MatrizSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var disciplinaLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

            var matrizesApi = await mediator.Send(new ObterMatrizPorDisciplinaIdApiSerapQuery(disciplinaLegadoId));

            if (matrizesApi == null || !matrizesApi.Any())
                return false;

            var disciplinaBase = await mediator.Send(new ObterDisciplinaPorLegadoIdQuery(disciplinaLegadoId));

            if (disciplinaBase == null)
                return false;

            foreach (var matriz in matrizesApi)
                matriz.AtribuirDisciplinaId(disciplinaBase.Id);

            if (disciplinaBase is { Id: > 0 })
                await Tratar(matrizesApi.ToList(), disciplinaBase);

            return true;
        }

        private async Task Tratar(List<MatrizDto> matrizesApi, Disciplina disciplinaBase)
        {
            var matrizesBase = await mediator.Send(new ObterMatrizesPorDisciplinaIdQuery(disciplinaBase.Id));
            var matrizesInativar = matrizesBase.Where(a => matrizesApi.All(api => api.Id != a.LegadoId));

            if (matrizesInativar.Any())
            {
                matrizesApi.AddRange(matrizesInativar.Select(a =>
                        new MatrizDto(a.LegadoId, a.DisciplinaId, a.Descricao, StatusGeral.Inativo))
                    .Except(matrizesApi));
            }

            foreach (var matrizApi in matrizesApi)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.MatrizTratar, matrizApi));
        }
    }
}
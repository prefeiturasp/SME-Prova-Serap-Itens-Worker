using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
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
            var disciplinaLegadoId = mensagemRabbit.ObterStringMensagem();
            if (string.IsNullOrEmpty(disciplinaLegadoId)) return false;

            var matrizApi = await mediator.Send(new ObterMatrizPorDisciplinaIdApiSerapQuery(long.Parse(disciplinaLegadoId)));
            if (matrizApi == null || !matrizApi.Any()) return true;

            var disciplina = await mediator.Send(new ObterDisciplinasPorLegadoIdQuery(long.Parse(disciplinaLegadoId)));
            if (disciplina != null && disciplina.Id > 0)
                await Tratar(matrizApi.ToList(), disciplina.Id);

            return true;
        }

        private async Task Tratar(List<MatrizDto> matrizApi, long disciplinaId)
        {
            var matrizesTratar = matrizApi.Select(a => new MatrizDto(a.Id, disciplinaId, a.Descricao, a.Status)).ToList();

            var matrizesItens = await mediator.Send(new ObterMatrizPorDisciplinaIdQuery(disciplinaId));
            var matrizesInativar = matrizesItens.Where(a => !matrizesTratar.Any(api => api.Id == a.LegadoId));

            if (matrizesInativar != null && matrizesInativar.Any())
                matrizesTratar.AddRange(matrizesInativar.Select(a => new MatrizDto(a.LegadoId, disciplinaId, a.Descricao, StatusGeral.Inativo)));

            foreach (var matriz in matrizesTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.MatrizTratar, matriz));

        }
    }
}
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

            var matrizApi = await mediator.Send(new ObterMatrizPorDisciplinaIdQuery(long.Parse(disciplinaLegadoId)));
            if (matrizApi == null || !matrizApi.Any()) return false;

            var assunto = await mediator.Send(new ObterAssuntoPorLegadoIdQuery(long.Parse(disciplinaLegadoId)));
            if (assunto != null && assunto.Id > 0)
                await Tratar(matrizApi.ToList(), assunto.Id);

            return true;
        }

        private async Task Tratar(List<MatrizDto> matrizApi, long matrizId)
        {
            var matrizesTratar = matrizApi.Select(a => new MatrizDto(a.Id, matrizId, a.Descricao, a.Status)).ToList();

            var matrizesItens = await mediator.Send(new ObterSubassuntosPorAssuntoIdQuery(matrizId));
            var matrizesInativar = matrizesItens.Where(a => !matrizesTratar.Any(api => api.Id == a.LegadoId));

            if (matrizesInativar != null && matrizesInativar.Any())
                matrizesTratar.AddRange(matrizesInativar.Select(a => new MatrizDto(a.LegadoId, matrizId, a.Descricao, StatusGeral.Inativo)));

            foreach (var matriz in matrizesTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.MatrizTratar, matriz));
        }
    }
}
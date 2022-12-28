using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class CompetenciaSyncUseCase : AbstractUseCase, ICompetenciaSyncUseCase
    {
        private Matriz MatrizAtual;

        public CompetenciaSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {

            var matrizLegadoId = mensagemRabbit.ObterStringMensagem();
            if (string.IsNullOrEmpty(matrizLegadoId)) return false;

            MatrizAtual = await mediator.Send(new ObterMatrizPorLegadoIdQuery(long.Parse(matrizLegadoId)));
            if (MatrizAtual == null) return false;

            var competenciasApiSerap = await ObterCompetenciasApiSerap();
            if (!competenciasApiSerap.Any()) return false;

            return await Tratar(competenciasApiSerap);

        }

        private async Task<bool> Tratar(List<CompetenciaDto> dadosApi)
        {
            var dadosTratar = dadosApi;
            var dadosBDItem = await mediator.Send(new ObterCompetenciasPorMatrizLegadoIdQuery(MatrizAtual.LegadoId));
            var dadosInativar = dadosBDItem.Where(a => !dadosApi.Any(api => api.Id == a.LegadoId));

            if (dadosInativar != null && dadosInativar.Any())
                dadosTratar.AddRange(dadosInativar.Select(a => new CompetenciaDto(a.LegadoId, a.Codigo, MatrizAtual.Id, a.Descricao, StatusGeral.Inativo)));

            foreach (var dadoTratar in dadosTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.CompetenciaTratar, dadoTratar));

            return true;
        }

        private async Task<List<CompetenciaDto>> ObterCompetenciasApiSerap()
        {
            var list = new List<CompetenciaDto>();
            string uri = $"{UriApiSerap.Competencias}{MatrizAtual.LegadoId}";
            var resultApiSerap = await mediator.Send(new GetSimplesApiSerapQuery(uri));
            if (string.IsNullOrEmpty(resultApiSerap)) return list;

            var arrDto = JsonSerializer.Deserialize<CompetenciaDto[]>(resultApiSerap, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            if (arrDto != null && arrDto.Length > 0)
                list = arrDto.Select(a => a.AlterarMatrizIdStatus(MatrizAtual.Id, StatusGeral.Ativo)).ToList();
            return list;
        }
    }
}

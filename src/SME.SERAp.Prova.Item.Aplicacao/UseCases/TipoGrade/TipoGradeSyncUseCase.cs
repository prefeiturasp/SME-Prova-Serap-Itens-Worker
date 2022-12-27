using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class TipoGradeSyncUseCase : AbstractUseCase, ITipoGradeSyncUseCase
    {

        private long MatrizId;

        public TipoGradeSyncUseCase(IMediator mediator) : base(mediator){}

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {

            var matrizLegadoId = mensagemRabbit.ObterStringMensagem();
            if (string.IsNullOrEmpty(matrizLegadoId)) return false;

            var matrizAtual = await mediator.Send(new ObterMatrizPorLegadoIdQuery(long.Parse(matrizLegadoId)));
            if (matrizAtual == null) return false;

            MatrizId = matrizAtual.Id;

            var tiposGradeApiSerap = await ObterTiposGradeApiSerap();
            if (!tiposGradeApiSerap.Any()) return false;

            return await Tratar(tiposGradeApiSerap);

        }

        private async Task<bool> Tratar(List<TipoGradeDto> dadosApi)
        {
            var dadosTratar = dadosApi;
            var dadosBDItem = await mediator.Send(new ObterTipoGradePorMatrizLegadoIdQuery(MatrizId));
            var dadosInativar = dadosBDItem.Where(a => !dadosApi.Any(api => api.Id == a.LegadoId));

            if (dadosInativar != null && dadosInativar.Any())
                dadosTratar.AddRange(dadosInativar.Select(a => new TipoGradeDto(a.LegadoId, MatrizId, a.Descricao, a.Ordem, StatusGeral.Inativo)));

            foreach (var dadoTratar in dadosTratar)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TipoGradeTratar, dadoTratar));
            
            return true;
        }

        private async Task<List<TipoGradeDto>> ObterTiposGradeApiSerap()
        {
            var list = new List<TipoGradeDto>();
            string uri = $"{UriApiSerap.TiposGradeCurricular}{MatrizId}";
            var resultApiSerap = await mediator.Send(new GetSimplesApiSerapQuery(uri));
            if (string.IsNullOrEmpty(resultApiSerap)) return list;

            var arrDto = JsonSerializer.Deserialize<TipoGradeDto[]>(resultApiSerap, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            if (arrDto != null && arrDto.Length > 0)
                list = arrDto.Select(a => a.AlterarMatrizIdStatus(long.Parse(MatrizId), StatusGeral.Ativo)).ToList();
            return list;
        }
    }
}

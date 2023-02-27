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
    public class TipoGradeSyncUseCase : AbstractUseCase, ITipoGradeSyncUseCase
    {
        public TipoGradeSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var matrizLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());
            
            var matrizBase = await mediator.Send(new ObterMatrizPorLegadoIdQuery(matrizLegadoId));
            
            if (matrizBase == null)
                return false;

            var tiposGradeApi = await ObterTiposGradeApiSerap(matrizBase);

            if (!tiposGradeApi.Any())
                return false;

            foreach (var tipoGrade in tiposGradeApi)
            {
                tipoGrade.AtribuirMatrizId(matrizBase.Id);
                tipoGrade.AtribuirStatus(StatusGeral.Ativo);
            }

            return await Tratar(tiposGradeApi);
        }

        private async Task<bool> Tratar(List<TipoGradeDto> dadosApi)
        {
            var matrizLegadoId = dadosApi.Select(c => c.MatrizId).FirstOrDefault();

            if (matrizLegadoId <= 0)
                return false;
            
            var tiposGradeBase = await mediator.Send(new ObterTipoGradePorMatrizLegadoIdQuery(matrizLegadoId));
            var tiposGradeInativar = tiposGradeBase.Where(a => dadosApi.All(api => api.Id != a.LegadoId));

            if (tiposGradeInativar.Any())
            {
                dadosApi.AddRange(tiposGradeInativar.Select(a =>
                        new TipoGradeDto(a.LegadoId, a.MatrizId, a.Descricao, a.Ordem, StatusGeral.Inativo))
                    .Except(dadosApi));
            }

            foreach (var dadoTratar in dadosApi)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TipoGradeTratar, dadoTratar));

            return true;
        }

        private async Task<List<TipoGradeDto>> ObterTiposGradeApiSerap(Matriz matrizBase)
        {
            var list = new List<TipoGradeDto>();
            
            var uri = $"{UriApiSerap.TiposGradeCurricular}{matrizBase.LegadoId}";
            var resultApiSerap = await mediator.Send(new GetSimplesApiSerapQuery(uri));
            
            if (string.IsNullOrEmpty(resultApiSerap)) 
                return list;

            var arrDto = JsonSerializer.Deserialize<TipoGradeDto[]>(resultApiSerap,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if (arrDto is { Length: > 0 })
                list.AddRange(arrDto);
            
            return list;
        }
    }
}

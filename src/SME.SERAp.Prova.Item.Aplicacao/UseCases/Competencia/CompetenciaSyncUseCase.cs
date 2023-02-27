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
        public CompetenciaSyncUseCase(IMediator mediator) : base(mediator) { }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;
            
            var matrizLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());
            
            var matrizBase = await mediator.Send(new ObterMatrizPorLegadoIdQuery(matrizLegadoId));
            
            if (matrizBase == null)
                return false;

            var competenciasApi = await ObterCompetenciasApiSerap(matrizBase);
            
            if (competenciasApi == null || !competenciasApi.Any()) 
                return false;            

            foreach (var competenciaApi in competenciasApi)
            {
                competenciaApi.AtribuirMatrizId(matrizBase.Id);
                competenciaApi.AtribuirStatus(StatusGeral.Ativo);
            }

            await Tratar(competenciasApi, matrizBase);

            return true;
        }

        private async Task Tratar(List<CompetenciaDto> competenciasApi, Matriz matrizBase)
        {
            var competenciasBase = await mediator.Send(new ObterCompetenciasPorMatrizLegadoIdQuery(matrizBase.LegadoId));

            if (!competenciasBase.Any())
                return;
            
            var competenciasInativar = competenciasBase.Where(a => competenciasApi.All(api => api.Id != a.LegadoId));

            if (competenciasInativar.Any())
            {
                competenciasApi.AddRange(competenciasInativar.Select(a =>
                        new CompetenciaDto(a.LegadoId, a.Codigo, a.MatrizId, a.Descricao, StatusGeral.Inativo))
                    .Except(competenciasApi));
            }

            foreach (var competenciaApi in competenciasApi)
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.CompetenciaTratar, competenciaApi));
        }

        private async Task<List<CompetenciaDto>> ObterCompetenciasApiSerap(Matriz matrizBase)
        {
            var list = new List<CompetenciaDto>();

            var uri = $"{UriApiSerap.Competencias}{matrizBase.LegadoId}";
            var resultApiSerap = await mediator.Send(new GetSimplesApiSerapQuery(uri));

            if (string.IsNullOrEmpty(resultApiSerap)) 
                return list;

            var arrDto = JsonSerializer.Deserialize<CompetenciaDto[]>(resultApiSerap,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            if (arrDto is { Length: > 0 })
                list.AddRange(arrDto);
            
            return list;
        }
    }
}

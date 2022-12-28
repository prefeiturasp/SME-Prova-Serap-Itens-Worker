using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Fila;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class HabilidadeSyncUseCase : AbstractUseCase, IHabilidadeSyncUseCase
    {
        public HabilidadeSyncUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var competenciaLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

            var habilidadesApi = await mediator.Send(new ObterHabilidadeApiSerapQuery(competenciaLegadoId));
            if (habilidadesApi == null || !habilidadesApi.Any()) return false;

            var competencia = await mediator.Send(new ObterCompetenciaPorLegadoIdQuery(competenciaLegadoId));

            foreach (var habilidadeApi in habilidadesApi)
            {
                var habilidade = new Dominio.Entities.Habilidade(habilidadeApi.Id, competencia.Id, habilidadeApi.Codigo, habilidadeApi.Descricao);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeTratar, habilidade));
            }

            var habilidadesBanco = await mediator.Send(new ObterHabilidadesPorCompetenciaLegadoIdQuery(competenciaLegadoId));
            if (habilidadesBanco != null && habilidadesBanco.Any())
            {
                var inativos = habilidadesBanco.Where(t => !habilidadesApi.Any(x => x.Id == t.LegadoId));
                if (inativos.Any())
                {
                    foreach (var inativo in inativos)
                    {
                        inativo.Inativar();
                        await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeTratar, inativo));
                    }
                }
            }

            return true;
        }
    }
}

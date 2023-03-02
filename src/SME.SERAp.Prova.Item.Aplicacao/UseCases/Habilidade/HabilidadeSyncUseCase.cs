using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
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
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var competenciaLegadoId = long.Parse(mensagemRabbit.ObterStringMensagem());

            var habilidadesApi = await mediator.Send(new ObterHabilidadeApiSerapQuery(competenciaLegadoId));

            if (habilidadesApi == null || !habilidadesApi.Any())
                return false;

            var competenciaBase = await mediator.Send(new ObterCompetenciaPorLegadoIdQuery(competenciaLegadoId));

            foreach (var habilidadeApi in habilidadesApi)
            {
                var habilidade = new Habilidade(habilidadeApi.Id, competenciaBase.Id, habilidadeApi.Codigo, habilidadeApi.Descricao);
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeTratar, habilidade));
            }

            var habilidadesBase = await mediator.Send(new ObterHabilidadesPorCompetenciaLegadoIdQuery(competenciaLegadoId));

            if (habilidadesBase == null || !habilidadesBase.Any())
                return true;

            var inativos = habilidadesBase.Where(t => !habilidadesApi.Any(x => x.Id == t.LegadoId));

            if (!inativos.Any())
                return true;

            foreach (var inativo in inativos)
            {
                inativo.Inativar();
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.HabilidadeTratar, inativo));
            }

            return true;
        }
    }
}

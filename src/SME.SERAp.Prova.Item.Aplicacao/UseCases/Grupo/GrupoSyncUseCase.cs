using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GrupoSyncUseCase : AbstractUseCase, IGrupoSyncUseCase
    {
        private readonly CoressoOptions coressoOptions;

        public GrupoSyncUseCase(IMediator mediator, CoressoOptions coressoOptions) : base(mediator)
        {
            this.coressoOptions = coressoOptions ?? throw new ArgumentNullException(nameof(coressoOptions));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var gruposCoresso = await mediator.Send(new ObterGruposCoreSSOQuery(coressoOptions.SistemaId, coressoOptions.ItensModuloId));

            foreach (var grupoCoresso in gruposCoresso)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.GrupoTratar, grupoCoresso));
            }

            var grupos = await mediator.Send(new ObterTodosGruposQuery());
            foreach (var grupo in grupos)
            {
                if (!gruposCoresso.Any(t => t.Id == grupo.LegadoId))
                {
                    grupo.Inativar();
                    await mediator.Send(new AlterarGrupoCommand(grupo));
                }
            }

            return true;
        }
    }
}
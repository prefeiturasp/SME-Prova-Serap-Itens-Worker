using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos.Usuario;
using SME.SERAp.Prova.Item.Infra.Dtos.UsuarioGrupo;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UsuarioSyncUseCase : AbstractUseCase, IUsuarioSyncUseCase
    {
        private readonly CoressoOptions coressoOptions;

        public UsuarioSyncUseCase(IMediator mediator, CoressoOptions coressoOptions) : base(mediator)
        {
            this.coressoOptions = coressoOptions ?? throw new ArgumentNullException(nameof(coressoOptions));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            if (string.IsNullOrEmpty(mensagemRabbit.ObterStringMensagem()))
                return false;

            var grupoLegadoId = Guid.Parse(mensagemRabbit.ObterStringMensagem());

            var grupo = await mediator.Send(new ObterGrupoPorLegadoIdQuery(grupoLegadoId));
            if (grupo == null) throw new Exception($"Grupo {grupoLegadoId} não encontrado");

            var usuariosCoresso = await mediator.Send(new ObterUsuariosPorGrupoIdCoreSSOQuery(coressoOptions.SistemaId,
                coressoOptions.ItensModuloId, grupoLegadoId));

            foreach (var usuarioCoresso in usuariosCoresso)
            {
                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UsuarioTratar, new UsuarioTratarDto()
                {
                    GrupoId = grupo.Id,
                    GrupoLegadoId = grupo.LegadoId,
                    UsuarioCoresso = usuarioCoresso
                }));
            }

            var usuarios = await mediator.Send(new ObterUsuariosPorGrupoIdQuery(grupo.Id));
            foreach (var usuario in usuarios)
            {
                if (!usuariosCoresso.Any(t => t.Id == usuario.LegadoId))
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UsuarioGrupoInativar, new UsuarioGrupoDto(grupo.Id, usuario.Id)));
                }
            }

            return true;
        }
    }
}

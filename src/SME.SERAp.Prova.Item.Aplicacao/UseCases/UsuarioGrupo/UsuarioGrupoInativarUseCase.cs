using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos.UsuarioGrupo;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UsuarioGrupoInativarUseCase : AbstractUseCase, IUsuarioGrupoInativarUseCase
    {
        public UsuarioGrupoInativarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var usuarioGrupoDto = mensagemRabbit.ObterObjetoMensagem<UsuarioGrupoDto>();
            
            if (usuarioGrupoDto == null)
                throw new Exception("Usuario Grupo inválido");

            var usuarioGrupo = await mediator.Send(new ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery(usuarioGrupoDto.UsuarioId, usuarioGrupoDto.GrupoId));
            
            if (usuarioGrupo == null)
                throw new Exception("Usuario Grupo não encontrado");

            usuarioGrupo.Inativar();

            await mediator.Send(new AlterarUsuarioGrupoCommand(usuarioGrupo));
            return true;
        }
    }
}

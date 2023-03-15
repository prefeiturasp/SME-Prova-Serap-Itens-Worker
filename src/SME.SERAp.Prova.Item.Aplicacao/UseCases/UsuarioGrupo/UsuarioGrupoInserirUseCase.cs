using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.UsuarioGrupo;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UsuarioGrupoInserirUseCase : AbstractUseCase, IUsuarioGrupoInserirUseCase
    {
        public UsuarioGrupoInserirUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var usuarioGrupo = mensagemRabbit.ObterObjetoMensagem<UsuarioGrupoDto>();

            if (usuarioGrupo == null)
                throw new Exception("Usuario Grupo inválido");

            await mediator.Send(new InserirUsuarioGrupoCommand(new UsuarioGrupo(usuarioGrupo.UsuarioId, usuarioGrupo.GrupoId)));
            return true;
        }
    }
}

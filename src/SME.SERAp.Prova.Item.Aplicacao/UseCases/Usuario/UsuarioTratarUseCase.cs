using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.Usuario;
using SME.SERAp.Prova.Item.Infra.Dtos.UsuarioGrupo;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UsuarioTratarUseCase : AbstractUseCase, IUsuarioTratarUseCase
    {
        public UsuarioTratarUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var usuarioTratar = mensagemRabbit.ObterObjetoMensagem<UsuarioTratarDto>();
            
            if (usuarioTratar == null) 
                throw new Exception("Usuário coresso inválido");

            var usuario = await mediator.Send(new ObterUsuarioPorLegadoIdQuery(usuarioTratar.UsuarioCoresso.Id));
            if (usuario == null)
            {
                var inserir = new Usuario(
                    usuarioTratar.UsuarioCoresso.Id,
                    usuarioTratar.UsuarioCoresso.Login,
                    usuarioTratar.UsuarioCoresso.Nome);

                var usuarioId = await mediator.Send(new InserirUsuarioCommand(inserir));

                await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UsuarioGrupoInserir, new UsuarioGrupoDto(usuarioId, usuarioTratar.GrupoId)));
            }
            else
            {
                if (usuarioTratar.UsuarioCoresso.Login != usuario.Login ||
                    usuarioTratar.UsuarioCoresso.Nome != usuario.Nome)
                {
                    usuario.Alterar(usuarioTratar.UsuarioCoresso.Login, usuarioTratar.UsuarioCoresso.Nome);
                    await mediator.Send(new AlterarUsuarioCommand(usuario));
                }

                var usuarioGrupo = await mediator.Send(new ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery(usuario.Id, usuarioTratar.GrupoId));
                if (usuarioGrupo == null)
                {
                    await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.UsuarioGrupoInserir, new UsuarioGrupoDto(usuario.Id, usuarioTratar.GrupoId)));
                }
            }

            return true;
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class AlterarUsuarioGrupoCommand : IRequest<long>
    {
        public AlterarUsuarioGrupoCommand(UsuarioGrupo usuarioGrupo)
        {
            UsuarioGrupo = usuarioGrupo;
        }

        public UsuarioGrupo UsuarioGrupo { get; set; }

    }
}

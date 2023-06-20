using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class InserirUsuarioGrupoCommand : IRequest<long>
    {
        public InserirUsuarioGrupoCommand(UsuarioGrupo usuarioGrupo)
        {
            UsuarioGrupo = usuarioGrupo;
        }

        public UsuarioGrupo UsuarioGrupo { get; set; }
    }
}

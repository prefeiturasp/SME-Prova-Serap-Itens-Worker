using MediatR;
using SME.SERAp.Prova.Item.Dominio;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery : IRequest<UsuarioGrupo>
    {
        public ObterUsuarioGrupoPorUsuarioIdGrupoIdQuery(long usuarioId, long grupoId)
        {
            UsuarioId = usuarioId;
            GrupoId = grupoId;
        }

        public long UsuarioId { get; set; }
        public long GrupoId { get; set; }
    }
}

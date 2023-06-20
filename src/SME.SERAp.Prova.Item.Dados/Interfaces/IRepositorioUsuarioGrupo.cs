using SME.SERAp.Prova.Item.Dominio;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioUsuarioGrupo : IRepositorioBase<UsuarioGrupo>
    {
        Task<UsuarioGrupo> ObterPorUsuarioIdGrupoIdAsync(long usuarioId, long grupoId);
    }
}

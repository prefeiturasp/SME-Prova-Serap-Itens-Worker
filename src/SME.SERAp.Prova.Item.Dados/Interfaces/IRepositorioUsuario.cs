using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Task<IEnumerable<Usuario>> ObterPorGrupoIdAsync(long grupoId);
        Task<Usuario> ObterPorLegadoIdAsync(Guid legadoId);
    }
}

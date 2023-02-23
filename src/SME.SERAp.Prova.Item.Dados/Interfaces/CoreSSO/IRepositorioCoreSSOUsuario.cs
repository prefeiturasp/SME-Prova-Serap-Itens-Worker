using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO
{
    public interface IRepositorioCoreSSOUsuario
    {
        Task<IEnumerable<UsuarioCoressoDto>> ObterUsuariosAtivosPorGrupoIdAsync(long sistemaId, long moduloId, Guid grupoLegadoId);
    }
}

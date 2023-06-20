using SME.SERAp.Prova.Item.Infra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces.CoreSSO
{
    public interface IRepositorioCoreSSOGrupo
    {
        Task<IEnumerable<GrupoCoressoDto>> ObterGruposAtivosAsync(long sistemaId, long moduloId);
    }
}

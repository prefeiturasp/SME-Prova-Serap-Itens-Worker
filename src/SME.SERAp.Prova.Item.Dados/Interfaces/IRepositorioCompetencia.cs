using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioCompetencia : IRepositorioBase<Competencia>
    {
        Task<Competencia> ObterPorLegadoIdAsync(long legadoId);
        Task<IEnumerable<Competencia>> ObterPorMatrizLegadoIdAsync(long matrizLegadoId);
    }
}

using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioTipoGrade : IRepositorioBase<TipoGrade>
    {
        Task<TipoGrade> ObterPorLegadoIdAsync(long legadoId);
        Task<IEnumerable<TipoGrade>> ObterPorMatrizLegadoIdAsync(long matrizLegadoId);
    }
}

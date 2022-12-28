using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioSubassunto : IRepositorioBase<Subassunto>
    {
        Task<Subassunto> ObterPorLegadoIdAsync(long legadoId);
        Task<IEnumerable<Subassunto>> ObterPorAssuntoIdAsync(long assuntoId);
    }
}

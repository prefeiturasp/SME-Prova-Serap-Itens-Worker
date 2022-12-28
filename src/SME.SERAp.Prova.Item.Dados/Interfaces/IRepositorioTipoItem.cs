using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioTipoItem : IRepositorioBase<TipoItem>
    {
        Task<TipoItem> ObterPorLegadoIdAsync(long legadoId);
    }
}

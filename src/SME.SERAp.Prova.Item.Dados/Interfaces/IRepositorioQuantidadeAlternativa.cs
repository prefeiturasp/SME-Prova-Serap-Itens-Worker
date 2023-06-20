using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioQuantidadeAlternativa : IRepositorioBase<QuantidadeAlternativa>
    {
        Task<QuantidadeAlternativa> ObterPorLegadoIdAsync(long legadoId);
    }
}

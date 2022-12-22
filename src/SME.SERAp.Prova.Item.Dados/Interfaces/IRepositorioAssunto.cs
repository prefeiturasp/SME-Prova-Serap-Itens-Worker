using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioAssunto : IRepositorioBase<Assunto>
    {
        Task<Assunto> ObterPorLegadoIdAsync(long legadoId);
    }
}

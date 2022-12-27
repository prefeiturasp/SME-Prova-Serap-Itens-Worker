
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioDisciplina : IRepositorioBase<Disciplina>
    {
        public Task<Disciplina> ObterPorLegadoIdAsync(long legadoId);
    }
}

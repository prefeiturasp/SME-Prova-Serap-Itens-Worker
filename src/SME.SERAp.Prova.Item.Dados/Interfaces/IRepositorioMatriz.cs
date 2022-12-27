using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioMatriz : IRepositorioBase<Matriz>
    {
        public  Task<Matriz> ObterPorLegadoIdAsync(long legadoId);
        public Task<IEnumerable<Matriz>> ObterPorDisciplinaId(long disciplinaId);


    }
}

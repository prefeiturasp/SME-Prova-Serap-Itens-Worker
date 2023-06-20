
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioDisciplina : IRepositorioBase<Disciplina>
    {
        public Task<Disciplina> ObterPorLegadoIdAsync(long legadoId);
        public Task<IEnumerable<Disciplina>> ObterPorAreaConhecimentoLegadoIdAsync(long areaConhecimentoLegadoId);
    }
}

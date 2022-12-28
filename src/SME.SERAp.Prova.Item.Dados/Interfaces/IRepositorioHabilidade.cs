using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioHabilidade : IRepositorioBase<Habilidade>
    {
        Task<Habilidade> ObterPorLegadoIdAsync(long legadoId);
        Task<IEnumerable<Habilidade>> ObterPorCompetenciaLegadoIdAsync(long competenciaLegadoId);
    }
}

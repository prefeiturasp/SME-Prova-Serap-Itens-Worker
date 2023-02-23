using SME.SERAp.Prova.Item.Dominio;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioGrupo : IRepositorioBase<Grupo>
    {
        Task<Grupo> ObterPorLegadoIdAsync(Guid legadoId);
    }
}

using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioItem : IRepositorioBase<Dominio.Entities.Item>
    {
        Task<Dominio.Entities.Item> ObterPorId(long itemId);
        Task<IEnumerable<Alternativa>> ObterAlternativasPorItemId(long itemId);
    }
}
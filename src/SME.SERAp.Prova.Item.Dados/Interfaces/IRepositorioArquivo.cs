using SME.SERAp.Prova.Item.Infra.Dtos.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioArquivo
    {
        Task<IEnumerable<ItemArquivoDto>> ObterAudiosPorItemIdAsync(long itemId);
        Task<IEnumerable<ItemArquivoDto>> ObterVideosPorItemIdAsync(long itemId);
    }
}

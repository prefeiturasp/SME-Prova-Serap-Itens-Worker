using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Infra.Interfaces
{
    public interface IServicoItemLegado
    {
        Task<bool> SalvarItemNoLegado(ItemSalvarLegadoDto itemSalvarLegadoDto);
    }
}
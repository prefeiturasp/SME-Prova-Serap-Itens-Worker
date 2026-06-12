using SME.SERAp.Prova.Item.Infra.Fila;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IItemSalvarLegadoUseCase
    {
        Task<bool> Executar(MensagemRabbit mensagem);
    }
}
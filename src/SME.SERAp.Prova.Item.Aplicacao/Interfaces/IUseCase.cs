using SME.SERAp.Prova.Item.Infra.Fila;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IUseCase
    {
        Task<bool> Executar(MensagemRabbit mensagemRabbit);
    }
}

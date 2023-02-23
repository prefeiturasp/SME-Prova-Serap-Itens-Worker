using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Fila;

namespace SME.SERAp.Prova.Item.Infra.Interfaces
{
    public interface IServicoMensageria
    {
        Task<bool> Publicar(MensagemRabbit mensagemRabbit, string rota, string exchange, string nomeAcao);
    }
}
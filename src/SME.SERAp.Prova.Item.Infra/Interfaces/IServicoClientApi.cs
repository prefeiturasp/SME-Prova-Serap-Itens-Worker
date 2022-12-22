using System.Net.Http;

namespace SME.SERAp.Prova.Item.Infra
{
    public interface IServicoClientApi
    {
        HttpClient ObterClientSerapApi();
    }
}

using System;
using System.Net.Http;

namespace SME.SERAp.Prova.Item.Infra
{
    public class ServicoClientApi : IServicoClientApi
    {

        private readonly ClientApiOptions clientApiOptions;

        public ServicoClientApi(ClientApiOptions clientApiOptions)
        {
            this.clientApiOptions = clientApiOptions ?? throw new ArgumentNullException(nameof(clientApiOptions));
        }

        public HttpClient ObterClientSerapApi()
        {
            var clientSerapApi = new HttpClient();
            clientSerapApi.BaseAddress = new Uri(clientApiOptions.SerapApiUrlBase);
            clientSerapApi.DefaultRequestHeaders.Accept.Clear();
            clientSerapApi.DefaultRequestHeaders.Add(clientApiOptions.SerapApiNomeChave, clientApiOptions.SerapApiValorChave);
            return clientSerapApi;
        }
    }
}

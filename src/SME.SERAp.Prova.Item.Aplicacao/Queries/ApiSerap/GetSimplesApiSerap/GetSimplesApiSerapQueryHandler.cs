using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GetSimplesApiSerapQueryHandler : IRequestHandler<GetSimplesApiSerapQuery, string>
    {
        private readonly IServicoClientApi servicoClientApi;

        public GetSimplesApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<string> Handle(GetSimplesApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync(request.RequestUri);

                if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                string content = await response?.Content?.ReadAsStringAsync() ?? string.Empty;
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    throw new Exception($"Não foi possível obter os dados. StatusCode:{response.StatusCode}, Content:{content}, Uri:{request.RequestUri}");
                return string.Empty;
            }
            catch
            {
                throw;
            }
        }
    }
}

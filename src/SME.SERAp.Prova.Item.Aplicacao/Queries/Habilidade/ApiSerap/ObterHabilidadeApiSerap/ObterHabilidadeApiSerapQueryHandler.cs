using MediatR;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadeApiSerapQueryHandler : IRequestHandler<ObterHabilidadeApiSerapQuery, IEnumerable<HabilidadeDto>>
    {
        private readonly IServicoClientApi servicoClientApi;

        public ObterHabilidadeApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<IEnumerable<HabilidadeDto>> Handle(ObterHabilidadeApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                string uri = $"{UriApiSerap.Habilidades}{request.CompetenciaId}";
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var assuntos = JsonSerializer.Deserialize<HabilidadeDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return assuntos;
                }
                string content = await response?.Content?.ReadAsStringAsync() ?? string.Empty;
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    throw new Exception($"Não foi possível obter os dados. StatusCode:{response.StatusCode}, Content:{content}, Uri:{uri}");
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

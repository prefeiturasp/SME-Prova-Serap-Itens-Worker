using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosApiSerapQueryHandler : IRequestHandler<ObterAssuntosApiSerapQuery, List<AssuntoDto>>
    {

        private readonly IServicoClientApi servicoClientApi;

        public ObterAssuntosApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<List<AssuntoDto>> Handle(ObterAssuntosApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync("Item/Assuntos");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var assuntos = JsonSerializer.Deserialize<AssuntoDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (assuntos != null && assuntos.Length > 0)
                        return assuntos.Select(a => new AssuntoDto(a.Id, a.Descricao, Dominio.StatusGeral.Ativo)).ToList();
                    return default;
                }
                throw new Exception("Não foi possível obter os dados");
            }
            catch
            {
                throw;
            }
        }
    }
}

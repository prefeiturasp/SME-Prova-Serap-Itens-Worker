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
    public class ObterSubassuntosApiSerapQueryHandler : IRequestHandler<ObterSubassuntosApiSerapQuery, List<SubassuntoDto>>
    {
        private readonly IServicoClientApi servicoClientApi;

        public ObterSubassuntosApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<List<SubassuntoDto>> Handle(ObterSubassuntosApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync($"Item/Subassuntos/AssuntoId?assuntoId={request.AssuntoLegadoId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var dadosResult = JsonSerializer.Deserialize<SubassuntoDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (dadosResult != null && dadosResult.Length > 0)
                        return dadosResult.Select(a => new SubassuntoDto(a.Id, 0, a.Descricao, Dominio.StatusGeral.Ativo)).ToList();
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

using MediatR;
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
            var client = servicoClientApi.ObterClientSerapApi();
            HttpResponseMessage response = await client.GetAsync("api/Item/Habilidades/CompetenciaId?competenciaId=" + request.CompetenciaId);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var assuntos = JsonSerializer.Deserialize<HabilidadeDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return assuntos;
            }
            throw new Exception("Não foi possível obter os dados");
        }
    }
}

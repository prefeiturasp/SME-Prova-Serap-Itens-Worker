using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos.Dificuldade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDificuldadesApiSerapQueryHandler : IRequestHandler<ObterDificuldadesApiSerapQuery, List<DificuldadeDto>>
    {
        private readonly IServicoClientApi servicoClientApi;

        public ObterDificuldadesApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<List<DificuldadeDto>> Handle(ObterDificuldadesApiSerapQuery request, CancellationToken cancellationToken)
        {
            var client = servicoClientApi.ObterClientSerapApi();
            HttpResponseMessage response = await client.GetAsync("Item/Dificuldades");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var assuntos = JsonSerializer.Deserialize<DificuldadeDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                if (assuntos != null && assuntos.Length > 0)
                    return assuntos.Select(a => new DificuldadeDto(a.Id, a.Descricao, a.Ordem, Dominio.StatusGeral.Ativo)).ToList();
                return default;
            }
            throw new Exception("Não foi possível obter os dados");
        }
    }
}

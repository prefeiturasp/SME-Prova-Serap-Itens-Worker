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
    public class ObterQuantidadeAlternativaApiSerapQueryHandler : IRequestHandler<ObterQuantidadeAlternativaApiSerapQuery, List<QuantidadeAlternativaDto>>
    {

        private readonly IServicoClientApi servicoClientApi;

        public ObterQuantidadeAlternativaApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<List<QuantidadeAlternativaDto>> Handle(ObterQuantidadeAlternativaApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync("Item/Tipos");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tipoItens = JsonSerializer.Deserialize<QuantidadeAlternativaDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (tipoItens != null && tipoItens.Length > 0)
                        return tipoItens.Select(a => new QuantidadeAlternativaDto(a.Id, a.EhPadrao, a.QuantidadeAlternativa, a.Descricao, Dominio.StatusGeral.Ativo)).ToList();
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

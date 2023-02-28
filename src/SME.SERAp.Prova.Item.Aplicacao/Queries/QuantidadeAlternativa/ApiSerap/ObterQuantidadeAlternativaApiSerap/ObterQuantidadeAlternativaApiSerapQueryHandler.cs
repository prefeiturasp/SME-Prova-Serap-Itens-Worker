using MediatR;
using SME.SERAp.Prova.Item.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var client = servicoClientApi.ObterClientSerapApi();
            var response = await client.GetAsync("Item/Tipos", cancellationToken);

            if (!response.IsSuccessStatusCode) 
                throw new Exception("Não foi possível obter os dados");
                
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            var quantidadesAlternativas = JsonSerializer.Deserialize<QuantidadeAlternativaDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return quantidadesAlternativas is { Length: > 0 }
                ? quantidadesAlternativas.Select(a => new QuantidadeAlternativaDto(a.Id, a.EhPadrao,
                    a.QuantidadeAlternativa, a.Descricao, Dominio.StatusGeral.Ativo)).ToList()
                : default;
        }
    }
}

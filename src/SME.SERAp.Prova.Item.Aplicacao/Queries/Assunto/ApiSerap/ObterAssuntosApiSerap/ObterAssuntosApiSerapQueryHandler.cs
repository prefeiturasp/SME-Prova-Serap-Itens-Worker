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
        public ObterAssuntosApiSerapQueryHandler(){}

        public async Task<List<AssuntoDto>> Handle(ObterAssuntosApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://hom-serapapi.sme.prefeitura.sp.gov.br/api");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("keyApi", "xxxxx");
                HttpResponseMessage response = await client.GetAsync("Item/Assuntos");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    //result = result.Replace("\"NA\"", "0").Replace(request.AlunoId.ToString(), "").Replace("_", "");
                    var assuntos = JsonSerializer.Deserialize<AssuntoDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return assuntos.ToList();
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

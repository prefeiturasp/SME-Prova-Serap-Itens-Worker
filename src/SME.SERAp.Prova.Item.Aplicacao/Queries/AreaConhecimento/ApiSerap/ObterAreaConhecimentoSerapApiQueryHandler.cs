using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaConhecimentoSerapApiQueryHandler : IRequestHandler<ObterAreaConhecimentoSerapApiQuery, IEnumerable<DisciplinaDto>>
    {
        private readonly IServicoClientApi servicoClientApi;
        private readonly IServicoLog servicoLog;

        public ObterAreaConhecimentoSerapApiQueryHandler(IServicoClientApi servicoClientApi, IServicoLog servicoLog)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<IEnumerable<DisciplinaDto>> Handle(ObterAreaConhecimentoSerapApiQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync("Item/AreasConhecimento");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var areasConhecimento = JsonSerializer.Deserialize<IEnumerable<DisciplinaDto>>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return areasConhecimento;
                }
                throw new Exception($"Não foi possível obter os dados, resposta da api: {response.StatusCode}.");

            }
            catch (Exception ex)
            {
                servicoLog.Registrar("Erro ao tentar obter dados api serap -  Item/AreasConhecimento", ex);
                throw;
            }
        }

    }
}

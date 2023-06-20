using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaConhecimentoSerapApiQueryHandler : IRequestHandler<ObterAreaConhecimentoSerapApiQuery, IEnumerable<AreaConhecimentoDto>>
    {
        private readonly IServicoClientApi servicoClientApi;
        private readonly IServicoLog servicoLog;

        public ObterAreaConhecimentoSerapApiQueryHandler(IServicoClientApi servicoClientApi, IServicoLog servicoLog)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<IEnumerable<AreaConhecimentoDto>> Handle(ObterAreaConhecimentoSerapApiQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                var response = await client.GetAsync("Item/AreasConhecimento", cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Não foi possível obter os dados, resposta da api: {response.StatusCode}.");

                var result = await response.Content.ReadAsStringAsync(cancellationToken);

                if (result is null or "")
                    return null;

                var areasConhecimento = JsonSerializer.Deserialize<IEnumerable<AreaConhecimentoDto>>(result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return areasConhecimento.Select(a => new AreaConhecimentoDto(a.Id, a.Descricao, Dominio.StatusGeral.Ativo)).ToList();

            }
            catch (Exception ex)
            {
                servicoLog.Registrar("Erro ao tentar obter dados api serap -  Item/AreasConhecimento", ex);
                throw;
            }
        }
    }
}

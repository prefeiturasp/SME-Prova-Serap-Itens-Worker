﻿using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Disciplina.ApiSerap
{
    public class ObterDisciplinaPorAreaConhecimentoIdQueryHandler : IRequestHandler<ObterDisciplinaPorAreaConhecimentoIdQuery, IEnumerable<DisciplinaDto>>
    {
        private readonly IServicoClientApi servicoClientApi;
        private readonly IServicoLog servicoLog;

        public ObterDisciplinaPorAreaConhecimentoIdQueryHandler(IServicoClientApi servicoClientApi, IServicoLog servicoLog)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<IEnumerable<DisciplinaDto>> Handle(ObterDisciplinaPorAreaConhecimentoIdQuery request, CancellationToken cancellationToken)
        {
            var rota = $"Item/Disciplinas/AreaConhecimentoid?areaconhecimentoId={request.AreaConhecimentoId}";

            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                var response = await client.GetAsync(rota, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Não foi possível obter os dados, resposta da api: {response.StatusCode}.");

                var result = await response.Content.ReadAsStringAsync(cancellationToken);

                if (result is null or "")
                    return null;

                var disciplinas = JsonSerializer.Deserialize<IEnumerable<DisciplinaDto>>(result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return disciplinas.Select(a => new DisciplinaDto(a.Id, 0, a.Descricao, a.NivelEnsino, Dominio.StatusGeral.Ativo)).ToList();
            }
            catch (Exception ex)
            {
                servicoLog.Registrar($"Erro ao tentar obter dados api serap -  rota: {rota}", ex);
                throw;
            }
        }
    }
}
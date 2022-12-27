﻿using MediatR;
using SME.SERAp.Prova.Item.Infra;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Matriz.ApiSerap
{
    public class ObterMatrizPorDisciplinaIdQueryHandler : IRequestHandler<ObterMatrizPorDisciplinaIdQuery, IEnumerable<MatrizDto>>
    {
        private readonly IServicoClientApi servicoClientApi;
        private readonly IServicoLog servicoLog;

        public ObterMatrizPorDisciplinaIdQueryHandler(IServicoClientApi servicoClientApi, IServicoLog servicoLog)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public async Task<IEnumerable<MatrizDto>> Handle(ObterMatrizPorDisciplinaIdQuery request, CancellationToken cancellationToken)
        {
            var rota = $"Item/Matrizes/DisciplinaId?disciplinaId={request.DisciplinaId}";
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync(rota);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var matrizes = JsonSerializer.Deserialize<IEnumerable<MatrizDto>>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    return matrizes;

                }
                throw new Exception($"Não foi possível obter os dados, resposta da api: {response.StatusCode}.");

            }
            catch (Exception ex)
            {
                servicoLog.Registrar($"Erro ao tentar obter dados api serap -  rota: {rota}", ex);
                throw;
            }
        }

    }
}
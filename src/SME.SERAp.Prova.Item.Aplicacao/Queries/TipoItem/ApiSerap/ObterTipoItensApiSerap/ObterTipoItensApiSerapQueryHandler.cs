﻿using MediatR;
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
    public class ObterTipoItensApiSerapQueryHandler : IRequestHandler<ObterTipoItensApiSerapQuery, List<TipoItemDto>>
    {

        private readonly IServicoClientApi servicoClientApi;

        public ObterTipoItensApiSerapQueryHandler(IServicoClientApi servicoClientApi)
        {
            this.servicoClientApi = servicoClientApi ?? throw new ArgumentNullException(nameof(servicoClientApi));
        }

        public async Task<List<TipoItemDto>> Handle(ObterTipoItensApiSerapQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var client = servicoClientApi.ObterClientSerapApi();
                HttpResponseMessage response = await client.GetAsync("Item/Tipos");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var tipoItens = JsonSerializer.Deserialize<TipoItemDto[]>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    if (tipoItens != null && tipoItens.Length > 0)
                        return tipoItens.Select(a => new TipoItemDto(a.Id, a.EhPadrao, a.QuantidadeAlternativa, a.Descricao, Dominio.StatusGeral.Ativo)).ToList();
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

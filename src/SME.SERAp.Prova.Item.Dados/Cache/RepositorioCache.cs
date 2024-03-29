﻿using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Cache
{
    public class RepositorioCache : IRepositorioCache
    {
        private readonly IServicoLog servicoLog;
        private readonly IDatabase database;
        public RepositorioCache(IServicoLog servicoLog, IConnectionMultiplexer connectionMultiplexer)
        {
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
            if (connectionMultiplexer == null) throw new ArgumentNullException(nameof(connectionMultiplexer));
            this.database = connectionMultiplexer.GetDatabase();
        }

        public async Task SalvarRedisAsync(string nomeChave, object valor, int minutosParaExpirar = 720)
        {
            try
            {
                if (valor != null)
                {
                    await database.StringSetAsync(nomeChave, JsonSerializer.SerializeToUtf8Bytes(valor), TimeSpan.FromMinutes(minutosParaExpirar));
                }
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(ex);
            }
        }

        public async Task RemoverRedisAsync(string nomeChave)
        {
            try
            {
                await database.KeyDeleteAsync(nomeChave);
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(ex);
            }
        }

        public async Task<T> ObterRedisAsync<T>(string nomeChave, Func<Task<T>> buscarDados, int minutosParaExpirar = 720)
        {
            try
            {
                byte[] byteCache = await database.StringGetAsync(nomeChave);

                if (byteCache != null)
                {
                    return JsonSerializer.Deserialize<T>(byteCache);
                }

                var dados = await buscarDados();
                await SalvarRedisAsync(nomeChave, dados, minutosParaExpirar);

                return dados;
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(ex);
                return await buscarDados();
            }
        }

        public async Task<T> ObterRedisAsync<T>(string nomeChave)
        {
            try
            {
                byte[] byteCache = await database.StringGetAsync(nomeChave);

                if (byteCache != null)
                {
                    return JsonSerializer.Deserialize<T>(byteCache);
                }
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(ex);
            }

            return default;
        }
    }
}

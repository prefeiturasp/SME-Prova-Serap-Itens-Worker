using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Registry;
using RabbitMQ.Client;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Policies;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Services
{
    public class ServicoMensageria : IServicoMensageria
    {
        private readonly RabbitOptions rabbitOptions;
        private readonly IServicoTelemetria servicoTelemetria;
        private readonly IAsyncPolicy policy;
        private readonly ILogger<ServicoLog> logger;

        public ServicoMensageria(RabbitOptions rabbitOptions,
            IServicoTelemetria servicoTelemetria,
            IReadOnlyPolicyRegistry<string> registry,
            ILogger<ServicoLog> logger)
        {
            this.rabbitOptions = rabbitOptions ?? throw new ArgumentNullException(nameof(rabbitOptions));
            this.servicoTelemetria = servicoTelemetria ?? throw new ArgumentNullException(nameof(servicoTelemetria));
            policy = registry.Get<IAsyncPolicy>(PoliticaPolly.PublicaFila);
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Publicar(MensagemRabbit mensagemRabbit, string rota, string exchange, string nomeAcao)
        {
            var mensagem = JsonConvert.SerializeObject(mensagemRabbit, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var body = Encoding.UTF8.GetBytes(mensagem);

            await servicoTelemetria.RegistrarAsync(
                async () => await policy.ExecuteAsync(async () =>
                    await PublicarMensagem(rota, body, exchange)), nomeAcao, rota, string.Empty);

            return true;
        }

        private async Task PublicarMensagem(string rota, byte[] body, string exchange = null)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = rabbitOptions.HostName,
                    UserName = rabbitOptions.UserName,
                    Password = rabbitOptions.Password,
                    VirtualHost = rabbitOptions.VirtualHost
                };

                using var conexaoRabbit = await factory.CreateConnectionAsync();
                using var channel = await conexaoRabbit.CreateChannelAsync();
                var props = new BasicProperties
                {
                    Persistent = true
                };

                await channel.BasicPublishAsync(
                    ExchangeRabbit.Logs,
                    RotaRabbit.RotaLogs,
                    true,
                    props,
                    body
                );
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao publicar mensagem no RabbitMQ");
            }
        }
    }
}
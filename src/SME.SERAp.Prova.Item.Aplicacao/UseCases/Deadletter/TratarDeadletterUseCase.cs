using Polly;
using Polly.Registry;
using RabbitMQ.Client;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Fila;
using SME.SERAp.Prova.Item.Infra.Policies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class TratarDeadletterUseCase : ITratarDeadletterUseCase
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly IAsyncPolicy policy;

        public TratarDeadletterUseCase(ConnectionFactory connectionFactory, IReadOnlyPolicyRegistry<string> registry)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.policy = registry != null ? registry.Get<IAsyncPolicy>(PoliticaPolly.PublicaFila) : throw new ArgumentNullException(nameof(registry));
        }

        public async Task<bool> Executar(MensagemRabbit mensagemRabbit)
        {
            var fila = mensagemRabbit.Mensagem.ToString();

            await policy.ExecuteAsync(() => TratarMensagens(fila));

            return await Task.FromResult(true);
        }

        private async Task TratarMensagens(string fila)
        {
            using var conexaoRabbit = connectionFactory.CreateConnection();
            using IModel canal = conexaoRabbit.CreateModel();

            var props = canal.CreateBasicProperties();
            props.Persistent = true;

            while (true)
            {
                var mensagemParaEnviar = canal.BasicGet($"{fila}.deadletter", true);

                if (mensagemParaEnviar == null)
                    break;

                mensagemParaEnviar.BasicProperties.Headers.TryGetValue("x-retry", out object qntMensagem);
                var qntAtual = qntMensagem != null ? (int)qntMensagem : 0;

                if (qntAtual == 2)
                {
                    await Task.Run(() => canal.BasicPublish(ExchangeRabbit.SerapEstudanteItemDeadLetter, $"{fila}.deadletter.final", mensagemParaEnviar.BasicProperties, mensagemParaEnviar.Body.ToArray()));
                }
                else
                {
                    qntAtual += 1;
                    IBasicProperties basicProperties = canal.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    basicProperties.Headers = new Dictionary<string, object>();
                    basicProperties.Headers.Add("x-retry", qntAtual);

                    await Task.Run(() => canal.BasicPublish(ExchangeRabbit.SerapEstudanteItem, fila, basicProperties, mensagemParaEnviar.Body.ToArray()));
                }
            }
        }
    }
}

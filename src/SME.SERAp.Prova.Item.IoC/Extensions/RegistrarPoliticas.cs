using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SME.SERAp.Prova.Item.Infra.Policies;

namespace SME.SERAp.Prova.Item.IoC.Extensions
{
    public static class RegistrarPoliticas
    {
        public static void AddPoliticas(this IServiceCollection services)
        {
            var registry = services.AddPolicyRegistry();

            Random jitterer = new();
            var policyFila = Policy.Handle<Exception>()
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                    + TimeSpan.FromMilliseconds(jitterer.Next(0, 30)));

            registry.Add(PoliticaPolly.PublicaFila, policyFila);
        }        
    }
}
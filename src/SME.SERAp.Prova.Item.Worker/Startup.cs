using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using RabbitMQ.Client;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using SME.SERAp.Prova.Item.IoC;
using System;

namespace SME.SERAp.Prova.Item.Worker
{
    internal class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigEnvoiromentVariables(services);
            RegistraDependencias.Registrar(services);

            services.AddHostedService<WorkerRabbit>();
        }

        private void ConfigEnvoiromentVariables(IServiceCollection services)
        {
            var connectionStringOptions = new ConnectionStringOptions();
            Configuration.GetSection(ConnectionStringOptions.Secao).Bind(connectionStringOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(connectionStringOptions);

            var rabbitOptions = new RabbitOptions();
            Configuration.GetSection(RabbitOptions.Secao).Bind(rabbitOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitOptions);

            var factory = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            services.AddSingleton(factory);

            var conexaoRabbit = factory.CreateConnection();
            IModel channel = conexaoRabbit.CreateModel();
            services.AddSingleton(channel);

            var rabbitLogOptions = new RabbitLogOptions();
            Configuration.GetSection(RabbitLogOptions.Secao).Bind(rabbitLogOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitLogOptions);

            var factoryLog = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            var conexaoRabbitLog = factoryLog.CreateConnection();
            IModel channelLog = conexaoRabbitLog.CreateModel();

            var telemetriaOptions = new TelemetriaOptions();
            Configuration.GetSection(TelemetriaOptions.Secao).Bind(telemetriaOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(telemetriaOptions);

            var servicoTelemetria = new ServicoTelemetria(telemetriaOptions);
            services.AddSingleton<IServicoTelemetria>(servicoTelemetria);
            DapperExtensionMethods.Init(servicoTelemetria);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElasticApm(Configuration,
              new SqlClientDiagnosticSubscriber(),
              new HttpDiagnosticsSubscriber());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("workerrabbitmq!");
            });
        }
    }
}
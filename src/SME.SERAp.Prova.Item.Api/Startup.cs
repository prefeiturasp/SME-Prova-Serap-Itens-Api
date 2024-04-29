using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using Elastic.Apm.StackExchange.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using SME.SERAp.Prova.Item.Api.Configurations;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using SME.SERAp.Prova.Item.IoC;
using StackExchange.Redis;

namespace SME.SERAp.Prova.Item.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            ConfigEnvoiromentVariables(services);

            RegistraDependencias.Registrar(services, Configuration);
            RegistraAutenticacao.Registrar(services, Configuration);
            RegistraDocumentacaoSwagger.Registrar(services);
            RegistraMvc.Registrar(services);
        }

        private void ConfigEnvoiromentVariables(IServiceCollection services)
        {
            var conexaoDadosVariaveis = new ConnectionStringOptions();
            Configuration.GetSection(ConnectionStringOptions.Secao).Bind(conexaoDadosVariaveis, c => c.BindNonPublicProperties = true);
            services.AddSingleton(conexaoDadosVariaveis);

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

            var redisOptions = new RedisOptions();
            Configuration.GetSection(RedisOptions.Secao).Bind(redisOptions, c => c.BindNonPublicProperties = true);

            var redisConfigurationOptions = new ConfigurationOptions()
            {
                Proxy = redisOptions.Proxy,
                SyncTimeout = redisOptions.SyncTimeout,
                EndPoints = { redisOptions.Endpoint }
            };
            var muxer = ConnectionMultiplexer.Connect(redisConfigurationOptions);
            services.AddSingleton<IConnectionMultiplexer>(muxer);

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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SME.SERAp.Prova.Item.Api v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

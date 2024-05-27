using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SME.SERAp.Prova.Item.Dominio.Constantes;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Item.IoC.Extensions
{
    internal static class RegistrarEnvironmentVariablesExtension
    {
        private static void AddOptions<TOptions>(IServiceCollection services, IConfiguration configuration, string secao) where TOptions : class
        {
            services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(secao));

            services.AddSingleton<TOptions>();        
        }
        
        private static void AddClientApi(IServiceCollection services, IConfiguration configuration)
        {
            AddOptions<ClientApiOptions>(services, configuration, SecoesOptions.ClientApi);        
        }
        
        internal static void RegistrarEnvironmentVariables(this IServiceCollection services, IConfiguration configuration)
        {
            AddClientApi(services, configuration);
        }        
    }
}
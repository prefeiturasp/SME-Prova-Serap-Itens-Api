using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SME.SERAp.Prova.Item.Dominio.Constantes;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Exceptions;

namespace SME.SERAp.Prova.Item.IoC.Extensions
{
    internal static class RegistrarHttpClientExtension
    {
        private static void RegistrarHttpClientSerapApi(IServiceCollection services)
        {
            services.AddHttpClient(SerapConstants.ApiSerap, client =>
            {
                var clientApi = GetClientApiSerap(services);
            
                if (string.IsNullOrEmpty(clientApi.Item1))
                    throw new ErroException("Endereço base de comunicação com a API SERAp não localizado.");
            
                if (string.IsNullOrEmpty(clientApi.Item2))
                    throw new ErroException("Nome da chave não localizada.");            

                if (string.IsNullOrEmpty(clientApi.Item3))
                    throw new ErroException("Valor da chave não localizada.");            
            
                client.BaseAddress = new Uri(clientApi.Item1);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add(clientApi.Item2, clientApi.Item3);
            });        
        }
        
        private static (string, string, string) GetClientApiSerap(IServiceCollection services)
        {
            var resultado = (UrlBase: string.Empty, NomeChave: string.Empty, ValorChave: string.Empty);
            var serviceProvider = services.BuildServiceProvider();
            var clientApiOptions = serviceProvider.GetRequiredService<IOptions<ClientApiOptions>>().Value;

            resultado.UrlBase = clientApiOptions.SerapApiUrlBase;
            resultado.NomeChave = clientApiOptions.SerapApiNomeChave;
            resultado.ValorChave = clientApiOptions.SerapApiValorChave;

            return resultado;
        }
        
        internal static void RegistrarHttpClient(this IServiceCollection services)
        {
            RegistrarHttpClientSerapApi(services);
        }        
    }
}
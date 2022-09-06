using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SME.SERAp.Prova.Item.IoC.Extensions
{
    public static class RegistrarFluentValidation
    {
        public static void AdicionarValidadoresFluentValidation(this IServiceCollection services)
        {
            var assemblyInfra = AppDomain.CurrentDomain.Load("SME.SERAp.Prova.Item.Infra");

            AssemblyScanner
                .FindValidatorsInAssembly(assemblyInfra)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            var assembly = AppDomain.CurrentDomain.Load("SME.SERAp.Prova.Item.Aplicacao");

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));
        }
    }
}

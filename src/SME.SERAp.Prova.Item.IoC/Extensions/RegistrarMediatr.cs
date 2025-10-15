using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SME.SERAp.Prova.Item.Aplicacao;
using System;

namespace SME.SERAp.Prova.Item.IoC.Extensions
{
    public static class RegistraMediatr
    {
        public static void AdicionarMediatr(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SME.SERAp.Prova.Item.Aplicacao");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidacoesPipeline<,>));
        }
    }
}

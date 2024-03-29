﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SME.SERAp.Prova.Item.Api.Filters;
using SME.SERAp.Prova.Item.Infra.Interfaces;

namespace SME.SERAp.Prova.Item.Api.Configurations
{
    public static class RegistraMvc
    {
        public static void Registrar(IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            var sp = services.BuildServiceProvider();
            var servicoLog = sp.GetService<IServicoLog>();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = true;
                options.Filters.Add(new FiltroExcecoesAttribute(servicoLog));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
    }
}

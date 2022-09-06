﻿using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dados.Cache;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dados.Mappings;
using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using SME.SERAp.Prova.Item.IoC.Extensions;

namespace SME.SERAp.Prova.Item.IoC
{
    public static class RegistraDependencias
    {
        public static void Registrar(IServiceCollection services)
        {
            services.AdicionarMediatr();
            services.AdicionarValidadoresFluentValidation();
            RegistrarServicos(services);
            RegistrarRepositorios(services);
            RegistrarCasosDeUso(services);
            RegistraMapeamentos();
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.TryAddSingleton<IServicoLog, ServicoLog>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioTeste, RepositorioTeste>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IInserirTesteUseCase, InserirTesteUseCase>();
            services.AddScoped<IObterTodosTesteUseCase, ObterTodosTesteUseCase>();
        }

        private static void RegistraMapeamentos()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new TesteMap());
                config.ForDommel();
            });
        }
    }
}

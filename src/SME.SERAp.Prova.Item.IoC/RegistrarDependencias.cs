using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Item.Aplicacao;
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
            services.AddScoped<IRepositorioMatriz, RepositorioMatriz>();
            services.AddScoped<IRepositorioDisciplina, RepositorioDisciplina>();
            services.AddScoped<IRepositorioAreaConhecimento, RepositorioAreaConhecimento>();
            services.AddScoped<IRepositorioItem, RepositorioItem>();
            services.AddScoped<IRepositoSequencialItem, RepositorioSequencialItem>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IInserirTesteUseCase, InserirTesteUseCase>();
            services.AddScoped<IObterAreasConhecimentoUseCase, ObterAreasConhecimentoUseCase>();
            services.AddScoped<IObterDisciplinasPorAreaConhecimento, ObterDisciplinasPorAreaConhecimentoUseCase>();
            services.AddScoped<IObterMatrizesPorDisciplinaUseCase, ObterMatrizesPorDisciplinaUseCase>();
            services.AddScoped<IRepositorioItem, RepositorioItem>();
            services.AddScoped<IRepositoSequencialItem, RepositorioSequencialItem>();
            services.AddScoped<IInserirTesteUseCase, InserirTesteUseCase>();
            services.AddScoped<ISalvarRascunhoItemUseCase, SalvarRascunhoItemUseCase>();
        }

        private static void RegistraMapeamentos()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new TesteMap());
                config.AddMap(new AreaConhecimentoMap());
                config.AddMap(new DisciplinaMap());
                config.AddMap(new MatrizMap());
                config.AddMap(new ItemMap());
                config.AddMap(new SequencialMap());
                config.ForDommel();
            });
        }
    }
}

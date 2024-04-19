using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Cache;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dados.Mappings;
using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio.Entities;
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
            services.RegistrarHttpClient();
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.TryAddSingleton<IServicoLog, ServicoLog>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioMatriz, RepositorioMatriz>();
            services.AddScoped<IRepositorioDisciplina, RepositorioDisciplina>();
            services.AddScoped<IRepositorioAreaConhecimento, RepositorioAreaConhecimento>();
            services.AddScoped<IRepositorioCache, RepositorioCache>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioItem, RepositorioItem>();
            services.AddScoped<IRepositoSequencialItem, RepositorioSequencialItem>();
            services.AddScoped<IRepositorioDificuldade, RepositorioDificuldade>();
            services.AddScoped<IRepositorioCompetencia, RepositorioCompetencia>();
            services.AddScoped<IRepositorioHabilidade, RepositorioHabilidade>();
            services.AddScoped<IRepositorioTipoGrade, RepositorioTipoGrade>();
            services.AddScoped<IRepositorioAssunto, RepositorioAssunto>();
            services.AddScoped<IRepositorioSubAssunto, RepositorioSubAssunto>();
            services.AddScoped<IRepositorioQuantidadeAlternativas, RepositorioQuantidadeAlternativas>();
            services.AddScoped<IRepositorioAlternativa, RepositorioAlternativa>();
            services.AddScoped<IRepositorioArquivo, RepositorioArquivo>();
            services.AddScoped<IRepositorioItemAudio, RepositorioItemAudio>();
            services.AddScoped<IRepositorioItemVideo, RepositorioItemVideo>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IObterAreasConhecimentoUseCase, ObterAreasConhecimentoUseCase>();
            services.AddScoped<IObterDisciplinasPorAreaConhecimentoUseCase, ObterDisciplinasPorAreaConhecimentoUseCase>();
            services.AddScoped<IObterMatrizesPorDisciplinaUseCase, ObterMatrizesPorDisciplinaUseCase>();
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<IAutenticacaoValidarUseCase, AutenticacaoValidarUseCase>();
            services.AddScoped<IAutenticacaoRevalidarUseCase, AutenticacaoRevalidarUseCase>();
            services.AddScoped<ISalvarRascunhoItemUseCase, SalvarRascunhoItemUseCase>();
            services.AddScoped<ISalvarItemUseCase, SalvarItemUseCase>();
            services.AddScoped<IObterDisciplinaCompletaPorIdUseCase, ObterDisciplinaCompletaPorIdUseCase>();
            services.AddScoped<IObterMatrizPorIdUseCase, ObterMatrizPorIdUseCase>();
            services.AddScoped<IObterItemPorIdUseCase, ObterItemPorIdUseCase>();
            services.AddScoped<IObterDificuldadesUseCase, ObterDificuldadesUseCase>();
            services.AddScoped<IObterCompetenciasPorMatrizIdUseCase, ObterCompetenciasPorMatrizIdUseCase>();
            services.AddScoped<IObterHabilidadesPorCompetenciaIdUseCase, ObterHabilidadesPorCompetenciaIdUseCase>();
            services.AddScoped<IObterTiposGradePorMatrizIdUseCase, ObterTiposGradePorMatrizIdUseCase>();
            services.AddScoped<IObterAssuntosUseCase, ObterAssuntosUseCase>();
            services.AddScoped<IObterSubAssuntosPorAssuntoIdUseCase, ObterSubAssuntosPorAssuntoIdUseCase>();
            services.AddScoped<IObterQuantidadesAlternativasUseCase, ObterQuantidadesAlternativasUseCase>();
            services.AddScoped<IObterTiposItemUseCase, ObterTiposItemUseCase>();
            services.AddScoped<IObterSituacoesItemUseCase, ObterSituacoesItemUseCase>();

            services.AddScoped<IUploadArquivoUseCase, UploadArquivoUseCase>();
        }

        private static void RegistraMapeamentos()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new AreaConhecimentoMap());
                config.AddMap(new DisciplinaMap());
                config.AddMap(new MatrizMap());
                config.AddMap(new UsuarioMap());
                config.AddMap(new ItemMap());
                config.AddMap(new SequencialMap());
                config.AddMap(new DificuldadeMap());
                config.AddMap(new CompetenciaMap());
                config.AddMap(new HabilidadeMap());
                config.AddMap(new TipoGradeMap());
                config.AddMap(new AssuntoMap());
                config.AddMap(new SubAssuntoMap());
                config.AddMap(new QuantidadeAlternativaMap());
                config.AddMap(new AlternativaMap());
                config.AddMap(new ArquivoMap());
                config.AddMap(new ItemVideoMap());
                config.AddMap(new ItemAudioMap());
                config.ForDommel();
            });
        }
    }
}

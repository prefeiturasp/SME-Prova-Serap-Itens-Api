using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Aplicacao.UseCases.Arquivo;
using SME.SERAp.Prova.Item.Aplicacao.UseCases.Item;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Dados.Cache;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using SME.SERAp.Prova.Item.IoC.Extensions;

namespace SME.SERAp.Prova.Item.IoC
{
    public static class RegistraDependencias
    {
        public static void Registrar(IServiceCollection services, IConfiguration configuration)
        {
            services.RegistrarEnvironmentVariables(configuration);
            services.AdicionarMediatr();
            services.AdicionarValidadoresFluentValidation();
            RegistrarServicos(services);
            RegistrarRepositorios(services);
            RegistrarCasosDeUso(services);
            DapperConfig.RegistrarMapeamentos();
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
            services.AddScoped<IRepositorioNivelItem, RepositorioNivelItem>();
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
            services.AddScoped<IObterItemComAlternativaPorIdUseCase, ObterItemComAlternativaPorIdUseCase>();
            services.AddScoped<IObterDificuldadesUseCase, ObterDificuldadesUseCase>();
            services.AddScoped<IObterCompetenciasPorMatrizIdUseCase, ObterCompetenciasPorMatrizIdUseCase>();
            services.AddScoped<IObterHabilidadesPorCompetenciaIdUseCase, ObterHabilidadesPorCompetenciaIdUseCase>();
            services.AddScoped<IObterTiposGradePorMatrizIdUseCase, ObterTiposGradePorMatrizIdUseCase>();
            services.AddScoped<IObterAssuntosUseCase, ObterAssuntosUseCase>();
            services.AddScoped<IObterSubAssuntosPorAssuntoIdUseCase, ObterSubAssuntosPorAssuntoIdUseCase>();
            services.AddScoped<IObterQuantidadesAlternativasUseCase, ObterQuantidadesAlternativasUseCase>();
            services.AddScoped<IObterTiposItemUseCase, ObterTiposItemUseCase>();
            services.AddScoped<IObterSituacoesItemUseCase, ObterSituacoesItemUseCase>();
            services.AddScoped<IObterNivelItemUseCase, ObterNivelItemUseCase>();
            services.AddScoped<IUploadArquivoUseCase, UploadArquivoUseCase>();
            services.AddScoped<IObterCodigosItensUseCase, ObterCodigosItensUseCase>();
            services.AddScoped<IObterListaItemsUseCase, ObterListaItemsUseCase>();
            services.AddScoped<IObterAudioVideoPorItemIdUseCase, ObterAudioVideoPorItemIdUseCase>();
            services.AddScoped<IUploadArquivoUseCase, UploadArquivoUseCase>();
            services.AddScoped<IObterItemResumoPorIdUseCase, ObterItemResumoPorIdUseCase>();
            services.AddScoped<IUploadArquivoAudioVideo, UploadArquivoAudioVideo>();
        }
    }
}
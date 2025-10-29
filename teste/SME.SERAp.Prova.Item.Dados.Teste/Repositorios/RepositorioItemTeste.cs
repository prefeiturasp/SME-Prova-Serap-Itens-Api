using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Dapper;
using Nest;
using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using System;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Item.Dados.Teste.Repositorios
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    internal class RepositorioItemFake : RepositorioItem
    {
        private readonly IDbConnection _conexao;
        public RepositorioItemFake(ConnectionStringOptions options, IDbConnection conexao) : base(options)
        {
            _conexao = conexao;
        }

        protected override IDbConnection ObterConexao() => _conexao; 
    }

    [Collection("ColecaoMapeamentos")]
    public class RepositorioItemTeste
    {
        private readonly Mock<IDbConnection> conexaoDbMock;
        private readonly RepositorioItemFake repositorio;
        private const long ItemIdValido = 100L;
        private const string CodigoItemValido = "IT-001";
        
        public RepositorioItemTeste()
        {
            conexaoDbMock = new Mock<IDbConnection>();
            repositorio = new RepositorioItemFake(new ConnectionStringOptions(), conexaoDbMock.Object);
        }

        private DominioItem CriarItemMock()
        {
            return new DominioItem
            { 
                Id = ItemIdValido, 
                CodigoItem = CodigoItemValido,
                AreaconhecimentoId = 22,
                DisciplinaId = 65,
                VersaoItem = 1,
                DataCriacao = DateTime.Now,
                Enunciado = "Teste Enunciado",
            };
        }

        [Fact(DisplayName = "Deve retornar o Item ao buscar por Id")]
        public async Task ObterPorId_Deve_Retornar_Item()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            var itemEsperado = CriarItemMock();

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<DominioItem>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(itemEsperado);

            var resultado = await repositorio.ObterPorId(ItemIdValido);

            Assert.NotNull(resultado);
            Assert.Equal(ItemIdValido, resultado.Id);
            Assert.Equal(CodigoItemValido, resultado.CodigoItem);

            conexaoDbMock.Verify(c => c.Close(), Times.Once); 
        }

        [Fact(DisplayName = "Deve retornar null se não encontrar o Item por Id")]
        public async Task ObterPorId_Deve_Retornar_Nulo()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            DominioItem itemNulo = null;

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<DominioItem>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(itemNulo);

            var resultado = await repositorio.ObterPorId(ItemIdValido);

            Assert.Null(resultado);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar o maior ID quando a consulta for bem-sucedida")]
        public async Task ObterMaiorValorId_Deve_Retornar_Valor()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            long? maxIdEsperado = 5000L;

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<long?>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(maxIdEsperado);

            var resultado = await repositorio.ObterMaiorValorId();

            Assert.Equal(maxIdEsperado, resultado);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar null quando a tabela estiver vazia (MAX retorna null)")]
        public async Task ObterMaiorValorId_Deve_Retornar_Nulo()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            long? maxIdNulo = null;

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<long?>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(maxIdNulo);

            var resultado = await repositorio.ObterMaiorValorId();

            Assert.Null(resultado);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar a última versão do Item por Código")]
        public async Task ObterUltimaVersaoItemPorCodigo_Deve_Retornar_Item()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            var itemEsperado = CriarItemMock();
            itemEsperado.VersaoItem = 5;

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<DominioItem>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(itemEsperado);

            var resultado = await repositorio.ObterUltimaVersaoItemPorCodigo(CodigoItemValido);

            Assert.NotNull(resultado);
            Assert.Equal(CodigoItemValido, resultado.CodigoItem);
            Assert.Equal(5, resultado.VersaoItem);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }

        [Fact(DisplayName = "Deve propagar exceção com mensagem customizada se houver falha no DB")]
        public async Task ObterUltimaVersaoItemPorCodigo_Deve_Propagar_Excecao()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            var dbException = new InvalidOperationException("Erro de conexão simulado.");

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<DominioItem>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ThrowsAsync(dbException);

            var excecaoCapturada = await Assert.ThrowsAsync<Exception>(() =>
                repositorio.ObterUltimaVersaoItemPorCodigo(CodigoItemValido));

            Assert.Contains($"Erro ao obter a última versão do item pelo Código {CodigoItemValido}.", excecaoCapturada.Message);
            Assert.Equal(dbException, excecaoCapturada.InnerException);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }

        [Fact(DisplayName = "Deve retornar a quantidade de itens por Área de Conhecimento e Disciplina")]
        public async Task ObterQtdItensAreaConhecimentoEhDisciplina_Deve_Retornar_Quantidade()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            const long areaId = 1L;
            const long disciplinaId = 2L;
            long? quantidadeEsperada = 42L;

            var resultadoMockado = new List<long?> { quantidadeEsperada };

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<long?>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(resultadoMockado);

            var resultado = await repositorio.ObterQtdItensAreaConhecimentoEhDisciplina(areaId, disciplinaId);

            Assert.Equal(quantidadeEsperada, resultado);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }


        [Fact(DisplayName = "Deve retornar 0 se não houver itens para a Área de Conhecimento e Disciplina")]
        public async Task ObterQtdItensAreaConhecimentoEhDisciplina_Deve_Retornar_Zero()
        {
            var servicoTelemetria = new Mock<IServicoTelemetria>();
            DapperExtensionMethods.Init(servicoTelemetria.Object);

            const long areaId = 99L;
            const long disciplinaId = 98L;
            long? quantidadeEsperada = 0L;

            var resultadoMockado = new List<long?> { quantidadeEsperada };

            servicoTelemetria
                .Setup(c => c.RegistrarComRetornoAsync<long?>(
                    It.IsAny<Func<Task<object>>>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .ReturnsAsync(resultadoMockado);

            var resultado = await repositorio.ObterQtdItensAreaConhecimentoEhDisciplina(areaId, disciplinaId);

            Assert.Equal(0, resultado);
            conexaoDbMock.Verify(c => c.Close(), Times.Once);
        }
    }
}
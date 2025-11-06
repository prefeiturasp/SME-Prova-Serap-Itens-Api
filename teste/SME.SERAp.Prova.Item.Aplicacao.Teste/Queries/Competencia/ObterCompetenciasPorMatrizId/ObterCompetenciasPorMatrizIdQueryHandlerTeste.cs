using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Competencia
{
    public class ObterCompetenciasPorMatrizIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioCompetencia> repositorio;
        private readonly ObterCompetenciasPorMatrizIdQueryHandler handler;

        public ObterCompetenciasPorMatrizIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioCompetencia>();
            handler = new ObterCompetenciasPorMatrizIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterCompetenciasPorMatrizIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Competencias_Quando_Encontradas()
        {
            var competenciasEsperadas = new List<RetornoCompetenciaDto>
        {
            new RetornoCompetenciaDto { Id = 1, Descricao = "Competência 1" },
            new RetornoCompetenciaDto { Id = 2, Descricao = "Competência 2" },
            new RetornoCompetenciaDto { Id = 3, Descricao = "Competência 3" }
        };

            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(1))
                .ReturnsAsync(competenciasEsperadas);

            var query = new ObterCompetenciasPorMatrizIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal(3, listaResultado.Count);
            Assert.Equal(competenciasEsperadas[0].Id, listaResultado[0].Id);
            Assert.Equal(competenciasEsperadas[0].Descricao, listaResultado[0].Descricao);
            Assert.Equal(competenciasEsperadas[1].Id, listaResultado[1].Id);
            Assert.Equal(competenciasEsperadas[2].Id, listaResultado[2].Id);
            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Competencias()
        {
            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(5))
                .ReturnsAsync(new List<RetornoCompetenciaDto>());

            var query = new ObterCompetenciasPorMatrizIdQuery(5);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(5), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Uma_Competencia_Quando_Houver_Apenas_Uma()
        {
            var competenciasEsperadas = new List<RetornoCompetenciaDto>
        {
            new RetornoCompetenciaDto { Id = 1, Descricao = "Competência Única" }
        };

            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(2))
                .ReturnsAsync(competenciasEsperadas);

            var query = new ObterCompetenciasPorMatrizIdQuery(2);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal(1, listaResultado[0].Id);
            Assert.Equal("Competência Única", listaResultado[0].Descricao);
            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(2), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_MatrizId_Correto()
        {
            var competenciasEsperadas = new List<RetornoCompetenciaDto>
        {
            new RetornoCompetenciaDto { Id = 1, Descricao = "Competência Teste" }
        };

            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(10))
                .ReturnsAsync(competenciasEsperadas);

            var query = new ObterCompetenciasPorMatrizIdQuery(10);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(10), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async Task Deve_Passar_MatrizId_Correto_Para_Repositorio(long matrizId)
        {
            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(matrizId))
                .ReturnsAsync(new List<RetornoCompetenciaDto>());

            var query = new ObterCompetenciasPorMatrizIdQuery(matrizId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(matrizId), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterCompetenciasPorMatrizIdQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Retornar_Null()
        {
            repositorio
                .Setup(r => r.ObterCompetenciasPorMatrizId(99))
                .ReturnsAsync((IEnumerable<RetornoCompetenciaDto>)null);

            var query = new ObterCompetenciasPorMatrizIdQuery(99);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterCompetenciasPorMatrizId(99), Times.Once);
        }
    }
}

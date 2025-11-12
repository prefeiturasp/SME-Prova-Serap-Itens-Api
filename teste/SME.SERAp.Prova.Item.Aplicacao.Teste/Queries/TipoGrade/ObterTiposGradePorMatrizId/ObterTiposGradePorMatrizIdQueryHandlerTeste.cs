using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.TipoGrade
{
    public class ObterTiposGradePorMatrizIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioTipoGrade> repositorioTipoGrade;
        private readonly ObterTiposGradePorMatrizIdQueryHandler handler;

        public ObterTiposGradePorMatrizIdQueryHandlerTeste()
        {
            repositorioTipoGrade = new Mock<IRepositorioTipoGrade>();
            handler = new ObterTiposGradePorMatrizIdQueryHandler(repositorioTipoGrade.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterTiposGradePorMatrizIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_TiposGrade_Quando_Encontrados()
        {
            var matrizId = 10;
            var tiposGradeEsperados = new List<RetornoTipoGradeDto>
            {
                new RetornoTipoGradeDto { Id = 1, Descricao = "Grade A" },
                new RetornoTipoGradeDto { Id = 2, Descricao = "Grade B" }
            };

            repositorioTipoGrade
                .Setup(r => r.ObterTiposGradePorMatrizId(matrizId))
                .ReturnsAsync(tiposGradeEsperados);

            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Collection(resultado,
                item => Assert.Equal("Grade A", item.Descricao),
                item => Assert.Equal("Grade B", item.Descricao));

            repositorioTipoGrade.Verify(r => r.ObterTiposGradePorMatrizId(matrizId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_TiposGrade()
        {
            var matrizId = 50;
            repositorioTipoGrade
                .Setup(r => r.ObterTiposGradePorMatrizId(matrizId))
                .ReturnsAsync(new List<RetornoTipoGradeDto>());

            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioTipoGrade.Verify(r => r.ObterTiposGradePorMatrizId(matrizId), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            var matrizId = 99;
            repositorioTipoGrade
                .Setup(r => r.ObterTiposGradePorMatrizId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Erro no repositório"));

            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no repositório", excecao.Message);
            repositorioTipoGrade.Verify(r => r.ObterTiposGradePorMatrizId(matrizId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task Deve_Chamar_Repositorio_Com_MatrizId_Correto(long matrizId)
        {
            var tiposGrade = new List<RetornoTipoGradeDto> { new RetornoTipoGradeDto { Id = 1, Descricao = "Tipo X" } };

            repositorioTipoGrade
                .Setup(r => r.ObterTiposGradePorMatrizId(matrizId))
                .ReturnsAsync(tiposGrade);

            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);
            await handler.Handle(query, CancellationToken.None);

            repositorioTipoGrade.Verify(r => r.ObterTiposGradePorMatrizId(matrizId), Times.Once);
        }

        [Fact]
        public async Task Deve_Permitir_MatrizId_Igual_A_Zero_Sem_Erro()
        {
            repositorioTipoGrade
                .Setup(r => r.ObterTiposGradePorMatrizId(0))
                .ReturnsAsync(new List<RetornoTipoGradeDto>());

            var query = new ObterTiposGradePorMatrizIdQuery(0);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioTipoGrade.Verify(r => r.ObterTiposGradePorMatrizId(0), Times.Once);
        }
    }
}

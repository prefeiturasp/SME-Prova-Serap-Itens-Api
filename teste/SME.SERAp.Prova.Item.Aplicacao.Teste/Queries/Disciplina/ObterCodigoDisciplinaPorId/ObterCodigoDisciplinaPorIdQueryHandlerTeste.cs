using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Disciplina
{
    public class ObterCodigoDisciplinaPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioDisciplina> repositorio;
        private readonly ObterCodigoDisciplinaPorIdQueryHandler handler;

        public ObterCodigoDisciplinaPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioDisciplina>();
            handler = new ObterCodigoDisciplinaPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterCodigoDisciplinaPorIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Codigo_Quando_Encontrado()
        {
            var codigoEsperado = 456L;
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(1))
                .ReturnsAsync(codigoEsperado);

            var query = new ObterCodigoDisciplinaPorIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(codigoEsperado, resultado);
            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Zero_Quando_Nao_Encontrar_Codigo()
        {
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(99))
                .ReturnsAsync(0L);

            var query = new ObterCodigoDisciplinaPorIdQuery(99);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(0L, resultado);
            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(99), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterCodigoDisciplinaPorIdQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(It.IsAny<long>()), Times.Once);
        }

        [Theory]
        [InlineData(1, 100L)]
        [InlineData(5, 500L)]
        [InlineData(10, 1000L)]
        [InlineData(100, 10000L)]
        public async Task Deve_Chamar_Repositorio_Com_Id_Correto_E_Retornar_Codigo(long id, long codigoEsperado)
        {
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(id))
                .ReturnsAsync(codigoEsperado);

            var query = new ObterCodigoDisciplinaPorIdQuery(id);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(codigoEsperado, resultado);
            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(id), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Codigo_Positivo_Quando_Disciplina_Existir()
        {
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(3))
                .ReturnsAsync(789L);

            var query = new ObterCodigoDisciplinaPorIdQuery(3);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.True(resultado > 0);
            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(3), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            repositorio
                .Setup(r => r.ObterCodigoDisciplinaPorId(2))
                .ReturnsAsync(200L);

            var query = new ObterCodigoDisciplinaPorIdQuery(2);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterCodigoDisciplinaPorId(2), Times.Once);
        }
    }
}

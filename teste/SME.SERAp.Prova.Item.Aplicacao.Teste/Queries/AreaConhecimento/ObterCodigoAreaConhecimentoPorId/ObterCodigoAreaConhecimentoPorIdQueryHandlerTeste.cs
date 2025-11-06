using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.AreaConhecimento
{
    public class ObterCodigoAreaConhecimentoPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAreaConhecimento> repositorio;
        private readonly ObterCodigoAreaConhecimentoPorIdQueryHandler handler;

        public ObterCodigoAreaConhecimentoPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAreaConhecimento>();
            handler = new ObterCodigoAreaConhecimentoPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterCodigoAreaConhecimentoPorIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Codigo_Quando_Encontrado()
        {
            var codigoEsperado = 123L;
            repositorio
                .Setup(r => r.ObterCodigoAreaConhecimentoPorId(1))
                .ReturnsAsync(codigoEsperado);

            var query = new ObterCodigoAreaConhecimentoPorIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(codigoEsperado, resultado);
            repositorio.Verify(r => r.ObterCodigoAreaConhecimentoPorId(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Zero_Quando_Nao_Encontrar_Codigo()
        {
            repositorio
                .Setup(r => r.ObterCodigoAreaConhecimentoPorId(99))
                .ReturnsAsync(0L);

            var query = new ObterCodigoAreaConhecimentoPorIdQuery(99);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(0L, resultado);
            repositorio.Verify(r => r.ObterCodigoAreaConhecimentoPorId(99), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterCodigoAreaConhecimentoPorId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterCodigoAreaConhecimentoPorIdQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterCodigoAreaConhecimentoPorId(It.IsAny<long>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async Task Deve_Chamar_Repositorio_Com_Id_Correto(long id)
        {
            repositorio
                .Setup(r => r.ObterCodigoAreaConhecimentoPorId(id))
                .ReturnsAsync(id * 10);

            var query = new ObterCodigoAreaConhecimentoPorIdQuery(id);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterCodigoAreaConhecimentoPorId(id), Times.Once);
        }
    }
}

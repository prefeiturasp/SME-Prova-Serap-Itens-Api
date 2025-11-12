using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterMaiorValorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterMaiorValorIdQueryHandler handler;

        public ObterMaiorValorIdQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterMaiorValorIdQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Maior_Valor_Id_Quando_Repositorio_Retornar_Dado()
        {
            long? maiorValorEsperado = 999;
            var query = new ObterMaiorValorIdQuery();

            repositorioItemMock.Setup(r => r.ObterMaiorValorId())
                               .ReturnsAsync(maiorValorEsperado);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(maiorValorEsperado, resultado);

            repositorioItemMock.Verify(r => r.ObterMaiorValorId(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Nao_Tiver_Dados()
        {
            long? valorNulo = null;
            var query = new ObterMaiorValorIdQuery();

            repositorioItemMock.Setup(r => r.ObterMaiorValorId())
                               .ReturnsAsync(valorNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);

            repositorioItemMock.Verify(r => r.ObterMaiorValorId(), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterMaiorValorIdQuery();
            var excecaoEsperada = new InvalidOperationException("Falha no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterMaiorValorId())
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);
            repositorioItemMock.Verify(r => r.ObterMaiorValorId(), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterMaiorValorIdQueryHandler(null));
        }
    }
}

using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterItemPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterItemPorIdQueryHandler handler;
        private const long ItemIdValido = 123;

        public ObterItemPorIdQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterItemPorIdQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Item_Quando_Repositorio_Retornar_Dados()
        {
            var itemEsperado = new Dominio.Entities.Item
            {
                Id = ItemIdValido,
                 Enunciado = "Item de Teste"
            };

            var query = new ObterItemPorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterPorIdAsync(ItemIdValido))
                               .ReturnsAsync(itemEsperado);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(ItemIdValido, resultado.Id);
            Assert.Equal("Item de Teste", resultado.Enunciado);

            repositorioItemMock.Verify(r => r.ObterPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Nao_Encontrar_Item()
        {
            Dominio.Entities.Item itemNulo = null;
            var query = new ObterItemPorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterPorIdAsync(ItemIdValido))
                               .ReturnsAsync(itemNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioItemMock.Verify(r => r.ObterPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterItemPorIdQuery(ItemIdValido);
            var excecaoEsperada = new InvalidOperationException("Falha no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterPorIdAsync(ItemIdValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);
            repositorioItemMock.Verify(r => r.ObterPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterItemPorIdQueryHandler(null));
        }
    }
}

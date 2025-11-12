using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterItemComAlternativaPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterItemComAlternativaPorIdQueryHandler handler;
        private const long ItemIdValido = 123;

        public ObterItemComAlternativaPorIdQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterItemComAlternativaPorIdQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Item_Quando_Repositorio_Retornar_Dados()
        {
            var itemEsperado = new Dominio.Entities.Item
            {
                Id = ItemIdValido,
                Enunciado = "Item de Teste",
                Alternativas = new List<Alternativa>
                {
                    new Dominio.Entities.Alternativa { Id = 1, Descricao = "Alternativa A" },
                    new Dominio.Entities.Alternativa { Id = 2, Descricao = "Alternativa B" }
                }
            };

            var query = new ObterItemComAlternativaPorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterComAlternativaPorIdAsync(ItemIdValido))
                               .ReturnsAsync(itemEsperado);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(ItemIdValido, resultado.Id);
            Assert.Equal("Item de Teste", resultado.Enunciado);
            Assert.Equal(2, resultado.Alternativas.Count());

            repositorioItemMock.Verify(r => r.ObterComAlternativaPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Nao_Encontrar_Item()
        {
            Dominio.Entities.Item itemNulo = null;
            var query = new ObterItemComAlternativaPorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterComAlternativaPorIdAsync(ItemIdValido))
                               .ReturnsAsync(itemNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioItemMock.Verify(r => r.ObterComAlternativaPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterItemComAlternativaPorIdQuery(ItemIdValido);
            var excecaoEsperada = new InvalidOperationException("Falha no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterComAlternativaPorIdAsync(ItemIdValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);
            repositorioItemMock.Verify(r => r.ObterComAlternativaPorIdAsync(ItemIdValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterItemComAlternativaPorIdQueryHandler(null));
        }
    }
}

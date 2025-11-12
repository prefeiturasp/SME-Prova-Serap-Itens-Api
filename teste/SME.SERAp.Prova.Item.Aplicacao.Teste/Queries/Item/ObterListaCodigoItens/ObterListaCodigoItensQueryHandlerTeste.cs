using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterListaCodigoItensQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterListaCodigoItensQueryHandler handler;
        private const string CodigoItemValido = "ITEM001";

        public ObterListaCodigoItensQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterListaCodigoItensQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_Codigos_Quando_Repositorio_Retornar_Dados()
        {
            var codigosEsperados = new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 1, CodigoItem = "ITEM001" },
                new CodigoItemDto { Id = 2, CodigoItem = "ITEM002" }
            };

            var query = new ObterListaCodigoItensQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterListaCodigosItens(CodigoItemValido))
                               .ReturnsAsync(codigosEsperados);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, c => c.CodigoItem == "ITEM001");

            repositorioItemMock.Verify(r => r.ObterListaCodigosItens(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Repositorio_Nao_Encontrar_Dados()
        {
            var listaVazia = Enumerable.Empty<CodigoItemDto>();
            var query = new ObterListaCodigoItensQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterListaCodigosItens(CodigoItemValido))
                               .ReturnsAsync(listaVazia);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            repositorioItemMock.Verify(r => r.ObterListaCodigosItens(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterListaCodigoItensQuery(CodigoItemValido);
            var excecaoEsperada = new InvalidOperationException("Erro no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterListaCodigosItens(CodigoItemValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);

            repositorioItemMock.Verify(r => r.ObterListaCodigosItens(CodigoItemValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterListaCodigoItensQueryHandler(null));
        }
    }
}

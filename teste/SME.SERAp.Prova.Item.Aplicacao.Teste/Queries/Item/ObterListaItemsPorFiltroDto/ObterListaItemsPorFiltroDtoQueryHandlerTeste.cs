using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterListaItemsPorFiltroDtoQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterListaItemsPorFiltroDtoQueryHandler handler;

        public ObterListaItemsPorFiltroDtoQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterListaItemsPorFiltroDtoQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Paginacao_Quando_Repositorio_Retornar_Dados()
        {
            var filtro = new FiltroItemsDto { CodigoItem = "Teste" };
            var query = new ObterListaItemsPorFiltroDtoQuery(filtro);

            var registros = new List<ItemListaDto>  
            {
                new ItemListaDto { Id = 1, CodigoItem = "Item 1" },
                new ItemListaDto { Id = 2, CodigoItem = "Item 2" }
            };

            var paginacaoEsperada = new PaginacaoDto<ItemListaDto>(registros, 1, 10, registros.Count);

            repositorioItemMock.Setup(r => r.ObterListaItensPorFiltro(filtro))
                               .ReturnsAsync(paginacaoEsperada);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.TotalRegistros);
            Assert.Equal(2, resultado.Itens.Count());
            Assert.Contains(resultado.Itens, i => i.CodigoItem == "Item 1");

            repositorioItemMock.Verify(r => r.ObterListaItensPorFiltro(filtro), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Paginacao_Vazia_Quando_Repositorio_Nao_Trouxer_Dados()
        {
            var filtro = new FiltroItemsDto { CodigoItem = "Vazio" };
            var query = new ObterListaItemsPorFiltroDtoQuery(filtro);

            var paginacaoVazia = new PaginacaoDto<ItemListaDto>(new List<ItemListaDto>(), 1 , 10 , 0);

            repositorioItemMock.Setup(r => r.ObterListaItensPorFiltro(filtro))
                               .ReturnsAsync(paginacaoVazia);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(0, resultado.TotalRegistros);
            Assert.Empty(resultado.Itens);

            repositorioItemMock.Verify(r => r.ObterListaItensPorFiltro(filtro), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var filtro = new FiltroItemsDto { CodigoItem = "Erro" };
            var query = new ObterListaItemsPorFiltroDtoQuery(filtro);
            var excecaoEsperada = new InvalidOperationException("Falha no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterListaItensPorFiltro(filtro))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);
            repositorioItemMock.Verify(r => r.ObterListaItensPorFiltro(filtro), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterListaItemsPorFiltroDtoQueryHandler(null));
        }
    }
}

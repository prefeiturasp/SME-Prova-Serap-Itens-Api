using Moq;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterListaItemsUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterListaItemsUseCase useCase;

        public ObterListaItemsUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterListaItemsUseCase(mediatorMock.Object);
        }

        private PaginacaoDto<ItemListaDto> ObterPaginacaoVaziaMock()
        {
            var itens = Enumerable.Empty<ItemListaDto>();
            return new PaginacaoDto<ItemListaDto>(itens, 1, 10, 0);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterListaItemsUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Se_Query_Retornar_Lista_Vazia()
        {
            var filtro = new FiltroItemsDto();
            var listaVazia = ObterPaginacaoVaziaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(listaVazia);

            var resultado = await useCase.Executar(filtro);

            Assert.Null(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Se_Query_Retornar_Nulo()
        {
            var filtro = new FiltroItemsDto();
            PaginacaoDto<ItemListaDto> listaNula = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(listaNula);

            var resultado = await useCase.Executar(filtro);

            Assert.Null(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
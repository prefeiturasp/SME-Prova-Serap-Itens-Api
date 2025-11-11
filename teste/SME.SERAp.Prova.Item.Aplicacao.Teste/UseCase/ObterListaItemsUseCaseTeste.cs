using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dominio.Enums;
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

        private PaginacaoDto<ItemListaDto> ObterPaginacaoComItensMock()
        {
            var itens = new List<ItemListaDto>
            {
                new ItemListaDto { Id = 1, Situacao = (long)SituacaoItem.Ativo },
                new ItemListaDto { Id = 2, Situacao = (long)SituacaoItem.Pendente },
                new ItemListaDto { Id = 3, Situacao = null }
            };
            return new PaginacaoDto<ItemListaDto>(itens, 1, 10, 3);
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
        public async Task Deve_Retornar_ListaItems_Com_Descricao_Situacao_Preenchida_Em_Caso_De_Sucesso()
        {
            var filtro = new FiltroItemsDto();
            var listaItemsEsperada = ObterPaginacaoComItensMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(listaItemsEsperada);

            var resultado = await useCase.Executar(filtro);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Itens.Count());

            var itemAtivo = resultado.Itens.First(i => i.Id == 1);
            Assert.Equal((long)SituacaoItem.Ativo, itemAtivo.Situacao);
            Assert.Equal("Ativo", itemAtivo.SituacaoDesc);

            var itemPendente = resultado.Itens.First(i => i.Id == 2);
            Assert.Equal((long)SituacaoItem.Pendente, itemPendente.Situacao);
            Assert.Equal("Pendente", itemPendente.SituacaoDesc);

            var itemSemSituacao = resultado.Itens.First(i => i.Id == 3);
            Assert.Null(itemSemSituacao.Situacao);
            Assert.Null(itemSemSituacao.SituacaoDesc);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaItemsPorFiltroDtoQuery>(), It.IsAny<CancellationToken>()), Times.Once);
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
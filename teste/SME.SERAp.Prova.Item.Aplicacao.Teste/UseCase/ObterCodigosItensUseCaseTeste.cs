using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterCodigosItensUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterCodigosItensUseCase useCase;
        private const string CodigoItemFiltro = "IT-2023";

        public ObterCodigosItensUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterCodigosItensUseCase(mediatorMock.Object);
        }

        private IEnumerable<CodigoItemDto> ObterCodigosItensDesordenadosMock()
        {
            return new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 3, CodigoItem = "IT-2023-C" },
                new CodigoItemDto { Id = 1, CodigoItem = "IT-2023-A" },
                new CodigoItemDto { Id = 5, CodigoItem = "IT-2023-E" },
                new CodigoItemDto { Id = 2, CodigoItem = "IT-2023-B" },
                new CodigoItemDto { Id = 4, CodigoItem = "IT-2023-D" }
            };
        }

        private IEnumerable<CodigoItemDto> ObterCodigosItensOrdenadosMock()
        {
            return new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 1, CodigoItem = "IT-2023-A" },
                new CodigoItemDto { Id = 2, CodigoItem = "IT-2023-B" },
                new CodigoItemDto { Id = 3, CodigoItem = "IT-2023-C" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterCodigosItensUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Ordenada_Por_CodigoItem_Em_Caso_De_Sucesso()
        {
            var codigosItens = ObterCodigosItensDesordenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Equal("IT-2023-A", resultadoLista[0].Descricao);
            Assert.Equal("IT-2023-B", resultadoLista[1].Descricao);
            Assert.Equal("IT-2023-C", resultadoLista[2].Descricao);
            Assert.Equal("IT-2023-D", resultadoLista[3].Descricao);
            Assert.Equal("IT-2023-E", resultadoLista[4].Descricao);

            mediatorMock.Verify(m => m.Send(It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == CodigoItemFiltro), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Mapear_Corretamente_Id_Para_Valor_E_CodigoItem_Para_Descricao()
        {
            var codigosItens = ObterCodigosItensOrdenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Equal(1, resultadoLista[0].Valor);
            Assert.Equal("IT-2023-A", resultadoLista[0].Descricao);

            Assert.Equal(2, resultadoLista[1].Valor);
            Assert.Equal("IT-2023-B", resultadoLista[1].Descricao);

            Assert.Equal(3, resultadoLista[2].Valor);
            Assert.Equal("IT-2023-C", resultadoLista[2].Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Query_Retornar_Null()
        {
            IEnumerable<CodigoItemDto> codigosItensNull = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItensNull);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == CodigoItemFiltro), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Query_Retornar_Lista_Vazia()
        {
            var codigosItensVazios = Enumerable.Empty<CodigoItemDto>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItensVazios);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == CodigoItemFiltro), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Ordenar_Corretamente_Lista_Com_CodigosItens_Numericos()
        {
            var codigosItens = new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 10, CodigoItem = "IT-100" },
                new CodigoItemDto { Id = 2, CodigoItem = "IT-20" },
                new CodigoItemDto { Id = 5, CodigoItem = "IT-50" },
                new CodigoItemDto { Id = 1, CodigoItem = "IT-10" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.Count());

            var resultadoArray = resultado.ToArray();

            Assert.Equal("IT-10", resultadoArray[0].Descricao);
            Assert.Equal("IT-100", resultadoArray[1].Descricao);
            Assert.Equal("IT-20", resultadoArray[2].Descricao);
            Assert.Equal("IT-50", resultadoArray[3].Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Mediator_Com_CodigoItem_Correto()
        {
            const string codigoItemEspecifico = "IT-2024-TESTE";
            var codigosItens = ObterCodigosItensOrdenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            await useCase.Executar(codigoItemEspecifico);

            mediatorMock.Verify(m => m.Send(
                It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == codigoItemEspecifico),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_SelectDto_Correto_Para_Um_Unico_Codigo()
        {
            var codigoUnico = new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 999, CodigoItem = "IT-UNICO-2023" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigoUnico);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            Assert.Single(resultado);

            var item = resultado.First();
            Assert.Equal(999, item.Valor);
            Assert.Equal("IT-UNICO-2023", item.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Ordenar_Lista_Com_CodigosItens_Identicos_Mantendo_Estabilidade()
        {
            var codigosItens = new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 5, CodigoItem = "IT-2023-A" },
                new CodigoItemDto { Id = 3, CodigoItem = "IT-2023-A" },
                new CodigoItemDto { Id = 1, CodigoItem = "IT-2023-A" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var resultadoArray = resultado.ToArray();

            Assert.All(resultadoArray, item => Assert.Equal("IT-2023-A", item.Descricao));

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Converter_CodigoItem_Para_String_Na_Descricao()
        {
            var codigosItens = new List<CodigoItemDto>
            {
                new CodigoItemDto { Id = 1, CodigoItem = "IT-2023-TEST" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(CodigoItemFiltro);

            Assert.NotNull(resultado);
            var item = resultado.First();

            Assert.IsType<string>(item.Descricao);
            Assert.Equal("IT-2023-TEST", item.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Aceitar_CodigoItem_Null_Como_Parametro()
        {
            string codigoItemNull = null;
            var codigosItens = ObterCodigosItensOrdenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(codigoItemNull);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            mediatorMock.Verify(m => m.Send(
                It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == null),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Aceitar_CodigoItem_Vazio_Como_Parametro()
        {
            string codigoItemVazio = string.Empty;
            var codigosItens = ObterCodigosItensOrdenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterListaCodigoItensQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(codigosItens);

            var resultado = await useCase.Executar(codigoItemVazio);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            mediatorMock.Verify(m => m.Send(
                It.Is<ObterListaCodigoItensQuery>(q => q.CodigoItem == string.Empty),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
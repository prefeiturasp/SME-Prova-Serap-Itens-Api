using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterAlternativasBrutasPorItemId;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemBasePorId;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem;
using SME.SERAp.Prova.Item.Aplicacao.UseCases.Item;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterItemResumoPorIdUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterItemResumoPorIdUseCase useCase;
        private const long ItemIdValido = 100L;
        private const string CodigoItemValido = "IT-2023-A";

        public ObterItemResumoPorIdUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterItemResumoPorIdUseCase(mediatorMock.Object);
        }

        private DominioItem ObterItemBaseMock()
        {
            return new DominioItem
            {
                Id = ItemIdValido,
                CodigoItem = CodigoItemValido,
                TextoBase = "Base do Item",
                Enunciado = "Enunciado do Item",
                Fonte = "Fonte Simulado",
                VersaoItem = 2,
                DataCriacao = new DateTime(2023, 10, 27)
            };
        }

        private IEnumerable<Alternativa> ObterAlternativasMock()
        {
            return new List<Alternativa>
            {
                new Alternativa { Id = 201, ItemId = ItemIdValido, Descricao = "Alternativa A", Ordem = 1, Numeracao = "A" },
                new Alternativa { Id = 202, ItemId = ItemIdValido, Descricao = "Alternativa B", Ordem = 2, Numeracao = "B" }
            };
        }

        private IEnumerable<DominioItem> ObterVersoesMock()
        {
            return new List<DominioItem>
            {
                new DominioItem { Id = 101, CodigoItem = CodigoItemValido, VersaoItem = 1, DataCriacao = new DateTime(2023, 10, 20) },
                new DominioItem { Id = 100, CodigoItem = CodigoItemValido, VersaoItem = 2, DataCriacao = new DateTime(2023, 10, 27) },
                new DominioItem { Id = 102, CodigoItem = CodigoItemValido, VersaoItem = 3, DataCriacao = new DateTime(2023, 11, 01) }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterItemResumoPorIdUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_ItemResumoDto_Com_Dados_Completos_Em_Caso_De_Sucesso()
        {
            var itemBase = ObterItemBaseMock();
            var alternativas = ObterAlternativasMock();
            var versoes = ObterVersoesMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterItemBasePorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemBase);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAlternativasBrutasPorItemIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(alternativas);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTodasVersoesPorCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(versoes);

            var resultado = await useCase.Executar(ItemIdValido);

            Assert.NotNull(resultado);

            Assert.Equal(ItemIdValido, resultado.Id);
            Assert.Equal(CodigoItemValido, resultado.CodigoItem);
            Assert.Equal(itemBase.VersaoItem, resultado.VersaoItem);
            Assert.Equal(versoes.Count(), resultado.QuantidadeVersoes);

            Assert.Equal(alternativas.Count(), resultado.Alternativas.Count);
            Assert.Contains(resultado.Alternativas, a => a.Numeracao == "A" && a.Ordem == 1);

            Assert.Equal(versoes.Count(), resultado.VersoesDisponiveis.Count);
            Assert.Equal(1, resultado.VersoesDisponiveis.First().VersaoItem);
            Assert.Equal(3, resultado.VersoesDisponiveis.Last().VersaoItem);

            mediatorMock.Verify(m => m.Send(It.Is<ObterItemBasePorIdQuery>(q => q.ItemId == ItemIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterAlternativasBrutasPorItemIdQuery>(q => q.ItemId == ItemIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterTodasVersoesPorCodigoItemQuery>(q => q.CodigoItem == CodigoItemValido), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_E_Parar_Se_ItemBase_Nao_For_Encontrado()
        {
            DominioItem itemNulo = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterItemBasePorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemNulo);

            var resultado = await useCase.Executar(ItemIdValido);

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterItemBasePorIdQuery>(q => q.ItemId == ItemIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAlternativasBrutasPorItemIdQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterTodasVersoesPorCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Retornar_ItemResumoDto_Com_Listas_Vazias_Se_Alternativas_E_Versoes_Nao_Existirem()
        {
            var itemBase = ObterItemBaseMock();
            var alternativasVazias = Enumerable.Empty<Alternativa>();
            var versoesVazias = Enumerable.Empty<DominioItem>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterItemBasePorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemBase);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAlternativasBrutasPorItemIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(alternativasVazias);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTodasVersoesPorCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(versoesVazias);

            var resultado = await useCase.Executar(ItemIdValido);

            Assert.NotNull(resultado);

            Assert.Empty(resultado.Alternativas);
            Assert.Empty(resultado.VersoesDisponiveis);
            Assert.Equal(0, resultado.QuantidadeVersoes);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterItemBasePorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAlternativasBrutasPorItemIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterTodasVersoesPorCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Aplicacao.Queries.NivelItem.ObterIdNivelItemOrdem;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterNivelItemUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterNivelItemUseCase useCase;

        public ObterNivelItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterNivelItemUseCase(mediatorMock.Object);
        }

        private IEnumerable<NivelItem> ObterNiveisItemMock()
        {
            return new List<NivelItem>
            {
                new NivelItem(1, "Muito Fácil", 1, StatusGeral.Ativo),
                new NivelItem(2, "Fácil", 2, StatusGeral.Ativo),
                new NivelItem(3, "Médio", 3, StatusGeral.Ativo),
                new NivelItem(4, "Difícil", 4, StatusGeral.Ativo),
                new NivelItem(5, "Muito Difícil", 5, StatusGeral.Ativo)
            };
        }

        private IEnumerable<NivelItem> ObterNiveisItemDesordenadosMock()
        {
            return new List<NivelItem>
            {
                new NivelItem(3, "Médio", 3, StatusGeral.Ativo),
                new NivelItem(1, "Muito Fácil", 1, StatusGeral.Ativo),
                new NivelItem(5, "Muito Difícil", 5, StatusGeral.Ativo),
                new NivelItem(2, "Fácil", 2, StatusGeral.Ativo),
                new NivelItem(4, "Difícil", 4, StatusGeral.Ativo)
            };
        }

        private IEnumerable<NivelItem> ObterNiveisItemComStatusVariadoMock()
        {
            return new List<NivelItem>
            {
                new NivelItem(1, "Muito Fácil", 1, StatusGeral.Ativo),
                new NivelItem(2, "Fácil", 2, StatusGeral.Inativo),
                new NivelItem(3, "Médio", 3, StatusGeral.Ativo),
                new NivelItem(4, "Difícil", 4, StatusGeral.Inativo),
                new NivelItem(5, "Muito Difícil", 5, StatusGeral.Ativo)
            };
        }

        #region Testes de Construtor

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterNivelItemUseCase(null));
        }

        [Fact]
        public void Construtor_Deve_Inicializar_Corretamente_Com_Mediator_Valido()
        {
            var mediator = new Mock<IMediator>();
            var useCaseInstance = new ObterNivelItemUseCase(mediator.Object);

            Assert.NotNull(useCaseInstance);
        }

        #endregion

        #region Testes de Obtenção com Sucesso

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Com_Dados_Completos_Em_Caso_De_Sucesso()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Count());

            var primeiroItem = resultado.First();
            Assert.Equal(1, primeiroItem.Valor);
            Assert.Equal("1 - Muito Fácil", primeiroItem.Descricao);

            var ultimoItem = resultado.Last();
            Assert.Equal(5, ultimoItem.Valor);
            Assert.Equal("5 - Muito Difícil", ultimoItem.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Niveis_Ordenados_Por_Ordem()
        {
            var niveisDesordenados = ObterNiveisItemDesordenadosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisDesordenados);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Equal(1, listaResultado[0].Valor);
            Assert.Equal(2, listaResultado[1].Valor);
            Assert.Equal(3, listaResultado[2].Valor);
            Assert.Equal(4, listaResultado[3].Valor);
            Assert.Equal(5, listaResultado[4].Valor);
        }

        [Fact]
        public async Task Deve_Formatar_Descricao_Corretamente_Com_Ordem_E_Descricao()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var listaResultado = resultado.ToList();
            Assert.Equal("1 - Muito Fácil", listaResultado[0].Descricao);
            Assert.Equal("2 - Fácil", listaResultado[1].Descricao);
            Assert.Equal("3 - Médio", listaResultado[2].Descricao);
            Assert.Equal("4 - Difícil", listaResultado[3].Descricao);
            Assert.Equal("5 - Muito Difícil", listaResultado[4].Descricao);
        }

        [Fact]
        public async Task Deve_Mapear_Id_Para_Valor_Em_SelectDto()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item => Assert.True(item.Valor > 0));
            Assert.Contains(resultado, item => item.Valor == 1);
            Assert.Contains(resultado, item => item.Valor == 5);
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Os_Niveis_Sem_Filtrar_Por_Status()
        {
            var niveisComStatusVariado = ObterNiveisItemComStatusVariadoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisComStatusVariado);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Count());
        }

        #endregion

        #region Testes com Lista Vazia ou Nula

        [Fact]
        public async Task Deve_Retornar_Default_Quando_NivelItem_For_Nulo()
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((IEnumerable<NivelItem>)null);

            var resultado = await useCase.Executar();

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Default_Quando_NivelItem_For_Lista_Vazia()
        {
            var niveisVazios = Enumerable.Empty<NivelItem>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisVazios);

            var resultado = await useCase.Executar();

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Lancar_Excecao_Quando_Lista_For_Vazia()
        {
            var niveisVazios = new List<NivelItem>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisVazios);

            var resultado = await useCase.Executar();

            Assert.Null(resultado);
        }

        #endregion

        #region Testes com Diferentes Quantidades de Itens

        [Fact]
        public async Task Deve_Processar_Lista_Com_Um_Unico_Nivel()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Único Nível", 1, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal("1 - Único Nível", resultado.First().Descricao);
        }

        [Fact]
        public async Task Deve_Processar_Lista_Com_Dois_Niveis()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Fácil", 1, StatusGeral.Ativo),
                new NivelItem(2, "Difícil", 2, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task Deve_Processar_Lista_Com_Multiplos_Niveis()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Nível 1", 1, StatusGeral.Ativo),
                new NivelItem(2, "Nível 2", 2, StatusGeral.Ativo),
                new NivelItem(3, "Nível 3", 3, StatusGeral.Ativo),
                new NivelItem(4, "Nível 4", 4, StatusGeral.Ativo),
                new NivelItem(5, "Nível 5", 5, StatusGeral.Ativo),
                new NivelItem(6, "Nível 6", 6, StatusGeral.Ativo),
                new NivelItem(7, "Nível 7", 7, StatusGeral.Ativo),
                new NivelItem(8, "Nível 8", 8, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(8, resultado.Count());
        }

        #endregion

        #region Testes de Ordenação

        [Fact]
        public async Task Deve_Manter_Ordenacao_Quando_Ordens_Sao_Sequenciais()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Nível A", 1, StatusGeral.Ativo),
                new NivelItem(2, "Nível B", 2, StatusGeral.Ativo),
                new NivelItem(3, "Nível C", 3, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal("1 - Nível A", listaResultado[0].Descricao);
            Assert.Equal("2 - Nível B", listaResultado[1].Descricao);
            Assert.Equal("3 - Nível C", listaResultado[2].Descricao);
        }

        [Fact]
        public async Task Deve_Ordenar_Corretamente_Quando_Ordens_Nao_Sao_Sequenciais()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Nível A", 10, StatusGeral.Ativo),
                new NivelItem(2, "Nível B", 5, StatusGeral.Ativo),
                new NivelItem(3, "Nível C", 20, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal("5 - Nível B", listaResultado[0].Descricao);
            Assert.Equal("10 - Nível A", listaResultado[1].Descricao);
            Assert.Equal("20 - Nível C", listaResultado[2].Descricao);
        }

        [Fact]
        public async Task Deve_Ordenar_Mesmo_Com_Ordens_Duplicadas()
        {
            var niveisItem = new List<NivelItem>
            {
                new NivelItem(1, "Nível A", 2, StatusGeral.Ativo),
                new NivelItem(2, "Nível B", 1, StatusGeral.Ativo),
                new NivelItem(3, "Nível C", 2, StatusGeral.Ativo)
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal("1 - Nível B", listaResultado[0].Descricao);
            Assert.Contains(listaResultado[1].Descricao, new[] { "2 - Nível A", "2 - Nível C" });
            Assert.Contains(listaResultado[2].Descricao, new[] { "2 - Nível A", "2 - Nível C" });
        }

        #endregion

        #region Testes de Invocação do Mediator

        [Fact]
        public async Task Deve_Invocar_Mediator_Exatamente_Uma_Vez()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            await useCase.Executar();

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Mediator_Com_ObterNivelItemQuery_Correto()
        {
            var niveisItem = ObterNiveisItemMock();

            ObterNivelItemQuery queryCapturada = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .Callback<IRequest<IEnumerable<NivelItem>>, CancellationToken>((query, ct) =>
                        {
                            queryCapturada = query as ObterNivelItemQuery;
                        })
                        .ReturnsAsync(niveisItem);

            await useCase.Executar();

            Assert.NotNull(queryCapturada);
        }

        #endregion

        #region Testes de Propagação de Exceções

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Mediator_Lancar_Excecao()
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Erro ao obter níveis de item"));

            await Assert.ThrowsAsync<Exception>(() => useCase.Executar());

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_De_Timeout()
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new TimeoutException("Tempo limite excedido"));

            await Assert.ThrowsAsync<TimeoutException>(() => useCase.Executar());
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_De_InvalidOperation()
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new InvalidOperationException("Operação inválida"));

            await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.Executar());
        }

        #endregion

        #region Testes de Validação de Dados

        [Fact]
        public async Task Deve_Retornar_SelectDto_Com_Propriedades_Preenchidas()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item =>
            {
                Assert.True(item.Valor > 0);
                Assert.NotEmpty(item.Descricao);
            });
        }

        [Fact]
        public async Task Deve_Garantir_Que_Descricao_Contenha_Ordem_E_Texto()
        {
            var niveisItem = ObterNiveisItemMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item =>
            {
                Assert.Contains(" - ", item.Descricao);
                Assert.True(item.Descricao.Split(" - ").Length == 2);
            });
        }

        [Fact]
        public async Task Deve_Retornar_Quantidade_Correta_De_Itens()
        {
            var niveisItem = ObterNiveisItemMock();
            var quantidadeEsperada = niveisItem.Count();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterNivelItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(niveisItem);

            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(quantidadeEsperada, resultado.Count());
        }

        #endregion
    }
}
using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterAlternativasBrutasPorItemId;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterAlternativasBrutasPorItemIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterAlternativasBrutasPorItemIdQueryHandler handler;
        private const long ItemIdValido = 789;

        public ObterAlternativasBrutasPorItemIdQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterAlternativasBrutasPorItemIdQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Alternativas_Quando_Repositorio_Retornar_Dados()
        {
            var alternativasEsperadas = new List<Alternativa>
            {
                new Alternativa { Id = 1, ItemId = ItemIdValido, Descricao = "Alternativa A" },
                new Alternativa { Id = 2, ItemId = ItemIdValido, Descricao = "Alternativa B" }
            };
            var query = new ObterAlternativasBrutasPorItemIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterAlternativasPorItemId(ItemIdValido))
                               .ReturnsAsync(alternativasEsperadas);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, a => a.Id == 1);

            repositorioItemMock.Verify(r => r.ObterAlternativasPorItemId(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Repositorio_Nao_Encontrar_Alternativas()
        {
            var alternativasVazias = Enumerable.Empty<Alternativa>();
            var query = new ObterAlternativasBrutasPorItemIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterAlternativasPorItemId(ItemIdValido))
                               .ReturnsAsync(alternativasVazias);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            repositorioItemMock.Verify(r => r.ObterAlternativasPorItemId(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterAlternativasBrutasPorItemIdQuery(ItemIdValido);
            var excecaoEsperada = new InvalidOperationException("Erro de conexão com o banco.");

            repositorioItemMock.Setup(r => r.ObterAlternativasPorItemId(ItemIdValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);

            repositorioItemMock.Verify(r => r.ObterAlternativasPorItemId(ItemIdValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterAlternativasBrutasPorItemIdQueryHandler(null));
        }
    }
}
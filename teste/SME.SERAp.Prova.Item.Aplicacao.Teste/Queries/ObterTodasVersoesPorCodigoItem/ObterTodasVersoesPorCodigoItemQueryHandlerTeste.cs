using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.ObterTodasVersoesPorCodigoItem
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterTodasVersoesPorCodigoItemQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterTodasVersoesPorCodigoItemQueryHandler handler;
        private const string CodigoItemValido = "IT-VERSAO-A";

        public ObterTodasVersoesPorCodigoItemQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterTodasVersoesPorCodigoItemQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Todas_As_Versoes_Quando_Repositorio_Encontrar_Dados()
        {
            var itensEsperados = new List<DominioItem>
            {
                new DominioItem { Id = 10, CodigoItem = CodigoItemValido, VersaoItem = 1 },
                new DominioItem { Id = 11, CodigoItem = CodigoItemValido, VersaoItem = 2 },
                new DominioItem { Id = 12, CodigoItem = CodigoItemValido, VersaoItem = 3 }
            };
            var query = new ObterTodasVersoesPorCodigoItemQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido))
                               .ReturnsAsync(itensEsperados);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());
            Assert.True(resultado.All(i => i.CodigoItem == CodigoItemValido));

            repositorioItemMock.Verify(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Repositorio_Nao_Encontrar_Versoes()
        {
            var itensVazios = Enumerable.Empty<DominioItem>();
            var query = new ObterTodasVersoesPorCodigoItemQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido))
                               .ReturnsAsync(itensVazios);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            repositorioItemMock.Verify(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterTodasVersoesPorCodigoItemQuery(CodigoItemValido);
            var excecaoEsperada = new TimeoutException("Conexão expirou na consulta de versões.");

            repositorioItemMock.Setup(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<TimeoutException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);

            repositorioItemMock.Verify(r => r.ObterTodasVersoesPorCodigoItem(CodigoItemValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterTodasVersoesPorCodigoItemQueryHandler(null));
        }
    }
}
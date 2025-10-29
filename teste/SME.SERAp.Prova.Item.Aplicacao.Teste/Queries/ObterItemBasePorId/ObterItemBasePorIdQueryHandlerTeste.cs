using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemBasePorId;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.ObterItemBasePorId
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterItemBasePorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterItemBasePorIdQueryHandler handler;
        private const long ItemIdValido = 999;
        private const string CodigoItemValido = "IT-BASE-01";

        public ObterItemBasePorIdQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterItemBasePorIdQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Item_Quando_Repositorio_Encontrar_Dados()
        {
            var itemEsperado = new DominioItem { Id = ItemIdValido, CodigoItem = CodigoItemValido };
            var query = new ObterItemBasePorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterPorId(ItemIdValido))
                               .ReturnsAsync(itemEsperado);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(ItemIdValido, resultado.Id);
            Assert.Equal(CodigoItemValido, resultado.CodigoItem);

            repositorioItemMock.Verify(r => r.ObterPorId(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Repositorio_Nao_Encontrar_Item()
        {
            DominioItem itemNulo = null;
            var query = new ObterItemBasePorIdQuery(ItemIdValido);

            repositorioItemMock.Setup(r => r.ObterPorId(ItemIdValido))
                               .ReturnsAsync(itemNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);

            repositorioItemMock.Verify(r => r.ObterPorId(ItemIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterItemBasePorIdQuery(ItemIdValido);
            var excecaoEsperada = new Exception("Erro de Timeout na base de dados.");

            repositorioItemMock.Setup(r => r.ObterPorId(ItemIdValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);

            repositorioItemMock.Verify(r => r.ObterPorId(ItemIdValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterItemBasePorIdQueryHandler(null));
        }
    }
}
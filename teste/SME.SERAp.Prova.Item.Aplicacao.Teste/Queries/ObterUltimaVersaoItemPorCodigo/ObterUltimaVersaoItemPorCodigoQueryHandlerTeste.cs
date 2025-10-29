using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.ObterUltimaVersaoItemPorCodigo
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterUltimaVersaoItemPorCodigoQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterUltimaVersaoItemPorCodigoQueryHandler handler;
        private const string CodigoItemValido = "IT-ULTIMA-VERSAO";

        public ObterUltimaVersaoItemPorCodigoQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterUltimaVersaoItemPorCodigoQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterUltimaVersaoItemPorCodigoQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Ultima_Versao_Item_Quando_Repositorio_Encontrar_Dados()
        {
            var itemEsperado = new DominioItem { Id = 500, CodigoItem = CodigoItemValido, VersaoItem = 5, TextoBase = "Base V5" };
            var query = new ObterUltimaVersaoItemPorCodigoQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido))
                               .ReturnsAsync(itemEsperado);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(itemEsperado.Id, resultado.Id);
            Assert.Equal(itemEsperado.VersaoItem, resultado.VersaoItem);

            repositorioItemMock.Verify(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Repositorio_Nao_Encontrar_Item()
        {
            DominioItem itemNulo = null;
            var query = new ObterUltimaVersaoItemPorCodigoQuery(CodigoItemValido);

            repositorioItemMock.Setup(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido))
                               .ReturnsAsync(itemNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);

            repositorioItemMock.Verify(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterUltimaVersaoItemPorCodigoQuery(CodigoItemValido);
            var excecaoEsperada = new Exception("Erro de falha de serviço interno ao obter a última versão.");

            repositorioItemMock.Setup(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);

            repositorioItemMock.Verify(r => r.ObterUltimaVersaoItemPorCodigo(CodigoItemValido), Times.Once);
        }
    }
}
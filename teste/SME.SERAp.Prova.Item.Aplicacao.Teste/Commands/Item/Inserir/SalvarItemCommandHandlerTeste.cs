using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Item
{
    public class SalvarItemCommandHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItem;
        private readonly SalvarItemCommandHandler handler;

        public SalvarItemCommandHandlerTeste()
        {
            repositorioItem = new Mock<IRepositorioItem>();
            handler = new SalvarItemCommandHandler(repositorioItem.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SalvarItemCommandHandler(null));
            Assert.Equal("repositorioItem", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_Item_Com_Sucesso()
        {
            var item = new Dominio.Entities.Item { Id = 1 };
            var command = new SalvarItemCommand(item);

            repositorioItem
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Item>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositorioItem.Verify(r => r.SalvarAsync(It.Is<Dominio.Entities.Item>(i => i == item)), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var item = new Dominio.Entities.Item { Id = 1};
            var command = new SalvarItemCommand(item);

            repositorioItem
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Item>()))
                .ThrowsAsync(new Exception("Erro ao salvar item"));

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar item", ex.Message);
            repositorioItem.Verify(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Item>()), Times.Once);
        }
    }
}

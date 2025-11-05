using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.SequencialItem
{
    public class SalvarSequencialItemCommandHandlerTeste
    {
        private readonly Mock<IRepositoSequencialItem> repositoSequencialItem;
        private readonly SalvarSequencialItemCommandHandler handler;

        public SalvarSequencialItemCommandHandlerTeste()
        {
            repositoSequencialItem = new Mock<IRepositoSequencialItem>();
            handler = new SalvarSequencialItemCommandHandler(repositoSequencialItem.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SalvarSequencialItemCommandHandler(null));

            Assert.Equal("repositoSequencialItem", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_SequencialItem_Com_Sucesso()
        {
            var sequencialItem = new Dominio.Entities.SequencialItem { Id = 1 };
            var command = new SalvarSequencialItemCommand(sequencialItem);

            repositoSequencialItem
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.SequencialItem>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositoSequencialItem.Verify(r =>
                r.SalvarAsync(It.Is<Dominio.Entities.SequencialItem>(s => s == sequencialItem)),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var sequencialItem = new Dominio.Entities.SequencialItem { Id = 1 };
            var command = new SalvarSequencialItemCommand(sequencialItem);

            repositoSequencialItem
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.SequencialItem>()))
                .ThrowsAsync(new Exception("Erro ao salvar sequencial item"));

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar sequencial item", ex.Message);
            repositoSequencialItem.Verify(r =>
                r.SalvarAsync(It.IsAny<Dominio.Entities.SequencialItem>()),
                Times.Once);
        }
    }
}

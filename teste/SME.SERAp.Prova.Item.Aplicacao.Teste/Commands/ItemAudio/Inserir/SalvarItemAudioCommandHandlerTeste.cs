using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.ItemAudio
{
    public class SalvarItemAudioCommandHandlerTeste
    {
        private readonly Mock<IRepositorioItemAudio> repositorioItemAudio;
        private readonly SalvarItemAudioCommandHandler handler;

        public SalvarItemAudioCommandHandlerTeste()
        {
            repositorioItemAudio = new Mock<IRepositorioItemAudio>();
            handler = new SalvarItemAudioCommandHandler(repositorioItemAudio.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SalvarItemAudioCommandHandler(null));
            Assert.Equal("repositorioItemAudio", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_ItemAudio_Com_Sucesso()
        {
            var itemAudio = new Dominio.Entities.ItemAudio { Id = 1 };
            var command = new SalvarItemAudioCommand(itemAudio);

            repositorioItemAudio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.ItemAudio>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositorioItemAudio.Verify(r => r.SalvarAsync(It.Is<Dominio.Entities.ItemAudio>(i => i == itemAudio)), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var itemAudio = new Dominio.Entities.ItemAudio { Id = 1 };
            var command = new SalvarItemAudioCommand(itemAudio);

            repositorioItemAudio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.ItemAudio>()))
                .ThrowsAsync(new Exception("Erro ao salvar item audio"));

            var ex = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar item audio", ex.Message);
            repositorioItemAudio.Verify(r => r.SalvarAsync(It.IsAny<Dominio.Entities.ItemAudio>()), Times.Once);
        }
    }
}

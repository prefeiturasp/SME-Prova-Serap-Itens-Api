using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVideo.Inserir;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.ItemVideo
{
    public class SalvarItemVideoCommandHandlerTeste
    {
        private readonly Mock<IRepositorioItemVideo> repositorioItemVideo;
        private readonly SalvarItemVideoCommandHandler handler;

        public SalvarItemVideoCommandHandlerTeste()
        {
            repositorioItemVideo = new Mock<IRepositorioItemVideo>();
            handler = new SalvarItemVideoCommandHandler(repositorioItemVideo.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new SalvarItemVideoCommandHandler(null));

            Assert.Equal("repositorioItemVideo", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_ItemVideo_Com_Sucesso()
        {
            var itemVideo = new Dominio.Entities.ItemVideo { Id = 1 };
            var command = new SalvarItemVideoCommand(itemVideo);

            repositorioItemVideo
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.ItemVideo>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositorioItemVideo.Verify(r =>
                r.SalvarAsync(It.Is<Dominio.Entities.ItemVideo>(i => i == itemVideo)),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var itemVideo = new Dominio.Entities.ItemVideo { Id = 1 };
            var command = new SalvarItemVideoCommand(itemVideo);

            repositorioItemVideo
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.ItemVideo>()))
                .ThrowsAsync(new Exception("Erro ao salvar item video"));

            var ex = await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar item video", ex.Message);
            repositorioItemVideo.Verify(r =>
                r.SalvarAsync(It.IsAny<Dominio.Entities.ItemVideo>()),
                Times.Once);
        }
    }
}

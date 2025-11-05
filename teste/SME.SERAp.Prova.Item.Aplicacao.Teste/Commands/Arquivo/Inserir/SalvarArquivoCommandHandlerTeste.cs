using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Arquivo.Inserir;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Arquivo
{
    public class SalvarArquivoCommandHandlerTeste
    {
        private readonly Mock<IRepositorioArquivo> repositorio;
        private readonly SalvarArquivoCommandHandler handler;

        public SalvarArquivoCommandHandlerTeste()
        {
            repositorio = new Mock<IRepositorioArquivo>();
            handler = new SalvarArquivoCommandHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new SalvarArquivoCommandHandler(null));
            Assert.Equal("repositorioArquivo", exception.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_Arquivo_Com_Sucesso()
        {
            var arquivo = new Dominio.Entities.Arquivo
            {
                Id = 1,
                Nome = "teste.pdf",
                Caminho = "/uploads/teste.pdf"
            };

            var command = new SalvarArquivoCommand(arquivo);

            repositorio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Arquivo>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositorio.Verify(r => r.SalvarAsync(It.Is<Dominio.Entities.Arquivo>(a => a == arquivo)), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var arquivo = new Dominio.Entities.Arquivo
            {
                Id = 1,
                Nome = "teste.pdf",
                Caminho = "/uploads/teste.pdf"
            };

            var command = new SalvarArquivoCommand(arquivo);

            repositorio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Arquivo>()))
                .ThrowsAsync(new Exception("Erro ao salvar arquivo"));

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar arquivo", exception.Message);
            repositorio.Verify(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Arquivo>()), Times.Once);
        }
    }
}

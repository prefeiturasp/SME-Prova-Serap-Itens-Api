using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Alternativa
{
    public class SalvarAlternativaCommandHandlerTeste
    {
        private readonly Mock<IRepositorioAlternativa> repositorio;
        private readonly SalvarAlternativaCommandHandler handler;

        public SalvarAlternativaCommandHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAlternativa>();
            handler = new SalvarAlternativaCommandHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new SalvarAlternativaCommandHandler(null));
            Assert.Equal("repositorioAlternativa", exception.ParamName);
        }

        [Fact]
        public async Task Deve_Salvar_Alternativa_Com_Sucesso()
        {
            var alternativa = new Dominio.Entities.Alternativa
            {
                Id = 1,
                Descricao = "Alternativa A",
                Correta = true
            };

            var command = new SalvarAlternativaCommand(alternativa);

            repositorio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Alternativa>()))
                .ReturnsAsync(1);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.Equal(1, result);
            repositorio.Verify(r => r.SalvarAsync(It.Is<Dominio.Entities.Alternativa>(a => a == alternativa)), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Repositorio_Falhar()
        {
            var alternativa = new Dominio.Entities.Alternativa
            {
                Id = 1,
                Descricao = "Alternativa A",
                Correta = true
            };

            var command = new SalvarAlternativaCommand(alternativa);

            repositorio
                .Setup(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Alternativa>()))
                .ThrowsAsync(new Exception("Erro ao salvar alternativa"));

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Erro ao salvar alternativa", exception.Message);
            repositorio.Verify(r => r.SalvarAsync(It.IsAny<Dominio.Entities.Alternativa>()), Times.Once);
        }
    }
}

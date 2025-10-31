using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Autenticacao
{
    public class RemoverCodigoValidacaoAutenticacaoCommandHandlerTeste
    {
        private readonly Mock<IRepositorioCache> repositorioCache;
        private readonly RemoverCodigoValidacaoAutenticacaoCommandHandler handler;

        public RemoverCodigoValidacaoAutenticacaoCommandHandlerTeste()
        {
            repositorioCache = new Mock<IRepositorioCache>();
            handler = new RemoverCodigoValidacaoAutenticacaoCommandHandler(repositorioCache.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new RemoverCodigoValidacaoAutenticacaoCommandHandler(null));
            Assert.Equal("repositorioCache", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Remover_Codigo_E_Retornar_True()
        {
            var codigo = Guid.NewGuid().ToString();
            var command = new RemoverCodigoValidacaoAutenticacaoCommand(codigo);

            repositorioCache
                .Setup(r => r.RemoverRedisAsync(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result);

            repositorioCache.Verify(r =>
                r.RemoverRedisAsync(It.Is<string>(chave => chave.Contains(codigo.ToString()))),
                Times.Once);
        }
    }
}

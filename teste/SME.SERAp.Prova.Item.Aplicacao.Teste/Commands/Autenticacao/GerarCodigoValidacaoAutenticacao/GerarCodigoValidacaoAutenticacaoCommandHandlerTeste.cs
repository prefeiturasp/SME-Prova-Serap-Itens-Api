using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Autenticacao
{
    public class GerarCodigoValidacaoAutenticacaoCommandHandlerTeste
    {
        private readonly Mock<IRepositorioCache> repositorioCache;
        private readonly GerarCodigoValidacaoAutenticacaoCommandHandler handler;

        public GerarCodigoValidacaoAutenticacaoCommandHandlerTeste()
        {
            repositorioCache = new Mock<IRepositorioCache>();
            handler = new GerarCodigoValidacaoAutenticacaoCommandHandler(repositorioCache.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Repositorio_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new GerarCodigoValidacaoAutenticacaoCommandHandler(null));
            Assert.Equal("repositorioCache", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Gerar_Codigo_E_Salvar_No_Cache()
        {
            var usuarioPermissaoDto = new UsuarioPermissaoDto { Login = "Teste" };
            var command = new GerarCodigoValidacaoAutenticacaoCommand(usuarioPermissaoDto);

            repositorioCache
                .Setup(r => r.SalvarRedisAsync(It.IsAny<string>(), usuarioPermissaoDto, It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(Guid.TryParse(result.Codigo, out _));

            repositorioCache.Verify(r =>
                r.SalvarRedisAsync(It.Is<string>(chave => chave.Contains(result.Codigo)), usuarioPermissaoDto, It.IsAny<int>()),
                Times.Once);
        }
    }
}

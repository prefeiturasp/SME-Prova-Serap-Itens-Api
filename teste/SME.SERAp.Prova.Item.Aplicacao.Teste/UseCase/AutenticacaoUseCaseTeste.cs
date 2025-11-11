using MediatR;
using Moq;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class AutenticacaoUseCaseTeste : IDisposable
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly AutenticacaoUseCase useCase;
        private const string ChaveEnvironmentVariableName = "ChaveSerapProvaApi";
        private const string LoginValido = "usuario.teste";
        private const string PerfilValido = "550e8400-e29b-41d4-a716-446655440000";
        private const string ChaveApiValida = "chave-api-teste-123";

        public AutenticacaoUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new AutenticacaoUseCase(mediatorMock.Object);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
        }

        private AutenticacaoDto ObterAutenticacaoDtoValida()
        {
            return new AutenticacaoDto
            {
                Login = LoginValido,
                Perfil = PerfilValido,
                ChaveApi = ChaveApiValida
            };
        }

        private UsuarioPermissaoDto ObterUsuarioPermissaoMock()
        {
            return new UsuarioPermissaoDto(
                login: LoginValido,
                nome: "Usuário Teste",
                grupo: PerfilValido,
                permiteConsultar: true,
                permiteInserir: true,
                permiteAlterar: true,
                permiteExcluir: false
            );
        }

        private AutenticacaoValidarDto ObterAutenticacaoValidarDtoMock()
        {
            return new AutenticacaoValidarDto("codigo-validacao-123");
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new AutenticacaoUseCase(null));
        }

        [Fact]
        public async Task Deve_Autenticar_Com_Sucesso_Quando_ChaveApi_Nao_Configurada()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            Assert.Equal("codigo-validacao-123", resultado.Codigo);

            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(
                q => q.Login == LoginValido && q.GrupoId == Guid.Parse(PerfilValido)),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<GerarCodigoValidacaoAutenticacaoCommand>(
                c => c.UsuarioPermissaoDto == usuarioPermissao),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Autenticar_Com_Sucesso_Quando_ChaveApi_Configurada_E_Valida()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, ChaveApiValida);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            Assert.Equal("codigo-validacao-123", resultado.Codigo);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_ChaveApi_Invalida()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, ChaveApiValida);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.ChaveApi = "chave-invalida";

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoDto));

            Assert.Contains("chave-invalida", exception.Message);
            Assert.Contains("inválida", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_ChaveApi_Vazia()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, ChaveApiValida);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.ChaveApi = string.Empty;

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoDto));

            Assert.Contains("inválida", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_ChaveApi_Null()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, ChaveApiValida);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.ChaveApi = null;

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoDto));

            Assert.Contains("inválida", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_UsuarioPermissao_Null()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((UsuarioPermissaoDto)null);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoDto));

            Assert.Contains(LoginValido, exception.Message);
            Assert.Contains(PerfilValido, exception.Message);
            Assert.Contains("inválido", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Converter_Perfil_String_Para_Guid_Corretamente()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var guidEsperado = Guid.Parse(PerfilValido);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(
                q => q.GrupoId == guidEsperado),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Gerar_Novo_Guid_Quando_Perfil_For_Null()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.Perfil = null;
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Gerar_Novo_Guid_Quando_Perfil_For_String_Vazia()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.Perfil = string.Empty;
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Aceitar_ChaveApi_Configurada_Como_String_Vazia()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, string.Empty);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Aceitar_ChaveApi_Configurada_Com_Espacos()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, "   ");
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Validar_ChaveApi_Com_Case_Sensitive()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, "ChaveApiTESTE");
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.ChaveApi = "chaveapiteste";

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoDto));

            Assert.Contains("inválida", exception.Message);
            Assert.Equal(401, exception.StatusCode);
        }

        [Fact]
        public async Task Deve_Passar_UsuarioPermissaoDto_Para_Command()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<GerarCodigoValidacaoAutenticacaoCommand>(
                c => c.UsuarioPermissaoDto.Login == LoginValido &&
                     c.UsuarioPermissaoDto.Nome == "Usuário Teste" &&
                     c.UsuarioPermissaoDto.Grupo == PerfilValido &&
                     c.UsuarioPermissaoDto.PermiteConsultar == true &&
                     c.UsuarioPermissaoDto.PermiteInserir == true &&
                     c.UsuarioPermissaoDto.PermiteAlterar == true &&
                     c.UsuarioPermissaoDto.PermiteExcluir == false),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Aceitar_Login_Com_Caracteres_Especiais()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            autenticacaoDto.Login = "usuario.teste@exemplo.com.br";
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoValidar = ObterAutenticacaoValidarDtoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(
                q => q.Login == "usuario.teste@exemplo.com.br"),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Codigo_Gerado_Pelo_Command()
        {
            Environment.SetEnvironmentVariable(ChaveEnvironmentVariableName, null);
            var autenticacaoDto = ObterAutenticacaoDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var codigoEsperado = "codigo-especifico-xyz-789";
            var autenticacaoValidar = new AutenticacaoValidarDto(codigoEsperado);

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorLoginGrupoIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<GerarCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoValidar);

            var resultado = await useCase.Executar(autenticacaoDto);

            Assert.NotNull(resultado);
            Assert.Equal(codigoEsperado, resultado.Codigo);
        }
    }
}
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
    public class AutenticacaoValidarUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly AutenticacaoValidarUseCase useCase;
        private const string CodigoValido = "codigo-validacao-123";
        private const string LoginValido = "usuario.teste";
        private const string TokenValido = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

        public AutenticacaoValidarUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new AutenticacaoValidarUseCase(mediatorMock.Object);
        }

        private AutenticacaoValidarDto ObterAutenticacaoValidarDtoValida()
        {
            return new AutenticacaoValidarDto(CodigoValido);
        }

        private UsuarioPermissaoDto ObterUsuarioPermissaoMock()
        {
            return new UsuarioPermissaoDto(
                login: LoginValido,
                nome: "Usuário Teste",
                grupo: "550e8400-e29b-41d4-a716-446655440000",
                permiteConsultar: true,
                permiteInserir: true,
                permiteAlterar: true,
                permiteExcluir: false
            );
        }

        private AutenticacaoRetornoDto ObterAutenticacaoRetornoMock()
        {
            return new AutenticacaoRetornoDto(
                token: TokenValido,
                dataHoraExpiracao: DateTime.Now.AddHours(1)
            );
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new AutenticacaoValidarUseCase(null));
        }

        [Fact]
        public async Task Deve_Validar_Com_Sucesso_E_Retornar_Token()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            Assert.Equal(TokenValido, resultado.Token);
            Assert.NotEqual(default(DateTime), resultado.DataHoraExpiracao);

            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == CodigoValido),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<RemoverCodigoValidacaoAutenticacaoCommand>(
                c => c.Codigo == CodigoValido),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterTokenJwtQuery>(
                q => q.UsuarioPermissaoDto == usuarioPermissao),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_UsuarioPermissao_For_Null()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((UsuarioPermissaoDto)null);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoValidarDto));

            Assert.Equal("Código inválido", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == CodigoValido),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(),
                It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterTokenJwtQuery>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Remover_Codigo_Validacao_Antes_De_Gerar_Token()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();
            var ordemChamadas = new System.Collections.Generic.List<string>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao)
                        .Callback(() => ordemChamadas.Add("ObterPermissao"));
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true)
                        .Callback(() => ordemChamadas.Add("RemoverCodigo"));
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno)
                        .Callback(() => ordemChamadas.Add("ObterToken"));

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            Assert.Equal(3, ordemChamadas.Count);
            Assert.Equal("ObterPermissao", ordemChamadas[0]);
            Assert.Equal("RemoverCodigo", ordemChamadas[1]);
            Assert.Equal("ObterToken", ordemChamadas[2]);
        }

        [Fact]
        public async Task Deve_Validar_Com_Codigo_Vazio()
        {
            var autenticacaoValidarDto = new AutenticacaoValidarDto(string.Empty);
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == string.Empty),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Validar_Com_Codigo_Null()
        {
            var autenticacaoValidarDto = new AutenticacaoValidarDto(null);
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == null),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Validar_Com_Codigo_Longo()
        {
            var codigoLongo = new string('A', 1000);
            var autenticacaoValidarDto = new AutenticacaoValidarDto(codigoLongo);
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == codigoLongo),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Validar_Com_Codigo_Com_Caracteres_Especiais()
        {
            var codigoEspecial = "codigo@#$%&*()_+-=[]{}|;':\"<>,.?/\\~`";
            var autenticacaoValidarDto = new AutenticacaoValidarDto(codigoEspecial);
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(
                q => q.Codigo == codigoEspecial),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Token_Com_DataHoraExpiracao_Futura()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var dataExpiracao = DateTime.Now.AddHours(2);
            var autenticacaoRetorno = new AutenticacaoRetornoDto(TokenValido, dataExpiracao);

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            Assert.Equal(dataExpiracao, resultado.DataHoraExpiracao);
            Assert.True(resultado.DataHoraExpiracao > DateTime.Now);
        }

        [Fact]
        public async Task Deve_Passar_UsuarioPermissaoDto_Completo_Para_ObterTokenJwtQuery()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = new UsuarioPermissaoDto(
                login: "usuario.especial",
                nome: "Usuário Especial Teste",
                grupo: "123e4567-e89b-12d3-a456-426614174000",
                permiteConsultar: true,
                permiteInserir: false,
                permiteAlterar: false,
                permiteExcluir: true
            );
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterTokenJwtQuery>(
                q => q.UsuarioPermissaoDto.Login == "usuario.especial" &&
                     q.UsuarioPermissaoDto.Nome == "Usuário Especial Teste" &&
                     q.UsuarioPermissaoDto.Grupo == "123e4567-e89b-12d3-a456-426614174000" &&
                     q.UsuarioPermissaoDto.PermiteConsultar == true &&
                     q.UsuarioPermissaoDto.PermiteInserir == false &&
                     q.UsuarioPermissaoDto.PermiteAlterar == false &&
                     q.UsuarioPermissaoDto.PermiteExcluir == true),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Remover_Codigo_Mesmo_Que_Retorno_Seja_False()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<RemoverCodigoValidacaoAutenticacaoCommand>(
                c => c.Codigo == CodigoValido),
                It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterTokenJwtQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Token_Vazio_Se_ObterTokenJwtQuery_Retornar_Token_Vazio()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = new AutenticacaoRetornoDto(string.Empty, DateTime.Now.AddHours(1));

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            Assert.Equal(string.Empty, resultado.Token);
        }

        [Fact]
        public async Task Deve_Retornar_Token_Null_Se_ObterTokenJwtQuery_Retornar_Token_Null()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = new AutenticacaoRetornoDto(null, DateTime.Now.AddHours(1));

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Token);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Com_StatusCode_401()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((UsuarioPermissaoDto)null);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(
                () => useCase.Executar(autenticacaoValidarDto));

            Assert.Equal(401, exception.StatusCode);
        }

        [Fact]
        public async Task Deve_Validar_Com_UsuarioPermissao_Com_Todas_Permissoes_False()
        {
            var autenticacaoValidarDto = ObterAutenticacaoValidarDtoValida();
            var usuarioPermissao = new UsuarioPermissaoDto(
                login: "usuario.sem.permissoes",
                nome: "Usuário Sem Permissões",
                grupo: "grupo-teste",
                permiteConsultar: false,
                permiteInserir: false,
                permiteAlterar: false,
                permiteExcluir: false
            );
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterPermissaoUsuarioPorCodigoValidacaoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverCodigoValidacaoAutenticacaoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoValidarDto);

            Assert.NotNull(resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterTokenJwtQuery>(
                q => q.UsuarioPermissaoDto.PermiteConsultar == false &&
                     q.UsuarioPermissaoDto.PermiteInserir == false &&
                     q.UsuarioPermissaoDto.PermiteAlterar == false &&
                     q.UsuarioPermissaoDto.PermiteExcluir == false),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
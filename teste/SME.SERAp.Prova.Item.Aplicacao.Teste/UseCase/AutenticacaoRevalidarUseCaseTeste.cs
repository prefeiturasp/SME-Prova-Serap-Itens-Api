using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class AutenticacaoRevalidarUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly AutenticacaoRevalidarUseCase useCase;
        private const string TokenValido = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.token.valido";
        private const string LoginUsuario = "usuario.teste";
        private const string NomeUsuario = "Usuário Teste";
        private const string GrupoUsuario = "Administrador";

        public AutenticacaoRevalidarUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new AutenticacaoRevalidarUseCase(mediatorMock.Object);
        }

        private UsuarioPermissaoDto ObterUsuarioPermissaoMock()
        {
            return new UsuarioPermissaoDto(
                login: LoginUsuario,
                nome: NomeUsuario,
                grupo: GrupoUsuario,
                permiteConsultar: true,
                permiteInserir: true,
                permiteAlterar: true,
                permiteExcluir: false
            );
        }

        private AutenticacaoRetornoDto ObterAutenticacaoRetornoMock()
        {
            return new AutenticacaoRetornoDto(
                token: "novo.token.jwt.gerado",
                dataHoraExpiracao: DateTime.Now.AddHours(2)
            );
        }

        private AutenticacaoRevalidarDto ObterAutenticacaoRevalidarDtoMock()
        {
            return new AutenticacaoRevalidarDto
            {
                Token = TokenValido
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new AutenticacaoRevalidarUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_AutenticacaoRetornoDto_Com_Novo_Token_Em_Caso_De_Sucesso()
        {
            var autenticacaoRevalidarDto = ObterAutenticacaoRevalidarDtoMock();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterInformacoesPorTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(autenticacaoRetorno);

            var resultado = await useCase.Executar(autenticacaoRevalidarDto);

            Assert.NotNull(resultado);
            Assert.Equal(autenticacaoRetorno.Token, resultado.Token);
            Assert.Equal(autenticacaoRetorno.DataHoraExpiracao, resultado.DataHoraExpiracao);

            mediatorMock.Verify(m => m.Send(It.Is<ObterInformacoesPorTokenJwtQuery>(q => q.Token == TokenValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterTokenJwtQuery>(q => q.UsuarioPermissaoDto == usuarioPermissao), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_For_Invalido()
        {
            var autenticacaoRevalidarDto = ObterAutenticacaoRevalidarDtoMock();
            UsuarioPermissaoDto usuarioNulo = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterInformacoesPorTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioNulo);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                useCase.Executar(autenticacaoRevalidarDto));

            Assert.Equal("Token inválido", exception.Message);
            Assert.Equal(401, exception.StatusCode);

            mediatorMock.Verify(m => m.Send(It.Is<ObterInformacoesPorTokenJwtQuery>(q => q.Token == TokenValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Repassar_UsuarioPermissaoDto_Correto_Para_Gerar_Novo_Token()
        {
            var autenticacaoRevalidarDto = ObterAutenticacaoRevalidarDtoMock();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            UsuarioPermissaoDto usuarioCapturado = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterInformacoesPorTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .Callback<IRequest<AutenticacaoRetornoDto>, CancellationToken>((query, token) =>
                        {
                            usuarioCapturado = ((ObterTokenJwtQuery)query).UsuarioPermissaoDto;
                        })
                        .ReturnsAsync(autenticacaoRetorno);

            await useCase.Executar(autenticacaoRevalidarDto);

            Assert.NotNull(usuarioCapturado);
            Assert.Equal(LoginUsuario, usuarioCapturado.Login);
            Assert.Equal(NomeUsuario, usuarioCapturado.Nome);
            Assert.Equal(GrupoUsuario, usuarioCapturado.Grupo);
            Assert.True(usuarioCapturado.PermiteConsultar);
            Assert.True(usuarioCapturado.PermiteInserir);
            Assert.True(usuarioCapturado.PermiteAlterar);
            Assert.False(usuarioCapturado.PermiteExcluir);
        }

        [Fact]
        public async Task Deve_Validar_Token_Antes_De_Tentar_Gerar_Novo_Token()
        {
            var autenticacaoRevalidarDto = ObterAutenticacaoRevalidarDtoMock();
            var usuarioPermissao = ObterUsuarioPermissaoMock();
            var autenticacaoRetorno = ObterAutenticacaoRetornoMock();

            var ordemChamadas = new System.Collections.Generic.List<string>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterInformacoesPorTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .Callback(() => ordemChamadas.Add("ValidarToken"))
                        .ReturnsAsync(usuarioPermissao);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterTokenJwtQuery>(), It.IsAny<CancellationToken>()))
                        .Callback(() => ordemChamadas.Add("GerarNovoToken"))
                        .ReturnsAsync(autenticacaoRetorno);

            await useCase.Executar(autenticacaoRevalidarDto);

            Assert.Equal(2, ordemChamadas.Count);
            Assert.Equal("ValidarToken", ordemChamadas[0]);
            Assert.Equal("GerarNovoToken", ordemChamadas[1]);
        }
    }
}
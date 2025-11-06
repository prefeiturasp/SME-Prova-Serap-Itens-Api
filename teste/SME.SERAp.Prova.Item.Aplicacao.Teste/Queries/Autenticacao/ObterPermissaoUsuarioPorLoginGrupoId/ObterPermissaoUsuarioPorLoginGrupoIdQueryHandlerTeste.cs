using Moq;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Autenticacao
{
    public class ObterPermissaoUsuarioPorLoginGrupoIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioUsuario> repositorioUsuario;
        private readonly ObterPermissaoUsuarioPorLoginGrupoIdQueryHandler handler;

        public ObterPermissaoUsuarioPorLoginGrupoIdQueryHandlerTeste()
        {
            repositorioUsuario = new Mock<IRepositorioUsuario>();
            handler = new ObterPermissaoUsuarioPorLoginGrupoIdQueryHandler(repositorioUsuario.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_RepositorioUsuario_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterPermissaoUsuarioPorLoginGrupoIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Quando_Encontrado()
        {
            var login = "user123";
            var grupoId = Guid.NewGuid();
            var usuarioEsperado = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(usuarioEsperado.Login, resultado.Login);
            Assert.Equal(usuarioEsperado.Nome, resultado.Nome);
            Assert.Equal(usuarioEsperado.Grupo, resultado.Grupo);
            Assert.Equal(usuarioEsperado.PermiteConsultar, resultado.PermiteConsultar);
            Assert.Equal(usuarioEsperado.PermiteInserir, resultado.PermiteInserir);
            Assert.Equal(usuarioEsperado.PermiteAlterar, resultado.PermiteAlterar);
            Assert.Equal(usuarioEsperado.PermiteExcluir, resultado.PermiteExcluir);
            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_Usuario()
        {
            var login = "usuario_inexistente";
            var grupoId = Guid.NewGuid();

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ReturnsAsync((UsuarioPermissaoDto)null);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            var login = "user123";
            var grupoId = Guid.NewGuid();

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Com_Todas_Permissoes_Verdadeiras()
        {
            var login = "admin";
            var grupoId = Guid.NewGuid();
            var usuarioEsperado = new UsuarioPermissaoDto(
                "admin",
                "Administrador",
                "SuperAdmin",
                true,
                true,
                true,
                true
            );

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.True(resultado.PermiteConsultar);
            Assert.True(resultado.PermiteInserir);
            Assert.True(resultado.PermiteAlterar);
            Assert.True(resultado.PermiteExcluir);
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Com_Todas_Permissoes_Falsas()
        {
            var login = "guest";
            var grupoId = Guid.NewGuid();
            var usuarioEsperado = new UsuarioPermissaoDto(
                "guest",
                "Convidado",
                "Visitante",
                false,
                false,
                false,
                false
            );

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.False(resultado.PermiteConsultar);
            Assert.False(resultado.PermiteInserir);
            Assert.False(resultado.PermiteAlterar);
            Assert.False(resultado.PermiteExcluir);
        }

        [Fact]
        public async Task Deve_Processar_Diferentes_Combinacoes_Login_GrupoId()
        {
            var login1 = "user1";
            var grupoId1 = Guid.NewGuid();
            var usuario1 = new UsuarioPermissaoDto("user1", "Usuario 1", "Grupo1", true, false, false, false);

            var login2 = "user2";
            var grupoId2 = Guid.NewGuid();
            var usuario2 = new UsuarioPermissaoDto("user2", "Usuario 2", "Grupo2", false, true, false, false);

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login1, grupoId1))
                .ReturnsAsync(usuario1);

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login2, grupoId2))
                .ReturnsAsync(usuario2);

            var query1 = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login1, grupoId1);
            var resultado1 = await handler.Handle(query1, CancellationToken.None);

            var query2 = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login2, grupoId2);
            var resultado2 = await handler.Handle(query2, CancellationToken.None);

            Assert.NotNull(resultado1);
            Assert.Equal("user1", resultado1.Login);
            Assert.Equal("Grupo1", resultado1.Grupo);

            Assert.NotNull(resultado2);
            Assert.Equal("user2", resultado2.Login);
            Assert.Equal("Grupo2", resultado2.Grupo);

            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login1, grupoId1), Times.Once);
            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login2, grupoId2), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Login_Existe_Mas_GrupoId_Nao_Associado()
        {
            var login = "user123";
            var grupoIdInexistente = Guid.NewGuid();

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoIdInexistente))
                .ReturnsAsync((UsuarioPermissaoDto)null);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoIdInexistente);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioUsuario.Verify(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoIdInexistente), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Com_Permissoes_Parciais()
        {
            var login = "moderador";
            var grupoId = Guid.NewGuid();
            var usuarioEsperado = new UsuarioPermissaoDto(
                "moderador",
                "Moderador Sistema",
                "Moderadores",
                true,
                true,
                false,
                false
            );

            repositorioUsuario
                .Setup(r => r.ObterPermissaoUsuarioPorLoginGrupoIdAsync(login, grupoId))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorLoginGrupoIdQuery(login, grupoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.True(resultado.PermiteConsultar);
            Assert.True(resultado.PermiteInserir);
            Assert.False(resultado.PermiteAlterar);
            Assert.False(resultado.PermiteExcluir);
        }
    }
}

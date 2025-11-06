using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Cache;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Autenticacao
{
    public class ObterPermissaoUsuarioPorCodigoValidacaoQueryHandlerTeste
    {
        private readonly Mock<IRepositorioCache> repositorioCache;
        private readonly ObterPermissaoUsuarioPorCodigoValidacaoQueryHandler handler;

        public ObterPermissaoUsuarioPorCodigoValidacaoQueryHandlerTeste()
        {
            repositorioCache = new Mock<IRepositorioCache>();
            handler = new ObterPermissaoUsuarioPorCodigoValidacaoQueryHandler(repositorioCache.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_RepositorioCache_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterPermissaoUsuarioPorCodigoValidacaoQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Quando_Encontrado_No_Cache()
        {
            var codigoValidacao = "ABC123XYZ";
            var usuarioEsperado = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(usuarioEsperado.Login, resultado.Login);
            Assert.Equal(usuarioEsperado.Nome, resultado.Nome);
            Assert.Equal(usuarioEsperado.Grupo, resultado.Grupo);
            Assert.Equal(usuarioEsperado.PermiteConsultar, resultado.PermiteConsultar);
            Assert.Equal(usuarioEsperado.PermiteInserir, resultado.PermiteInserir);
            Assert.Equal(usuarioEsperado.PermiteAlterar, resultado.PermiteAlterar);
            Assert.Equal(usuarioEsperado.PermiteExcluir, resultado.PermiteExcluir);
            repositorioCache.Verify(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_No_Cache()
        {
            var codigoValidacao = "CODIGO_INEXISTENTE";
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada))
                .ReturnsAsync((UsuarioPermissaoDto)null);

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioCache.Verify(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_RepositorioCache_Falhar()
        {
            var codigoValidacao = "ABC123";
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada))
                .ThrowsAsync(new InvalidOperationException("Falha ao acessar o cache"));

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha ao acessar o cache", exception.Message);
            repositorioCache.Verify(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada), Times.Once);
        }

        [Fact]
        public async Task Deve_Utilizar_Chave_Correta_Do_Cache()
        {
            var codigoValidacao = "TESTE456";
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);
            string chaveUtilizada = null;

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(It.IsAny<string>()))
                .Callback<string>(chave => chaveUtilizada = chave)
                .ReturnsAsync((UsuarioPermissaoDto)null);

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);
            await handler.Handle(query, CancellationToken.None);

            Assert.Equal(chaveEsperada, chaveUtilizada);
            repositorioCache.Verify(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Com_Todas_Permissoes_Verdadeiras()
        {
            var codigoValidacao = "ADMIN999";
            var usuarioEsperado = new UsuarioPermissaoDto(
                "admin",
                "Administrador",
                "SuperAdmin",
                true,
                true,
                true,
                true
            );
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);
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
            var codigoValidacao = "GUEST001";
            var usuarioEsperado = new UsuarioPermissaoDto(
                "guest",
                "Convidado",
                "Visitante",
                false,
                false,
                false,
                false
            );
            var chaveEsperada = CacheChave.ObterChave(CacheChave.Autenticacao, codigoValidacao);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(chaveEsperada))
                .ReturnsAsync(usuarioEsperado);

            var query = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigoValidacao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.False(resultado.PermiteConsultar);
            Assert.False(resultado.PermiteInserir);
            Assert.False(resultado.PermiteAlterar);
            Assert.False(resultado.PermiteExcluir);
        }

        [Fact]
        public async Task Deve_Processar_Codigos_Validacao_Diferentes_Corretamente()
        {
            var codigo1 = "CODIGO_A";
            var codigo2 = "CODIGO_B";
            var usuario1 = new UsuarioPermissaoDto("user1", "Usuario 1", "Grupo1", true, false, false, false);
            var usuario2 = new UsuarioPermissaoDto("user2", "Usuario 2", "Grupo2", false, true, false, false);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(CacheChave.ObterChave(CacheChave.Autenticacao, codigo1)))
                .ReturnsAsync(usuario1);

            repositorioCache
                .Setup(r => r.ObterRedisAsync<UsuarioPermissaoDto>(CacheChave.ObterChave(CacheChave.Autenticacao, codigo2)))
                .ReturnsAsync(usuario2);

            var query1 = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigo1);
            var resultado1 = await handler.Handle(query1, CancellationToken.None);

            var query2 = new ObterPermissaoUsuarioPorCodigoValidacaoQuery(codigo2);
            var resultado2 = await handler.Handle(query2, CancellationToken.None);

            Assert.NotNull(resultado1);
            Assert.Equal("user1", resultado1.Login);
            Assert.NotNull(resultado2);
            Assert.Equal("user2", resultado2.Login);
        }
    }
}

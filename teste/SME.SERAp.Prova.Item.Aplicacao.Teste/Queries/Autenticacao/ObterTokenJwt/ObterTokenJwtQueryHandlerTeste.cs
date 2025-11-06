using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Autenticacao
{
    public class ObterTokenJwtQueryHandlerTeste
    {
        private readonly JwtOptions jwtOptions;
        private readonly ObterTokenJwtQueryHandler handler;
        private readonly string issuerSigningKey;
        private readonly string issuer;
        private readonly string audience;
        private readonly string expiresInMinutes;

        public ObterTokenJwtQueryHandlerTeste()
        {
            issuerSigningKey = "ChaveSecretaSuperSeguraComPeloMenos32Caracteres123456";
            issuer = "TestIssuer";
            audience = "TestAudience";
            expiresInMinutes = "60";

            jwtOptions = new JwtOptions
            {
                IssuerSigningKey = issuerSigningKey,
                Issuer = issuer,
                Audience = audience,
                ExpiresInMinutes = expiresInMinutes
            };

            handler = new ObterTokenJwtQueryHandler(jwtOptions);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_JwtOptions_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterTokenJwtQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Claims_Corretas()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Token);
            Assert.NotEmpty(resultado.Token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            Assert.Equal("user123", token.Claims.FirstOrDefault(c => c.Type == "LOGIN")?.Value);
            Assert.Equal("Usuario Teste", token.Claims.FirstOrDefault(c => c.Type == "USUARIO")?.Value);
            Assert.Equal("Admin", token.Claims.FirstOrDefault(c => c.Type == "GRUPO")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITECONSULTAR")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITEINSERIR")?.Value);
            Assert.Equal("False", token.Claims.FirstOrDefault(c => c.Type == "PERMITEALTERAR")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITEEXCLUIR")?.Value);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Issuer_Correto()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            Assert.Equal(issuer, token.Issuer);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Audience_Correto()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            Assert.Contains(audience, token.Audiences);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_DataHoraExpiracao_Correta()
        {
            var dataHoraAntes = DateTime.Now;

            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var dataHoraDepois = DateTime.Now;
            var minutosExpiracao = double.Parse(expiresInMinutes);
            Assert.True(resultado.DataHoraExpiracao >= dataHoraAntes.AddMinutes(minutosExpiracao));
            Assert.True(resultado.DataHoraExpiracao <= dataHoraDepois.AddMinutes(minutosExpiracao));
        }

        [Fact]
        public async Task Deve_Gerar_Token_Valido_Que_Pode_Ser_Validado()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey)),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(resultado.Token, validationParameters, out SecurityToken validatedToken);

            Assert.NotNull(principal);
            Assert.NotNull(validatedToken);
            Assert.IsType<JwtSecurityToken>(validatedToken);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Todas_Permissoes_Verdadeiras()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "admin",
                "Administrador",
                "SuperAdmin",
                true,
                true,
                true,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITECONSULTAR")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITEINSERIR")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITEALTERAR")?.Value);
            Assert.Equal("True", token.Claims.FirstOrDefault(c => c.Type == "PERMITEEXCLUIR")?.Value);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Todas_Permissoes_Falsas()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "guest",
                "Convidado",
                "Visitante",
                false,
                false,
                false,
                false
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            Assert.Equal("False", token.Claims.FirstOrDefault(c => c.Type == "PERMITECONSULTAR")?.Value);
            Assert.Equal("False", token.Claims.FirstOrDefault(c => c.Type == "PERMITEINSERIR")?.Value);
            Assert.Equal("False", token.Claims.FirstOrDefault(c => c.Type == "PERMITEALTERAR")?.Value);
            Assert.Equal("False", token.Claims.FirstOrDefault(c => c.Type == "PERMITEEXCLUIR")?.Value);
        }

        [Fact]
        public async Task Deve_Gerar_Tokens_Diferentes_Para_Usuarios_Diferentes()
        {
            var usuario1 = new UsuarioPermissaoDto("user1", "Usuario 1", "Grupo1", true, false, false, false);
            var usuario2 = new UsuarioPermissaoDto("user2", "Usuario 2", "Grupo2", false, true, false, false);

            var query1 = new ObterTokenJwtQuery(usuario1);
            var resultado1 = await handler.Handle(query1, CancellationToken.None);

            var query2 = new ObterTokenJwtQuery(usuario2);
            var resultado2 = await handler.Handle(query2, CancellationToken.None);

            Assert.NotEqual(resultado1.Token, resultado2.Token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token1 = tokenHandler.ReadJwtToken(resultado1.Token);
            var token2 = tokenHandler.ReadJwtToken(resultado2.Token);

            Assert.Equal("user1", token1.Claims.FirstOrDefault(c => c.Type == "LOGIN")?.Value);
            Assert.Equal("user2", token2.Claims.FirstOrDefault(c => c.Type == "LOGIN")?.Value);
        }

        [Fact]
        public async Task Deve_Retornar_AutenticacaoRetornoDto_Completo()
        {
            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.IsType<AutenticacaoRetornoDto>(resultado);
            Assert.NotNull(resultado.Token);
            Assert.NotEmpty(resultado.Token);
            Assert.NotEqual(default(DateTime), resultado.DataHoraExpiracao);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_Tempo_Expiracao_Configurado()
        {
            var jwtOptionsCustom = new JwtOptions
            {
                IssuerSigningKey = issuerSigningKey,
                Issuer = issuer,
                Audience = audience,
                ExpiresInMinutes = "30"
            };

            var handlerCustom = new ObterTokenJwtQueryHandler(jwtOptionsCustom);
            var dataHoraAntes = DateTime.Now;

            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handlerCustom.Handle(query, CancellationToken.None);

            var dataHoraDepois = DateTime.Now;
            var dataHoraEsperadaMin = dataHoraAntes.AddMinutes(30);
            var dataHoraEsperadaMax = dataHoraDepois.AddMinutes(30);

            Assert.True(resultado.DataHoraExpiracao >= dataHoraEsperadaMin);
            Assert.True(resultado.DataHoraExpiracao <= dataHoraEsperadaMax);
        }

        [Fact]
        public async Task Deve_Gerar_Token_Com_NotBefore_Valido()
        {
            var dataHoraAntes = DateTime.Now;

            var usuarioPermissao = new UsuarioPermissaoDto(
                "user123",
                "Usuario Teste",
                "Admin",
                true,
                true,
                false,
                true
            );

            var query = new ObterTokenJwtQuery(usuarioPermissao);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(resultado.Token);

            var diferencaEmSegundos = Math.Abs((DateTime.UtcNow - token.ValidFrom).TotalSeconds);
            Assert.True(diferencaEmSegundos < 10, $"ValidFrom deveria estar próximo do horário atual. Diferença: {diferencaEmSegundos} segundos");
        }
    }
}

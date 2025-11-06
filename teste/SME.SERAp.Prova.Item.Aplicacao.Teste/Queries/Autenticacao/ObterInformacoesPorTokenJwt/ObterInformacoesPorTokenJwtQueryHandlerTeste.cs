using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Autenticacao
{
    public class ObterInformacoesPorTokenJwtQueryHandlerTeste
    {
        private readonly JwtOptions jwtOptions;
        private readonly ObterInformacoesPorTokenJwtQueryHandler handler;
        private readonly string issuerSigningKey;
        private readonly string issuer;
        private readonly string audience;

        public ObterInformacoesPorTokenJwtQueryHandlerTeste()
        {
            issuerSigningKey = "ChaveSecretaSuperSeguraComPeloMenos32Caracteres123456";
            issuer = "TestIssuer";
            audience = "TestAudience";

            jwtOptions = new JwtOptions
            {
                IssuerSigningKey = issuerSigningKey,
                Issuer = issuer,
                Audience = audience
            };

            handler = new ObterInformacoesPorTokenJwtQueryHandler(jwtOptions);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_JwtOptions_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterInformacoesPorTokenJwtQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Quando_Token_Valido()
        {
            var token = GerarTokenValido("user123", "Usuario Teste", "Admin", true, true, false, true);
            var query = new ObterInformacoesPorTokenJwtQuery(token);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal("user123", resultado.Login);
            Assert.Equal("Usuario Teste", resultado.Nome);
            Assert.Equal("Admin", resultado.Grupo);
            Assert.True(resultado.PermiteConsultar);
            Assert.True(resultado.PermiteInserir);
            Assert.False(resultado.PermiteAlterar);
            Assert.True(resultado.PermiteExcluir);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_Invalido()
        {
            var query = new ObterInformacoesPorTokenJwtQuery("token.invalido.aqui");

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Token inválido", exception.Message);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_Sem_Claims_Obrigatorias()
        {
            var token = GerarTokenSemTodasClaims();
            var query = new ObterInformacoesPorTokenJwtQuery(token);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Token inválido", exception.Message);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_Com_Issuer_Invalido()
        {
            var token = GerarTokenComIssuerInvalido();
            var query = new ObterInformacoesPorTokenJwtQuery(token);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Token inválido", exception.Message);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_Com_Audience_Invalido()
        {
            var token = GerarTokenComAudienceInvalido();
            var query = new ObterInformacoesPorTokenJwtQuery(token);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Token inválido", exception.Message);
        }

        [Fact]
        public async Task Deve_Lancar_NaoAutorizadoException_Quando_Token_Vazio()
        {
            var query = new ObterInformacoesPorTokenJwtQuery(string.Empty);

            var exception = await Assert.ThrowsAsync<NaoAutorizadoException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Token inválido", exception.Message);
        }

        [Fact]
        public async Task Deve_Retornar_UsuarioPermissaoDto_Com_Todas_Permissoes_Verdadeiras()
        {
            var token = GerarTokenValido("admin", "Administrador", "SuperAdmin", true, true, true, true);
            var query = new ObterInformacoesPorTokenJwtQuery(token);

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
            var token = GerarTokenValido("guest", "Convidado", "Visitante", false, false, false, false);
            var query = new ObterInformacoesPorTokenJwtQuery(token);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.False(resultado.PermiteConsultar);
            Assert.False(resultado.PermiteInserir);
            Assert.False(resultado.PermiteAlterar);
            Assert.False(resultado.PermiteExcluir);
        }

        private string GerarTokenValido(string login, string usuario, string grupo,
            bool permiteConsultar, bool permiteInserir, bool permiteAlterar, bool permiteExcluir)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("LOGIN", login),
            new Claim("USUARIO", usuario),
            new Claim("GRUPO", grupo),
            new Claim("PERMITECONSULTAR", permiteConsultar.ToString()),
            new Claim("PERMITEINSERIR", permiteInserir.ToString()),
            new Claim("PERMITEALTERAR", permiteAlterar.ToString()),
            new Claim("PERMITEEXCLUIR", permiteExcluir.ToString())
        };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GerarTokenSemTodasClaims()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("LOGIN", "user123"),
                new Claim("USUARIO", "Usuario Teste")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GerarTokenComIssuerInvalido()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("LOGIN", "user123"),
                new Claim("USUARIO", "Usuario Teste"),
                new Claim("GRUPO", "Admin"),
                new Claim("PERMITECONSULTAR", "true"),
                new Claim("PERMITEINSERIR", "true"),
                new Claim("PERMITEALTERAR", "true"),
                new Claim("PERMITEEXCLUIR", "true")
            };

            var token = new JwtSecurityToken(
                issuer: "IssuerInvalido",
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GerarTokenComAudienceInvalido()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("LOGIN", "user123"),
                new Claim("USUARIO", "Usuario Teste"),
                new Claim("GRUPO", "Admin"),
                new Claim("PERMITECONSULTAR", "true"),
                new Claim("PERMITEINSERIR", "true"),
                new Claim("PERMITEALTERAR", "true"),
                new Claim("PERMITEEXCLUIR", "true")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: "AudienceInvalido",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

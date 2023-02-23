using MediatR;
using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterInformacoesPorTokenJwtQueryHandler : IRequestHandler<ObterInformacoesPorTokenJwtQuery, UsuarioPermissaoDto>
    {
        private readonly JwtOptions jwtOptions;

        public ObterInformacoesPorTokenJwtQueryHandler(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions ?? throw new System.ArgumentNullException(nameof(jwtOptions));
        }

        public async Task<UsuarioPermissaoDto> Handle(ObterInformacoesPorTokenJwtQuery request, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey));
            var validator = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = key,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateLifetime = false
            };
            try
            {
                if (validator.CanReadToken(request.Token))
                {
                    ClaimsPrincipal principal;
                    principal = validator.ValidateToken(request.Token, validationParameters, out SecurityToken validatedToken);

                    if (principal.HasClaim(c => c.Type == "LOGIN") &&
                        principal.HasClaim(c => c.Type == "USUARIO") &&
                        principal.HasClaim(c => c.Type == "GRUPO") &&
                        principal.HasClaim(c => c.Type == "PERMITECONSULTAR") &&
                        principal.HasClaim(c => c.Type == "PERMITEINSERIR") &&
                        principal.HasClaim(c => c.Type == "PERMITEALTERAR") &&
                        principal.HasClaim(c => c.Type == "PERMITEEXCLUIR"))
                    {
                        var login = principal.Claims.FirstOrDefault(c => c.Type == "LOGIN").Value;
                        var usuario = principal.Claims.FirstOrDefault(c => c.Type == "USUARIO").Value;
                        var grupo = principal.Claims.FirstOrDefault(c => c.Type == "GRUPO").Value;
                        var permiteConsultar = bool.Parse(principal.Claims.FirstOrDefault(c => c.Type == "PERMITECONSULTAR").Value);
                        var permiteInserir = bool.Parse(principal.Claims.FirstOrDefault(c => c.Type == "PERMITEINSERIR").Value);
                        var permiteAlterar = bool.Parse(principal.Claims.FirstOrDefault(c => c.Type == "PERMITEALTERAR").Value);
                        var permiteExcluir = bool.Parse(principal.Claims.FirstOrDefault(c => c.Type == "PERMITEEXCLUIR").Value);

                        return new UsuarioPermissaoDto(login, usuario, grupo, permiteConsultar, permiteInserir, permiteAlterar, permiteExcluir);
                    }
                }

                throw new NaoAutorizadoException("Token inválido");
            }
            catch (Exception)
            {
                throw new NaoAutorizadoException("Token inválido");
            }
        }
    }
}

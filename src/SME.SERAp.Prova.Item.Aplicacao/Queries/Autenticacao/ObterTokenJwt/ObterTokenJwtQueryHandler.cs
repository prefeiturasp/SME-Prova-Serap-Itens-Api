using MediatR;
using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTokenJwtQueryHandler : IRequestHandler<ObterTokenJwtQuery, AutenticacaoRetornoDto>
    {
        private readonly JwtOptions jwtOptions;

        public ObterTokenJwtQueryHandler(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public Task<AutenticacaoRetornoDto> Handle(ObterTokenJwtQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim("LOGIN", request.UsuarioPermissaoDto.Login),
                new Claim("USUARIO", request.UsuarioPermissaoDto.Nome),
                new Claim("GRUPO", request.UsuarioPermissaoDto.Grupo),
                new Claim("PERMITECONSULTAR", request.UsuarioPermissaoDto.PermiteConsultar.ToString()),
                new Claim("PERMITEINSERIR", request.UsuarioPermissaoDto.PermiteInserir.ToString()),
                new Claim("PERMITEALTERAR", request.UsuarioPermissaoDto.PermiteAlterar.ToString()),
                new Claim("PERMITEEXCLUIR", request.UsuarioPermissaoDto.PermiteExcluir.ToString()),
            };

            var dataHoraExpiracao = now.AddMinutes(double.Parse(jwtOptions.ExpiresInMinutes));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                notBefore: now,
                claims: claims,
                expires: dataHoraExpiracao,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)),
                        SecurityAlgorithms.HmacSha256)
                );

            var tokenGerado = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(new AutenticacaoRetornoDto(tokenGerado, dataHoraExpiracao));
        }
    }
}

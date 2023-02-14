using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterInformacoesPorTokenJwtQuery : IRequest<UsuarioPermissaoDto>
    {
        public ObterInformacoesPorTokenJwtQuery(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}

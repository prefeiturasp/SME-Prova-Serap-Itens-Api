using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTokenJwtQuery : IRequest<AutenticacaoRetornoDto>
    {
        public ObterTokenJwtQuery(UsuarioPermissaoDto usuarioPermissaoDto)
        {
            UsuarioPermissaoDto = usuarioPermissaoDto;
        }

        public UsuarioPermissaoDto UsuarioPermissaoDto { get; set; }
    }
}

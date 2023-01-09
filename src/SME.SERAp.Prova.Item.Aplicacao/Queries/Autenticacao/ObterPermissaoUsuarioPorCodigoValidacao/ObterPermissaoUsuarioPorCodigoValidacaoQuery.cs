using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterPermissaoUsuarioPorCodigoValidacaoQuery : IRequest<UsuarioPermissaoDto>
    {
        public ObterPermissaoUsuarioPorCodigoValidacaoQuery(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}

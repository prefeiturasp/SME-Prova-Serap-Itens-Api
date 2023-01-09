using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterPermissaoUsuarioPorLoginGrupoIdQuery : IRequest<UsuarioPermissaoDto>
    {
        public ObterPermissaoUsuarioPorLoginGrupoIdQuery(string login, Guid grupoId)
        {
            Login = login;
            GrupoId = grupoId;
        }

        public string Login { get; set; }
        public Guid GrupoId { get; set; }
    }
}

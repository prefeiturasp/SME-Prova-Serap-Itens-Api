using MediatR;
using SME.SERAp.Prova.Item.Dados;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterPermissaoUsuarioPorLoginGrupoIdQueryHandler : IRequestHandler<ObterPermissaoUsuarioPorLoginGrupoIdQuery, UsuarioPermissaoDto>
    {
        private readonly IRepositorioUsuario repositorioUsuario;

        public ObterPermissaoUsuarioPorLoginGrupoIdQueryHandler(IRepositorioUsuario repositorioUsuario)
        {
            this.repositorioUsuario = repositorioUsuario ?? throw new ArgumentNullException(nameof(repositorioUsuario));
        }

        public async Task<UsuarioPermissaoDto> Handle(ObterPermissaoUsuarioPorLoginGrupoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioUsuario.ObterPermissaoUsuarioPorLoginGrupoIdAsync(request.Login, request.GrupoId);
        }
    }
}

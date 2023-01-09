using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Task<UsuarioPermissaoDto> ObterPermissaoUsuarioPorLoginGrupoIdAsync(string login, Guid grupoLegadoId);
    }
}

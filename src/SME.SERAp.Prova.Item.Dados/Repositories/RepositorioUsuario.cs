using SME.SERAp.Prova.Item.Dados.Repositories;
using SME.SERAp.Prova.Item.Dominio;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<UsuarioPermissaoDto> ObterPermissaoUsuarioPorLoginGrupoIdAsync(string login, Guid grupoLegadoId)
        {
            var conexao = ObterConexaoLeitura();
            try
            {
                var query = @"select u.login, 
                                     u.nome, 
                                     g.nome as grupo, 
                                     g.permite_consultar as permiteConsultar, 
                                     g.permite_inserir as permiteInserir, 
                                     g.permite_alterar as permiteAlterar, 
                                     g.permite_excluir as permiteExcluir
                              from usuario u 
                              inner join usuario_grupo ug on ug.usuario_id = u.id 
                              inner join grupo g on g.id = ug.grupo_id 
                              where u.status = 1 and ug.status = 1 and g.status = 1 
                                and u.login = @login and g.legado_id = @grupoLegadoId";

                return await conexao.QueryFirstOrDefaultAsync<UsuarioPermissaoDto>(query, new { login, grupoLegadoId });
            }
            finally
            {
                conexao.Close();
                conexao.Dispose();
            }
        }
    }
}

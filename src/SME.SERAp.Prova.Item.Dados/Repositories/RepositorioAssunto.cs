using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioAssunto : RepositorioBase<Assunto>, IRepositorioAssunto
    {
        public RepositorioAssunto(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {

        }

        public async Task<IEnumerable<Assunto>> ObterAssuntos(long disciplinaId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select 
                                Id,
                                Legado_Id LegadoId,
                                Disciplina_Id DisciplinaId,
                                Descricao,
                                Criado_Em CriadoEm,
                                Alterado_Em AlteradoEm,
                                Status
                              from assunto
                              where status = 1
                              and disciplina_id = @disciplinaId";

                return await conn.QueryAsync<Assunto>(query, new { disciplinaId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
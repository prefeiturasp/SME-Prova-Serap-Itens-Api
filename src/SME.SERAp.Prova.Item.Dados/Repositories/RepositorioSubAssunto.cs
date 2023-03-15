using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
     public class RepositorioSubAssunto : RepositorioBase<SubAssunto>, IRepositorioSubAssunto
    {
        public RepositorioSubAssunto(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<SubAssunto>> ObterSubAssuntosPorAssuntoId(long assuntoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select *
                              from SubAssunto
                              where status = 1
                                and assunto_id = @assuntoId";

                return await conn.QueryAsync<SubAssunto>(query, new { assuntoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

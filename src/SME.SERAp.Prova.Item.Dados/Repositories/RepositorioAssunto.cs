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
    public class RepositorioAssunto : RepositorioBase<Assunto>, IRepositorioAssunto
    {
        public RepositorioAssunto(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<Assunto>> ObterAssuntos()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select *
                              from Assunto
                              where status = 1";

                return await conn.QueryAsync<Assunto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
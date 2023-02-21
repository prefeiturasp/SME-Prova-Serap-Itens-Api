using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioDificuldade : RepositorioBase<Dificuldade>, IRepositorioDificuldade
    {
        public RepositorioDificuldade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<Dificuldade>> ObterIdDescricaoOrdemAsync()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, descricao, ordem
                              from dificuldade
                              where status = 1";

                return await conn.QueryAsync<Dificuldade>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioNivelItem : RepositorioBase<NivelItem>, IRepositorioNivelItem
    {
        public RepositorioNivelItem(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<NivelItem>> Obter()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, descricao, ordem
                              from nivel_item
                              where status = 1";

                return await conn.QueryAsync<NivelItem>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

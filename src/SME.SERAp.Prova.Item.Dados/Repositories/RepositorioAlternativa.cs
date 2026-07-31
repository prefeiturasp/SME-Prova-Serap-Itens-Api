using Dapper;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioAlternativa : RepositorioBase<Alternativa>, IRepositorioAlternativa
    {
        public RepositorioAlternativa(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<bool> RemoverAlternativasAusentesAsync(long itemId, IEnumerable<long> idsManter)
        {
            var idsList = idsManter.ToArray();

            var query = idsList.Length == 0
                                            ? "DELETE FROM alternativa WHERE item_id = @itemId"
                                            : @"DELETE FROM alternativa
                                        WHERE item_id = @itemId
                                          AND id != ALL(@idsManter)";

            using var conn = ObterConexao();
            try
            {
                var linhasAfetadas = await conn.ExecuteAsync(query, new { itemId, idsManter = idsList });
                return linhasAfetadas >= 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
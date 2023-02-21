using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioDificuldade : RepositorioBase<Dificuldade>, IRepositorioDificuldade
    {
        public RepositorioDificuldade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<Dificuldade> ObterPorLegadoIdAsync(long legadoId)
        {

            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                legado_id as LegadoId, 
                                descricao,
                                ordem,
                                criado_em as CriadoEm,
                                alterado_em as AlteradoEm,
                                status
                              from dificuldade 
                              where legado_id = @legadoId";

                return await conn.QueryFirstOrDefaultAsync<Dificuldade>(query, new { legadoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}

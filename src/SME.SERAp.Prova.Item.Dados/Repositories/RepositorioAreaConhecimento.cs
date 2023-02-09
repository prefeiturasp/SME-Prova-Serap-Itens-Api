using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioAreaConhecimento : RepositorioBase<AreaConhecimento>, IRepositorioAreaConhecimento
    {
        public RepositorioAreaConhecimento(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<AreaConhecimento> ObterAreaConhecimentoPorLegadoId(long legadoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select * 
                                from area_conhecimento d  
                               where legado_id = @legadoId ";

                return await conn.QueryFirstOrDefaultAsync<AreaConhecimento>(query, new { legadoId });
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<long> ObterCodigoAreaConhecimentoPorId(long id)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select codigo 
                                from area_conhecimento d  
                               where id = @id ";

                return await conn.QueryFirstOrDefaultAsync<long>(query, new { id });
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
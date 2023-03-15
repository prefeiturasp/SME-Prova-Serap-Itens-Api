using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioQuantidadeAlternativas : RepositorioBase<QuantidadeAlternativas>, IRepositorioQuantidadeAlternativas
    {
        public RepositorioQuantidadeAlternativas(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<QuantidadeAlternativas>> ObterQuantidadeAlternativas()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select *
                              from quantidade_alternativa
                              where status = 1";

                return await conn.QueryAsync<QuantidadeAlternativas>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<SelectDto>> ObterListaQuantidadeAlternativas()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id valor, descricao  
                                from quantidade_alternativa qa 
                                where status = 1
                                ORDER BY eh_padrao  DESC, descricao";

                return await conn.QueryAsync<SelectDto>(query);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
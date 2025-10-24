using Dapper;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioItem : RepositorioBase<Dominio.Entities.Item>, IRepositorioItem
    {
        public RepositorioItem(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<long?> ObterMaiorValorId()
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"SELECT MAX(ID) FROM Item";

                return await conn.QueryFirstOrDefaultAsync<long?>(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<long?> ObterQtdItensAreaConhecimentoEhDisciplina(long areaConhecimentoLegadoId, long disciplinaLegadoId) 
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"SELECT count(i.id) 
                               FROM item i
                               inner join area_conhecimento ac on ac.legado_id  =  i.are_conhecimento_legado_id 
                               inner join disciplina d  on d.legado_id  = i.disciplina_legado_id 
                               where  are_conhecimento_legado_id = @areaConhecimentoLegadoId
                                 and disciplina_legado_id  = @disciplinaLegadoId";
                              

                return await conn.QueryFirstOrDefaultAsync<long?>(query, new { areaConhecimentoLegadoId , disciplinaLegadoId });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<CodigoItemDto>> ListaCodigosItens(long? codigoItem)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@" SELECT ID,
	                                                    Codigo_Item as CodigoItem
    	                                           FROM ITEM");


               if(codigoItem is not null)
                    query.Append($@" WHERE  CAST(codigo_item AS TEXT) LIKE '%{codigoItem}%';");


                return await conn.QueryAsync<CodigoItemDto>(query.ToString(), new { codigoItem });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

    }
}

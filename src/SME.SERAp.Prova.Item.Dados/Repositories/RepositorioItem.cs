using Dapper;
using Nest;
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
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
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
                throw;
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


                var resultado = await conn.QueryAsync<long?>(query, new { areaConhecimentoLegadoId, disciplinaLegadoId });
                return resultado?.FirstOrDefault() ?? 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<DominioItem> ObterPorId(long itemId)
        {
            const string queryItem = @"
                                        SELECT * FROM item 
                                        WHERE id = @itemId;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<DominioItem>(queryItem, new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<DominioItem> ObterUltimaVersaoItemPorCodigo(string codigoItem)
        {
            const string queryUltimaVersao = @"
                                                SELECT *
                                                FROM item i
                                                WHERE i.codigo_item = @codigoItem
                                                ORDER BY i.versao_item DESC
                                                LIMIT 1;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<DominioItem>(queryUltimaVersao, new { codigoItem });
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter a última versão do item pelo Código {codigoItem}.", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<Alternativa>> ObterAlternativasPorItemId(long itemId)
        {
            const string query = @"
                                    SELECT
                                        id,
                                        item_id ItemId,
                                        descricao,
                                        ordem,
                                        numeracao
                                    FROM alternativa
                                    WHERE item_id = @itemId
                                    ORDER BY ordem;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryAsync<Alternativa>(query, new { itemId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<DominioItem>> ObterTodasVersoesPorCodigoItem(string codigoItem)
        {
            const string query = @"
                                    SELECT 
                                        id, -- Adicionado o ID da versão do item
                                        codigo_item CodigoItem, 
                                        versao_item VersaoItem,
                                        criado_em DataCriacao
                                    FROM item
                                    WHERE codigo_item = @codigoItem
                                    ORDER BY versao_item;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryAsync<DominioItem>(query, new { codigoItem });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
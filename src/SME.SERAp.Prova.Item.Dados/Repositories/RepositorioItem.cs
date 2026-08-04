using Dapper;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
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

        public async Task<Dominio.Entities.Item> ObterComAlternativaPorIdAsync(long itemId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"SELECT * FROM Item WHERE Id = @itemId;
                              SELECT * FROM Alternativa WHERE item_id = @itemId;";
                using var multi = await conn.QueryMultipleAsync(query, new { itemId });
                var item = await multi.ReadFirstOrDefaultAsync<Dominio.Entities.Item>();
                if (item != null)
                    item.Alternativas = (await multi.ReadAsync<Alternativa>()).ToList();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DominioItem> ObterUltimaVersaoItemPorCodigo(string codigoItem)
        {
            const string queryUltimaVersao = @"
                                                SELECT *
                                                FROM item i
                                                WHERE i.codigo_item = @codigoItem
                                                  AND i.situacao != @situacaoRascunho
                                                ORDER BY i.versao_item DESC
                                                LIMIT 1;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<DominioItem>(queryUltimaVersao, new
                {
                    codigoItem,
                    situacaoRascunho = (int)SituacaoItem.Rascunho
                });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<IEnumerable<CodigoItemDto>> ObterListaCodigosItens(string codigoItem)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@" SELECT ID,
	                                                    Codigo_Item as CodigoItem
    	                                           FROM ITEM");


                if (codigoItem is not null)
                    query.Append($@" WHERE  CAST(codigo_item AS TEXT) LIKE '%{codigoItem}%';");


                return await conn.QueryAsync<CodigoItemDto>(query.ToString(), new { codigoItem });
            }

            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<PaginacaoDto<ItemListaDto>> ObterListaItensPorFiltro(FiltroItemsDto filtroDto)
        {
            using var conn = ObterConexao();
            try
            {
                var parameters = new DynamicParameters();
                var queryBase = new StringBuilder(@"
                                                    FROM (
                                                        SELECT DISTINCT ON (I.codigo_item) I.*
                                                        FROM ITEM I
                                                        ORDER BY I.codigo_item, I.versao_item DESC
                                                    ) I
                                                    LEFT JOIN DIFICULDADE D ON D.Id = I.dificuldade_sugerida_id  
                                                    LEFT JOIN DISCIPLINA D2 ON D2.Id = I.disciplina_id
                                                    WHERE 1 = 1
                                                ");

                if (filtroDto.CodigoItem is not null)
                {
                    queryBase.Append($@" AND I.codigo_item = @codigoItem ");
                    parameters.Add("codigoItem", filtroDto.CodigoItem);
                }

                if (filtroDto.AreaConhecimentoId is not null)
                {
                    queryBase.Append($@" AND I.area_conhecimento_id = @areaConhecimentoId ");
                    parameters.Add("areaConhecimentoId", filtroDto.AreaConhecimentoId);
                }

                if (filtroDto.CompetenciaId is not null)
                {
                    queryBase.Append($@" AND I.competencia_id = @competenciaId ");
                    parameters.Add("competenciaId", filtroDto.CompetenciaId);
                }

                if (filtroDto.HabilidadeId is not null)
                {
                    queryBase.Append($@" AND I.habilidade_id = @habilidadeId ");
                    parameters.Add("habilidadeId", filtroDto.HabilidadeId);
                }

                if (filtroDto.DisciplinaId is not null)
                {
                    queryBase.Append($@" AND I.disciplina_id = @disciplinaId ");
                    parameters.Add("disciplinaId", filtroDto.DisciplinaId);
                }

                if (filtroDto.DificuldadeSugeridaId is not null)
                {
                    queryBase.Append($@" AND I.dificuldade_sugerida_id = @dificuldadeSugeridaId ");
                    parameters.Add("dificuldadeSugeridaId", filtroDto.DificuldadeSugeridaId);
                }

                if (filtroDto.MatrizId is not null)
                {
                    queryBase.Append($@" AND I.matriz_id = @matrizId ");
                    parameters.Add("matrizId", filtroDto.MatrizId);
                }

                if (filtroDto.Situacao is not null)
                {
                    queryBase.Append($@" AND I.situacao = @situacao ");
                    parameters.Add("situacao", filtroDto.Situacao);
                }

                if (filtroDto.AnoMatrizId is not null)
                {
                    queryBase.Append($@" AND I.tipo_grade_id = @anoMatrizId ");
                    parameters.Add("anoMatrizId", filtroDto.AnoMatrizId);
                }

                if (filtroDto.CategoriaId is not null)
                {
                    queryBase.Append($@" AND I.quantidade_alternativa_id = @categoriaId ");
                    parameters.Add("categoriaId", filtroDto.CategoriaId);
                }

                if (filtroDto?.PalavrasChave?.Any() ?? false)
                {
                    queryBase.Append($@" AND EXISTS (SELECT 1 FROM unnest(string_to_array(I.palavras_chave, ';')) AS palavras WHERE palavras ILIKE ANY (@palavrasChave))");
                    parameters.Add("palavrasChave", filtroDto.PalavrasChave.Select(p => $"%{p}%").ToArray());
                }

                if (filtroDto.InformacoesEstatisticas == true)
                    queryBase.Append($"AND ( I.discriminacao IS NOT NULL  AND I.acerto_casual IS NOT NULL AND I.Dificuldade is not NULL )");

                if (filtroDto.InformacoesEstatisticas == false)
                    queryBase.Append($"AND ( I.discriminacao IS  NULL  OR I.acerto_casual IS  NULL OR I.Dificuldade is  NULL )");

                var countQuery = $"SELECT COUNT(*) {queryBase}";
                var totalRegistros = await conn.ExecuteScalarAsync<int>(countQuery, parameters);

                var querySelect = new StringBuilder(@"
                                                        SELECT 
                                                            I.Id, 
                                                            I.codigo_item AS CodigoItem, 
                                                            I.Enunciado,
                                                            D2.Descricao AS Disciplina,  
                                                            D.Descricao AS Dificuldade, 
                                                            I.Situacao, 
                                                            I.Criado_em AS DataCriacao
                                                    ");

                querySelect.Append(queryBase);

                querySelect.Append(" ORDER BY I.Id DESC ");

                int pagina = filtroDto.Pagina ?? 1;
                int tamanhoPagina = filtroDto.TamanhoPagina ?? 10;
                int offset = (pagina - 1) * tamanhoPagina;

                querySelect.Append($" LIMIT @tamanhoPagina OFFSET @offset ");
                parameters.Add("tamanhoPagina", tamanhoPagina);
                parameters.Add("offset", offset);

                var itens = await conn.QueryAsync<ItemListaDto>(querySelect.ToString(), parameters);

                return new PaginacaoDto<ItemListaDto>(itens, pagina, tamanhoPagina, totalRegistros);
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
                                        criado_em DataCriacao,
                                        situacao Situacao
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

        public async Task<bool> InativarVersoesAnterioresAsync(string codigoItem, long versaoAtual)
        {
            const string query = @"
                                    UPDATE item
                                    SET situacao = @situacaoInativo,
                                        alterado_em = @agora
                                    WHERE codigo_item = @codigoItem
                                      AND versao_item < @versaoAtual
                                      AND situacao != @situacaoInativo
                                      AND situacao != @situacaoRascunho";

            using var conn = ObterConexao();
            try
            {
                var linhasAfetadas = await conn.ExecuteAsync(query, new
                {
                    codigoItem,
                    versaoAtual,
                    situacaoInativo = (int)SituacaoItem.Inativo,
                    situacaoRascunho = (int)SituacaoItem.Rascunho,
                    agora = DateTime.Now
                });

                return linhasAfetadas > 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<bool> InativarRascunhoPorCodigoItemAsync(string codigoItem)
        {
            const string query = @"
                                    UPDATE item
                                    SET situacao = @situacaoInativo,
                                        alterado_em = @agora
                                    WHERE codigo_item = @codigoItem
                                      AND situacao = @situacaoRascunho";

            using var conn = ObterConexao();
            try
            {
                var linhasAfetadas = await conn.ExecuteAsync(query, new
                {
                    codigoItem,
                    situacaoInativo = (int)SituacaoItem.Inativo,
                    situacaoRascunho = (int)SituacaoItem.Rascunho,
                    agora = DateTime.Now
                });

                return linhasAfetadas > 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<bool> AtualizarSituacaoItemAsync(string codigoItem, long versaoItem, SituacaoItem situacao)
        {
            const string query = @"
                                    UPDATE item
                                    SET situacao    = @situacao,
                                        alterado_em = @agora
                                    WHERE codigo_item = @codigoItem
                                      AND versao_item = @versaoItem";

            using var conn = ObterConexao();
            try
            {
                var linhasAfetadas = await conn.ExecuteAsync(query, new
                {
                    situacao = (int)situacao,
                    agora = DateTime.Now,
                    codigoItem,
                    versaoItem
                });

                return linhasAfetadas > 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<DominioItem> ObterRascunhoNovaVersaoPorCodigoAsync(string codigoItem)
        {
            const string query = @"
                                    SELECT *
                                    FROM item
                                    WHERE codigo_item = @codigoItem
                                      AND situacao    = @situacaoRascunho
                                      AND versao_item > 0
                                    ORDER BY versao_item DESC
                                    LIMIT 1;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<DominioItem>(query, new
                {
                    codigoItem,
                    situacaoRascunho = (int)SituacaoItem.Rascunho
                });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<DominioItem> ObterRascunhoPorCodigoAsync(string codigoItem)
        {
            const string query = @"
                                    SELECT *
                                    FROM item
                                    WHERE codigo_item    = @codigoItem
                                      AND situacao       = @situacaoRascunho
                                    ORDER BY versao_item DESC
                                    LIMIT 1;";

            using var conn = ObterConexao();
            try
            {
                return await conn.QueryFirstOrDefaultAsync<DominioItem>(query, new
                {
                    codigoItem,
                    situacaoRascunho = (int)SituacaoItem.Rascunho
                });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
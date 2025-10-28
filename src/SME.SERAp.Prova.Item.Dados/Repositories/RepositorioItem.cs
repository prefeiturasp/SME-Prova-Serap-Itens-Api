using Dapper;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
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

        public async Task<IEnumerable<CodigoItemDto>> ObterListaCodigosItens(long? codigoItem)
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
          
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }



        public async Task<IEnumerable<ItemListaDto>> ObterListaItensPorFiltro(FiltroItemsDto filtroDto)
        {
            using var conn = ObterConexao();
            try
            {
                var query = new StringBuilder(@" SELECT I.Id, 
                                                        I.codigo_item as CodigoItem, 
                                                        I.Enunciado,
                                                        D2.Descricao  as Disciplina,  
                                                        D.Descricao as Dificuldade , 
                                                        I.Situacao, 
                                                        I.Criado_em as DataCriacao
                                                 FROM  ITEM I  
                                                 LEFT JOIN DIFICULDADE D  on D.Id  = I.dificuldade_sugerida_id  
                                                 LEFT JOIN DISCIPLINA  D2  on D2.Id = i.disciplina_id
                                                 WHERE 1 = 1");


                if (filtroDto.CodigoItem is not null)
                    query.Append($@" AND  I.codigo_item  = '{filtroDto.CodigoItem}' ");

                if (filtroDto.AreaConhecimentoId is not null)
                    query.Append($@" AND  I.area_conhecimento_id  = {filtroDto.AreaConhecimentoId} ");

                if (filtroDto.CompetenciaId is not null)
                    query.Append($@" AND  I.competencia_id  = {filtroDto.CompetenciaId} ");
                if (filtroDto.HabilidadeId is not null)
                    query.Append($@" AND  I.habilidade_id  = {filtroDto.HabilidadeId} ");
                if (filtroDto.DisciplinaId is not null)
                    query.Append($@" AND  I.disciplina_id  = {filtroDto.DisciplinaId} ");

                if (filtroDto.DificuldadeSugeridaId is not null)
                    query.Append($@" AND  I.dificuldade_sugerida_id  = {filtroDto.DificuldadeSugeridaId} ");
                if (filtroDto.MatrizId is not null)
                    query.Append($@" AND  I.matriz_id  = {filtroDto.MatrizId} ");

                if (filtroDto.Situacao is not null)
                    query.Append($@" AND  I.situacao  = {filtroDto.Situacao} ");

                return await conn.QueryAsync<ItemListaDto>(query.ToString());
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

﻿using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioDisciplina : RepositorioBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplina(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<Disciplina>> ObterPorAreaconhecimentoId(long areaconhecimentoId)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id, 
                                     legado_id as LegadoId, 
                                     descricao,
                                     nivel_ensino as NivelEnsino,
                                     criado_em as CriadoEm,
                                     alterado_em as AlteradoEm,
                                     status
                                from disciplina where area_conhecimento_id = @areaconhecimentoId";

                return await conn.QueryAsync<Disciplina>(query, new { areaconhecimentoId });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public async Task<long> ObterCodigoDisciplinaPorId(long id)
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select codigo 
                                from disciplina d  
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

        public async Task<Disciplina> ObterDisciplinaPorLegadoId(long legadoId)
        {

            using var conn = ObterConexao();
            try
            {
                var query = @"select * 
                                from disciplina d  
                               where legado_id = @legadoId ";

                return await conn.QueryFirstOrDefaultAsync<Disciplina>(query, new { legadoId });
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

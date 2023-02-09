using Dommel;
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
    public class RepositorioSequencialItem : RepositorioBase<SequencialItem>, IRepositoSequencialItem
    {
        public RepositorioSequencialItem(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }
        public async Task<SequencialItem> ObterSequencialItemPorCodigoAreaEDisciplina(long codigoAreaConhecimento, long codigoDisciplina )
        {
            using var conn = ObterConexao();
            try
            {
                var query = @"select id as Id,
                                     codigo_area_conhecimento as CodigoAreaConhecimento,
                                     codigo_disciplina as CodigoDisciplina,
                                     sequencial as Sequencial, 
                                     criado_em as CriadoEm, 
                                     alterado_em as AlteradoEm
                                from sequencial_item 
                                where codigo_area_conhecimento = @codigoAreaConhecimento
                                  and codigo_disciplina = @codigoDisciplina";

                return await conn.QueryFirstOrDefaultAsync<SequencialItem>(query, new { codigoAreaConhecimento, codigoDisciplina });
            }
            catch (System.Exception ex)
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

       
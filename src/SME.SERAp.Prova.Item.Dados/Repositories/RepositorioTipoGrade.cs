using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioTipoGrade : RepositorioBase<TipoGrade>, IRepositorioTipoGrade
    {
        public RepositorioTipoGrade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<RetornoTipoGradeDto>> ObterTiposGradePorMatrizId(long matrizId)
        {
            using var conn = ObterConexao();

            try
            {
                const string query = @"select tg.id,
                                            tg.descricao
                                        from tipo_grade tg
                                        where tg.matriz_id = @matrizId
                                        and tg.status = @status
                                        order by tg.ordem";

                return await conn.QueryAsync<RetornoTipoGradeDto>(query,
                    new { matrizId, status = (int)StatusGeral.Ativo });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
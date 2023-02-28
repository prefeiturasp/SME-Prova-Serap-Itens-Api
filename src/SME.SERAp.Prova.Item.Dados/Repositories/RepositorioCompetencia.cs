using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioCompetencia : RepositorioBase<Competencia>, IRepositorioCompetencia
    {
        public RepositorioCompetencia(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<RetornoCompetenciaDto>> ObterCompetenciasPorMatrizId(long matrizId)
        {
            using var conn = ObterConexao();

            try
            {
                const string query = @"select c.id,
                                            c.descricao
                                        from competencia c
                                        where c.matriz_id = @matrizId
                                        and c.status = @status";

                return await conn.QueryAsync<RetornoCompetenciaDto>(query,
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
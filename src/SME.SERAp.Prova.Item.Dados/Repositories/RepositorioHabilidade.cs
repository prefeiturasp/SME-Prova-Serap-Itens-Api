using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;

namespace SME.SERAp.Prova.Item.Dados.Repositories
{
    public class RepositorioHabilidade : RepositorioBase<Habilidade>, IRepositorioHabilidade
    {
        public RepositorioHabilidade(ConnectionStringOptions connectionStrings) : base(connectionStrings)
        {
        }

        public async Task<IEnumerable<RetornoHabilidadeDto>> ObterHabilidadesPorCompetenciaId(long competenciaId)
        {
            using var conn = ObterConexao();

            try
            {
                const string query = @"select h.id,
                                            h.codigo,
                                            h.descricao
                                        from habilidade h
                                        where h.competencia_id = @competenciaId
                                        and h.status = @status";

                return await conn.QueryAsync<RetornoHabilidadeDto>(query,
                    new { competenciaId, status = (int)StatusGeral.Ativo });
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
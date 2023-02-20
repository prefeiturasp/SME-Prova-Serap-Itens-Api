using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioCompetencia : IRepositorioBase<Competencia>
    {
        Task<IEnumerable<RetornoCompetenciaDto>> ObterCompetenciasPorMatrizId(long matrizId);
    }
}
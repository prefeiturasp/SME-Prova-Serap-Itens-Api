using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioTipoGrade : IRepositorioBase<TipoGrade>
    {
        Task<IEnumerable<RetornoTipoGradeDto>> ObterTiposGradePorMatrizId(long matrizId);        
    }
}
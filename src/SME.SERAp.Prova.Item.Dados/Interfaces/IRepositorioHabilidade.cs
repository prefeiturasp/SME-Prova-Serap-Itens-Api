using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioHabilidade : IRepositorioBase<Habilidade>
    {
        Task<IEnumerable<RetornoHabilidadeDto>> ObterHabilidadesPorCompetenciaId(long competenciaId);        
    }
}
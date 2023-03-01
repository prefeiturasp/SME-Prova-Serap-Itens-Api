using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterHabilidadesPorCompetenciaIdUseCase
    {
        Task<IEnumerable<SelectDto>> Executar(long competenciaId);
    }
}
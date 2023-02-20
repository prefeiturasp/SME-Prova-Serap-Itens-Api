using System.Collections.Generic;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterCompetenciasPorMatrizIdUseCase
    {
        Task<IEnumerable<RetornoCompetenciaDto>> Executar(long matrizId);
    }
}
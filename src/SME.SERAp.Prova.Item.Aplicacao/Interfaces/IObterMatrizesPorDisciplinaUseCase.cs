using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterMatrizesPorDisciplinaUseCase
    {
        Task<IEnumerable<Matriz>> Executar(long disciplinaId);
    }
}

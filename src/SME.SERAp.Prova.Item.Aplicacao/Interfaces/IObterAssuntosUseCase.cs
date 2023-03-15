using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterAssuntosUseCase
    {
        Task<IEnumerable<SelectDto>> Executar(long disciplinaId);
    }
}

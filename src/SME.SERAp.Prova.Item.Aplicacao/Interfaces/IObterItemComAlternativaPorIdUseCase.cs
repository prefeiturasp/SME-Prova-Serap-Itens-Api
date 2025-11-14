using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterItemComAlternativaPorIdUseCase
    {
        Task<ItemComAlternativasDto> Executar(long itemId);
    }
}

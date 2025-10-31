using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterItemComAlternativaPorIdUseCase
    {
        Task<ItemConsulta> Executar(long itemId);
    }
}

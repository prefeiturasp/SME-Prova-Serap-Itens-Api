using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public interface IObterItemPorIdUseCase
    {
        Task<ItemConsulta> Executar(long itemId);
    }
}

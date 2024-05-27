using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface ISalvarRascunhoItemUseCase
    {
        Task<long> Executar(ItemRascunhoDto itemRascunhoDto);
    }
}

using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IObterListaItemsUseCase
    {
        Task<PaginacaoDto<ItemListaDto>> Executar(FiltroItemsDto filtroItem);
    }
}

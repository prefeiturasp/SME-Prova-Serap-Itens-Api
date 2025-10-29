using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterListaItemsPorFiltroDtoQuery : IRequest<PaginacaoDto<ItemListaDto>>
    {
        public ObterListaItemsPorFiltroDtoQuery(FiltroItemsDto filtroItems)
        {
           FiltroItemsDto = filtroItems;
        }

        public FiltroItemsDto FiltroItemsDto { get; set; }

    }
}
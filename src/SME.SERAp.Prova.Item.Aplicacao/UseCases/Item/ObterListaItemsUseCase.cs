using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using SME.SERAp.Prova.Item.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterListaItemsUseCase : AbstractUseCase, IObterListaItemsUseCase
    {
        public ObterListaItemsUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<ItemListaDto>> Executar(FiltroItemsDto filtroItem)
        {
            var listaItemsDto = await mediator.Send(new ObterListaItemsPorFiltroDtoQuery(filtroItem));
            if (listaItemsDto != null && listaItemsDto.Any())
            {
                foreach (var item in listaItemsDto)
                    if(item.Situacao is not null)
                     item.SituacaoDesc = EnumExtensions.Descricao((SituacaoItem)item.Situacao);

              return  listaItemsDto.OrderBy(x => x.CodigoItem);
            }

            return default;
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterListaItemsPorFiltroDtoQueryHandler : IRequestHandler<ObterListaItemsPorFiltroDtoQuery, PaginacaoDto<ItemListaDto>>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterListaItemsPorFiltroDtoQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<PaginacaoDto<ItemListaDto>> Handle(ObterListaItemsPorFiltroDtoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterListaItensPorFiltro(request.FiltroItemsDto);
        }
    }
}
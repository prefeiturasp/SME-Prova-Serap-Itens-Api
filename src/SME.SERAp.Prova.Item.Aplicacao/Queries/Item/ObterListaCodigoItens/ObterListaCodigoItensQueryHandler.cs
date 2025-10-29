using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    internal class ObterListaCodigoItensQueryHandler : IRequestHandler<ObterListaCodigoItensQuery, IEnumerable<CodigoItemDto>>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterListaCodigoItensQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<IEnumerable<CodigoItemDto>> Handle(ObterListaCodigoItensQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterListaCodigosItens(request.CodigoItem);
        }
    }
}

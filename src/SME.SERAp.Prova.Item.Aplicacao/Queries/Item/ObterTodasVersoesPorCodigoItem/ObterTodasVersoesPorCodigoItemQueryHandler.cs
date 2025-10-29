using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;

    public class ObterTodasVersoesPorCodigoItemQueryHandler : IRequestHandler<ObterTodasVersoesPorCodigoItemQuery, IEnumerable<DominioItem>>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterTodasVersoesPorCodigoItemQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<IEnumerable<DominioItem>> Handle(ObterTodasVersoesPorCodigoItemQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterTodasVersoesPorCodigoItem(request.CodigoItem);
        }
    }
}
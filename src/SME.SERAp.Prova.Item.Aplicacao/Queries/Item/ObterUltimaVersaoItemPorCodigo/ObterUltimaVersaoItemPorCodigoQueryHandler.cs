using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterUltimaVersaoItemPorCodigoQueryHandler : IRequestHandler<ObterUltimaVersaoItemPorCodigoQuery, DominioItem>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterUltimaVersaoItemPorCodigoQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<DominioItem> Handle(ObterUltimaVersaoItemPorCodigoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterUltimaVersaoItemPorCodigo(request.CodigoItem);
        }
    }
}
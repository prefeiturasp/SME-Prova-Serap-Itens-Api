using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemBasePorId
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;

    public class ObterItemBasePorIdQueryHandler : IRequestHandler<ObterItemBasePorIdQuery, DominioItem>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterItemBasePorIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<DominioItem> Handle(ObterItemBasePorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterPorId(request.ItemId);
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterAlternativasBrutasPorItemId
{
    public class ObterAlternativasBrutasPorItemIdQueryHandler : IRequestHandler<ObterAlternativasBrutasPorItemIdQuery, IEnumerable<Alternativa>>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterAlternativasBrutasPorItemIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<IEnumerable<Alternativa>> Handle(ObterAlternativasBrutasPorItemIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterAlternativasPorItemId(request.ItemId);
        }
    }
}
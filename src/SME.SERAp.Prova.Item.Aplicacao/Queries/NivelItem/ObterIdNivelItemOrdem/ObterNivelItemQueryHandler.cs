using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.NivelItem.ObterIdNivelItemOrdem
{
    public class ObterNivelItemQueryHandler : IRequestHandler<ObterNivelItemQuery, IEnumerable<Dominio.Entities.NivelItem>>
    {
        private readonly IRepositorioNivelItem repositorioNivelItem;

        public ObterNivelItemQueryHandler(IRepositorioNivelItem repositorioNivelItem)
        {
            this.repositorioNivelItem = repositorioNivelItem ?? throw new ArgumentNullException(nameof(repositorioNivelItem));
        }

        public async Task<IEnumerable<Dominio.Entities.NivelItem>> Handle(ObterNivelItemQuery request, CancellationToken cancellationToken)
        {
            return await repositorioNivelItem.Obter();
        }
    }
}

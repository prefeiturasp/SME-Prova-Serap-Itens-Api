using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId
{
    public class ObterItemComAlternativaPorIdQueryHandler : IRequestHandler<ObterItemComAlternativaPorIdQuery, ItemConsulta>
    {
        private readonly IRepositorioItem repositorioItem;
        public ObterItemComAlternativaPorIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }
        public async Task<ItemConsulta> Handle(ObterItemComAlternativaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterComAlternativaPorIdAsync(request.Id);
        }
    }
}

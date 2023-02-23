using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterItemPorIdQueryHandler : IRequestHandler<ObterItemPorIdQuery, ItemConsulta>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterItemPorIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<ItemConsulta> Handle(ObterItemPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterPorIdAsync(request.Id);
        }
    }
}

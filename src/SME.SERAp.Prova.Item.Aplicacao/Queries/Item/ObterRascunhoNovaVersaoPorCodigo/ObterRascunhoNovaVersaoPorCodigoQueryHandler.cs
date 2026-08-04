using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoNovaVersaoPorCodigo
{
    public class ObterRascunhoNovaVersaoPorCodigoQueryHandler : IRequestHandler<ObterRascunhoNovaVersaoPorCodigoQuery, Dominio.Entities.Item>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterRascunhoNovaVersaoPorCodigoQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<Dominio.Entities.Item> Handle(ObterRascunhoNovaVersaoPorCodigoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterRascunhoNovaVersaoPorCodigoAsync(request.CodigoItem);
        }
    }
}
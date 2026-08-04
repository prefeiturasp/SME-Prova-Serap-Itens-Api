using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoPorCodigo
{
    public class ObterRascunhoPorCodigoQueryHandler : IRequestHandler<ObterRascunhoPorCodigoQuery, Dominio.Entities.Item>
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterRascunhoPorCodigoQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<Dominio.Entities.Item> Handle(ObterRascunhoPorCodigoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterRascunhoPorCodigoAsync(request.CodigoItem);
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Rascunho
{
    public class InativarRascunhoPorCodigoItemCommandHandler : IRequestHandler<InativarRascunhoPorCodigoItemCommand, bool>
    {
        private readonly IRepositorioItem repositorioItem;

        public InativarRascunhoPorCodigoItemCommandHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<bool> Handle(InativarRascunhoPorCodigoItemCommand request, CancellationToken cancellationToken)
        {
            return await repositorioItem.InativarRascunhoPorCodigoItemAsync(request.CodigoItem);
        }
    }
}
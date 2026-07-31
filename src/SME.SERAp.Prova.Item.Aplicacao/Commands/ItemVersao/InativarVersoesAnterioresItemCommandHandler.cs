using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVersao
{
    public class InativarVersoesAnterioresItemCommandHandler : IRequestHandler<InativarVersoesAnterioresItemCommand, bool>
    {
        private readonly IRepositorioItem repositorioItem;

        public InativarVersoesAnterioresItemCommandHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<bool> Handle(InativarVersoesAnterioresItemCommand request, CancellationToken cancellationToken)
        {
            return await repositorioItem.InativarVersoesAnterioresAsync(request.CodigoItem, request.VersaoAtual);
        }
    }
}
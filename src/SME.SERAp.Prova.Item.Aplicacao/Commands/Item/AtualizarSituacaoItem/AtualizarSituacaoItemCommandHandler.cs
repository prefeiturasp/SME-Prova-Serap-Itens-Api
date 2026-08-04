using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Item.AtualizarSituacaoItem
{
    public class AtualizarSituacaoItemCommandHandler : IRequestHandler<AtualizarSituacaoItemCommand, bool>
    {
        private readonly IRepositorioItem repositorioItem;

        public AtualizarSituacaoItemCommandHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<bool> Handle(AtualizarSituacaoItemCommand request, CancellationToken cancellationToken)
        {
            if (request.Situacao == SituacaoItem.Rascunho)
                throw new Exception("Não é permitido alterar a situação de um item para Rascunho.");

            return await repositorioItem.AtualizarSituacaoItemAsync(
                request.CodigoItem,
                request.VersaoItem,
                request.Situacao);
        }
    }
}
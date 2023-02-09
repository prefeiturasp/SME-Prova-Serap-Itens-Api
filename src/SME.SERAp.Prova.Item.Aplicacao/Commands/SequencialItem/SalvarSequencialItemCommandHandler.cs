using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SalvarSequencialItemCommandHandler : IRequestHandler<SalvarSequencialItemCommand, long>
    {
        private readonly IRepositoSequencialItem repositoSequencialItem;

        public SalvarSequencialItemCommandHandler(IRepositoSequencialItem repositoSequencialItem)
        {
            this.repositoSequencialItem = repositoSequencialItem ?? throw new ArgumentNullException(nameof(repositoSequencialItem));
        }

        public async Task<long> Handle(SalvarSequencialItemCommand request, CancellationToken cancellationToken)
        {
            return await repositoSequencialItem.SalvarAsync(request.SequencialItem);
        }
    }
}
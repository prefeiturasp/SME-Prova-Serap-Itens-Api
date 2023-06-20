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
    public class SalvarItemCommandHandler : IRequestHandler<SalvarItemCommand, long>
    {
        private readonly IRepositorioItem repositorioItem;

        public SalvarItemCommandHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<long> Handle(SalvarItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await repositorioItem.SalvarAsync(request.Item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
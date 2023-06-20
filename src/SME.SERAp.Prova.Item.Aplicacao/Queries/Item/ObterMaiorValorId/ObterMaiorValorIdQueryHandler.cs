using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMaiorValorIdQueryHandler : IRequestHandler<ObterMaiorValorIdQuery, long?>
    {

        private readonly IRepositorioItem repositorioItem;

        public ObterMaiorValorIdQueryHandler(IRepositorioItem repositorioItem)
        {
            this.repositorioItem = repositorioItem ?? throw new ArgumentNullException(nameof(repositorioItem));
        }

        public async Task<long?> Handle(ObterMaiorValorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioItem.ObterMaiorValorId();
        }
    }
}

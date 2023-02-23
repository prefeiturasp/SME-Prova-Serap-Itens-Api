using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterCodigoAreaConhecimentoPorIdQueryHandler : IRequestHandler<ObterCodigoAreaConhecimentoPorIdQuery, long>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterCodigoAreaConhecimentoPorIdQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<long> Handle(ObterCodigoAreaConhecimentoPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterCodigoAreaConhecimentoPorId(request.Id);
        }
    }
}
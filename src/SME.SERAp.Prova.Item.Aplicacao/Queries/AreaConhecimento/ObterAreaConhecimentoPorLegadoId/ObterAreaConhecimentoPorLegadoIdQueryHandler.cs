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
    public class ObterAreaConhecimentoPorLegadoIdQueryHandler : IRequestHandler<ObterAreaConhecimentoPorLegadoIdQuery, AreaConhecimento>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterAreaConhecimentoPorLegadoIdQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<AreaConhecimento> Handle(ObterAreaConhecimentoPorLegadoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterAreaConhecimentoPorLegadoId(request.LegadoId);
        }
    }
}
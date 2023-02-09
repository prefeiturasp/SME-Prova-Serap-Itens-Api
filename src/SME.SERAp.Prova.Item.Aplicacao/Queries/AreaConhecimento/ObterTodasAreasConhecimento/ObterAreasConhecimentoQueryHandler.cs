using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
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
    public class ObterAreasConhecimentoQueryHandler : IRequestHandler<ObterAreasConhecimentoQuery, IEnumerable<AreaConhecimento>>
    {

        private readonly IRepositorioAreaConhecimento repositorioAreaConhecimento;

        public ObterAreasConhecimentoQueryHandler(IRepositorioAreaConhecimento repositorioAreaConhecimento)
        {
            this.repositorioAreaConhecimento = repositorioAreaConhecimento ?? throw new ArgumentNullException(nameof(repositorioAreaConhecimento));
        }

        public async Task<IEnumerable<AreaConhecimento>> Handle(ObterAreasConhecimentoQuery request, CancellationToken cancellationToken)
        {
            return await repositorioAreaConhecimento.ObterTudoAsync();
        }
    }
}
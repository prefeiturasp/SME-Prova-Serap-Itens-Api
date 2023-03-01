using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaIdQueryHandler : IRequestHandler<ObterHabilidadesPorCompetenciaIdQuery, IEnumerable<RetornoHabilidadeDto>>
    {
        private readonly IRepositorioHabilidade repositorioHabilidade;

        public ObterHabilidadesPorCompetenciaIdQueryHandler(IRepositorioHabilidade repositorioHabilidade)
        {
            this.repositorioHabilidade = repositorioHabilidade ?? throw new ArgumentNullException(nameof(repositorioHabilidade));
        }

        public async Task<IEnumerable<RetornoHabilidadeDto>> Handle(ObterHabilidadesPorCompetenciaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioHabilidade.ObterHabilidadesPorCompetenciaId(request.CompetenciaId);
        }
    }
}
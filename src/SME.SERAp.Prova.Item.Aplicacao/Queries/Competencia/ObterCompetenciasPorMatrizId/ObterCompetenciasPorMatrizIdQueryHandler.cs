using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizIdQueryHandler : IRequestHandler<ObterCompetenciasPorMatrizIdQuery, IEnumerable<RetornoCompetenciaDto>>
    {
        private readonly IRepositorioCompetencia repositorioCompetencia;

        public ObterCompetenciasPorMatrizIdQueryHandler(IRepositorioCompetencia repositorioCompetencia)
        {
            this.repositorioCompetencia = repositorioCompetencia ?? throw new ArgumentNullException(nameof(repositorioCompetencia));
        }

        public async Task<IEnumerable<RetornoCompetenciaDto>> Handle(ObterCompetenciasPorMatrizIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioCompetencia.ObterCompetenciasPorMatrizId(request.MatrizId);
        }
    }
}
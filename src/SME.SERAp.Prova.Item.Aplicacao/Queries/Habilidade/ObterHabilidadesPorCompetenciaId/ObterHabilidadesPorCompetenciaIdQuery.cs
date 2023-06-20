using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaIdQuery : IRequest<IEnumerable<RetornoHabilidadeDto>>
    {
        public ObterHabilidadesPorCompetenciaIdQuery(long competenciaId)
        {
            CompetenciaId = competenciaId;
        }

        public long CompetenciaId { get; }
    }
}
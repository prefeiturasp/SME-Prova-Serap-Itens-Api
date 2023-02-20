using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizIdQuery : IRequest<IEnumerable<RetornoCompetenciaDto>>
    {
        public ObterCompetenciasPorMatrizIdQuery(long matrizId)
        {
            MatrizId = matrizId;
        }

        public long MatrizId { get; }        
    }
}
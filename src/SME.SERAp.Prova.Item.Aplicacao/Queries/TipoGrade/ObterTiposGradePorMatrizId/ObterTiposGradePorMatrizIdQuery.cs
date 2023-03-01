using System.Collections.Generic;
using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTiposGradePorMatrizIdQuery : IRequest<IEnumerable<RetornoTipoGradeDto>>
    {
        public ObterTiposGradePorMatrizIdQuery(long matrizId)
        {
            MatrizId = matrizId;
        }

        public long MatrizId { get; }
    }
}
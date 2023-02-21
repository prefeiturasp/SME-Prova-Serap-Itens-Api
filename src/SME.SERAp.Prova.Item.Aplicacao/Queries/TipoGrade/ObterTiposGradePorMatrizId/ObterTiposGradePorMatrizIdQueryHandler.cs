using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTiposGradePorMatrizIdQueryHandler : IRequestHandler<ObterTiposGradePorMatrizIdQuery, IEnumerable<RetornoTipoGradeDto>>
    {
        private readonly IRepositorioTipoGrade repositorioTipoGrade;

        public ObterTiposGradePorMatrizIdQueryHandler(IRepositorioTipoGrade repositorioTipoGrade)
        {
            this.repositorioTipoGrade = repositorioTipoGrade ?? throw new ArgumentNullException(nameof(repositorioTipoGrade));
        }

        public async Task<IEnumerable<RetornoTipoGradeDto>> Handle(ObterTiposGradePorMatrizIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTipoGrade.ObterTiposGradePorMatrizId(request.MatrizId);
        }
    }
}
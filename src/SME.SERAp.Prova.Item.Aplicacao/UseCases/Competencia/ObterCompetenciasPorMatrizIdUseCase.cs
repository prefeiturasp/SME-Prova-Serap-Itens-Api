using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizIdUseCase : IObterCompetenciasPorMatrizIdUseCase
    {
        private readonly IMediator mediator;

        public ObterCompetenciasPorMatrizIdUseCase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IEnumerable<RetornoCompetenciaDto>> Executar(long matrizId)
        {
            return await mediator.Send(new ObterCompetenciasPorMatrizIdQuery(matrizId));
        }
    }
}
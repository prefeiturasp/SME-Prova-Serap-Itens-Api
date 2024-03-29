using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizIdUseCase : AbstractUseCase, IObterCompetenciasPorMatrizIdUseCase
    {
        public ObterCompetenciasPorMatrizIdUseCase(IMediator mediator) : base(mediator)
        {
        }
        
        public async Task<IEnumerable<SelectDto>> Executar(long matrizId)
        {
            var competencias = await mediator.Send(new ObterCompetenciasPorMatrizIdQuery(matrizId));

            return competencias.Select(c => new SelectDto
            {
                Valor = c.Id,
                Descricao = c.Descricao
            });
        }
    }
}
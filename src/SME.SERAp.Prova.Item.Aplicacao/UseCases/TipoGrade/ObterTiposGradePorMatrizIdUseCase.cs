using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTiposGradePorMatrizIdUseCase : AbstractUseCase, IObterTiposGradePorMatrizIdUseCase
    {
        public ObterTiposGradePorMatrizIdUseCase(IMediator mediator) : base(mediator)
        {
        }
        
        public async Task<IEnumerable<SelectDto>> Executar(long matrizId)
        {
            var tiposGrade = await mediator.Send(new ObterTiposGradePorMatrizIdQuery(matrizId));
            
            return tiposGrade.Select(c => new SelectDto
            {
                Valor = c.Id,
                Descricao = c.Descricao
            });            
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterAreasConhecimentoUseCase : AbstractUseCase, IObterAreasConhecimentoUseCase
    {
        public ObterAreasConhecimentoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var listaAreaConhecimento =  await mediator.Send(new ObterAreasConhecimentoQuery());

            if(listaAreaConhecimento != null)
            return listaAreaConhecimento.Select(x => new SelectDto(x.Id, x.Descricao));
            return null;
            
        }
    }
}

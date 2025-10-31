using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterItemComAlternativaPorIdUseCase : AbstractUseCase, IObterItemComAlternativaPorIdUseCase
    {
        public ObterItemComAlternativaPorIdUseCase(IMediator mediator) : base(mediator)
        {
               
        }

        public async Task<ItemConsulta> Executar(long itemId)
        {
            return await mediator.Send(new ObterItemComAlternativaPorIdQuery(itemId));
        }
    }
}

using MediatR;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterItemPorIdUseCase : AbstractUseCase, IObterItemPorIdUseCase
    {
        public ObterItemPorIdUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<ItemConsulta> Executar(long itemId)
        {
            return await mediator.Send(new ObterItemPorIdQuery(itemId));
        }
    }
}

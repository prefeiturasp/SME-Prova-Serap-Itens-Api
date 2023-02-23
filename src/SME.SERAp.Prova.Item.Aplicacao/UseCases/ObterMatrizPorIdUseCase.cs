using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorIdUseCase : AbstractUseCase, IObterMatrizPorIdUseCase
    {
        public ObterMatrizPorIdUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<Matriz> Executar(long matrizId)
        {
            return await mediator.Send(new ObterMatrizPorIdQuery(matrizId));
        }
    }
}

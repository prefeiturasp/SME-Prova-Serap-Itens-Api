using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class InserirTesteUseCase : AbstractUseCase, IInserirTesteUseCase
    {
        public InserirTesteUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<long> Executar(string descricao)
        {
            return await mediator.Send(new InserirTesteCommand(descricao));
        }
    }
}

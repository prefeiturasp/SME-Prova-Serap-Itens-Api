using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinaCompletaPorIdUseCase : AbstractUseCase, IObterDisciplinaCompletaPorIdUseCase
    {
        public ObterDisciplinaCompletaPorIdUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<Disciplina> Executar(long disciplinaId)
        {
            if (disciplinaId > 0)
                return await mediator.Send(new ObterDisciplinaPorIdQuery(disciplinaId));
            return null;
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterDisciplinasPorAreaConhecimentoUseCase : AbstractUseCase, IObterDisciplinasPorAreaConhecimento
    {
        public ObterDisciplinasPorAreaConhecimentoUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<IEnumerable<Disciplina>> Executar(long areaConhecimentoId)
        {
            return await mediator.Send(new ObterDisciplinasPorAreaConhecimentoIdQuery(areaConhecimentoId));
        }

    }
}

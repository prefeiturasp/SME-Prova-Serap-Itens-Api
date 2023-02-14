using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterDisciplinasPorAreaConhecimentoUseCase : AbstractUseCase, IObterDisciplinasPorAreaConhecimento
    {
        public ObterDisciplinasPorAreaConhecimentoUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<IEnumerable<SelectDto>> Executar(long areaConhecimentoId)
        {
            var listaDisciplinas =  await mediator.Send(new ObterDisciplinasPorAreaConhecimentoIdQuery(areaConhecimentoId));


            if (listaDisciplinas != null)
                return listaDisciplinas.Select(x => new SelectDto(x.Id, x.Descricao));
            return null;

        }

    }
}

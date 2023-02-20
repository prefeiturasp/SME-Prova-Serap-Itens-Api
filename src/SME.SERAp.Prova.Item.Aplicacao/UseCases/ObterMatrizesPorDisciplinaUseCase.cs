using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizesPorDisciplinaUseCase : AbstractUseCase, IObterMatrizesPorDisciplinaUseCase
    {
        public ObterMatrizesPorDisciplinaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar(long disciplinaId)
        {
           var listaMatrizes =  await mediator.Send(new ObterMatrizesPorDisciplinaIdQuery(disciplinaId));


            if (listaMatrizes != null)
                return listaMatrizes.Select(x => new SelectDto(x.Id, x.Descricao));
            return null;
        }
    }
}

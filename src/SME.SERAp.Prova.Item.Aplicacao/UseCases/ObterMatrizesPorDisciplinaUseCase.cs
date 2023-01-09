using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizesPorDisciplinaUseCase : AbstractUseCase, IObterMatrizesPorDisciplinaUseCase
    {
        public ObterMatrizesPorDisciplinaUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<Matriz>> Executar(long disciplinaId)
        {
          return await mediator.Send(new ObterMatrizesPorDisciplinaIdQuery(disciplinaId));
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubAssuntosPorAssuntoIdUseCase : AbstractUseCase, IObterSubAssuntosPorAssuntoIdUseCase
    {
        public ObterSubAssuntosPorAssuntoIdUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar(long assuntoId)
        {
            var listaSubAssuntos = await mediator.Send(new ObterSubAssuntoPorAssuntoIdQuery(assuntoId));
            return listaSubAssuntos?.Select(x => new SelectDto(x.Id, x.Descricao)).OrderBy(x => x.Descricao);
        }
    }
}
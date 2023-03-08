using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterAssuntosUseCase : AbstractUseCase, IObterAssuntosUseCase
    {
        public ObterAssuntosUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var listaAssuntos = await mediator.Send(new ObterAssuntosQuery());
            return listaAssuntos?.Select(x => new SelectDto(x.Id, x.Descricao)).OrderBy(x => x.Descricao);
        }
    }
}

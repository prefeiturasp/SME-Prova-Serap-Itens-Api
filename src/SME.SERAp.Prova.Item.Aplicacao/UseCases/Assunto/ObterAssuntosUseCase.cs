using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterAssuntosUseCase : AbstractUseCase, IObterAssuntosUseCase
    {
        public ObterAssuntosUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<IEnumerable<SelectDto>> Executar(long disciplinaId)
        {
            var listaAssuntos = await mediator.Send(new ObterAssuntosQuery(disciplinaId));
            return listaAssuntos?.Select(x => new SelectDto(x.Id, x.Descricao)).OrderBy(x => x.Descricao);
        }
    }
}

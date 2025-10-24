using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterListaCodigoItensUseCase : AbstractUseCase, IObterListaCodigoItensUseCase
    {
        public ObterListaCodigoItensUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar(long? codigoItem)
        {
            var listaCodigoItem = await mediator.Send(new ObterListaCodigoItensQuery(codigoItem));
            if (listaCodigoItem != null && listaCodigoItem.Any())
            {
                return listaCodigoItem
                    .OrderBy(o => o.CodigoItem)
                    .Select(s => new SelectDto(s.Id, s.CodigoItem.ToString()));
            }

            return default;
        }
    }
}
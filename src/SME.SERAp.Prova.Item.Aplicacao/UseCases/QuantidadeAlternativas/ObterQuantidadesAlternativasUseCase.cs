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
    public class ObterQuantidadesAlternativasUseCase : AbstractUseCase, IObterQuantidadesAlternativasUseCase
    {
        public ObterQuantidadesAlternativasUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var listaQuantidadeAlternativas = await mediator.Send(new ObterQuantidadeAlternativasQuery());
            return listaQuantidadeAlternativas?.Select(x => new SelectDto(x.Id, x.Descricao)).OrderBy(x => x.Descricao);
        }
    }
}

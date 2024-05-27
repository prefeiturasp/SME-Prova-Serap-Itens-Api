using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterQuantidadesAlternativasUseCase : AbstractUseCase, IObterQuantidadesAlternativasUseCase
    {
        public ObterQuantidadesAlternativasUseCase(IMediator mediator) : base(mediator)
        {

        }

        public async Task<IEnumerable<QuantidadeAlternativasDto>> Executar()
        {
            return await mediator.Send(new ObterListaQuantidadeAlternativasQuery());
        }
    }
}

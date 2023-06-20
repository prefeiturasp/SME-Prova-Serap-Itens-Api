using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterDificuldadesUseCase : AbstractUseCase, IObterDificuldadesUseCase
    {
        public ObterDificuldadesUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var dificuldades = await mediator.Send(new ObterIdDescricaoOrdemQuery());
            if (dificuldades != null && dificuldades.Any())
            {
                return dificuldades
                    .OrderBy(o => o.Ordem)
                    .Select(s => new SelectDto(s.Id, $"{s.Ordem} - {s.Descricao}"));
            }

            return default;
        }
    }
}

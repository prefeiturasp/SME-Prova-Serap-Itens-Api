using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.NivelItem.ObterIdNivelItemOrdem;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterNivelItemUseCase : AbstractUseCase, IObterNivelItemUseCase
    {
        public ObterNivelItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var NivelItem = await mediator.Send(new ObterNivelItemQuery());
            if (NivelItem != null && NivelItem.Any())
            {
                return NivelItem
                    .OrderBy(o => o.Ordem)
                    .Select(s => new SelectDto(s.Id, $"{s.Ordem} - {s.Descricao}"));
            }

            return default;
        }
    }
}
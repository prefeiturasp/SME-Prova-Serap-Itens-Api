using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class SalvarItemUseCase : AbstractUseCase, ISalvarItemUseCase
    {
        public SalvarItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<long> Executar(ItemDto itemDto)
        {
            var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorLegadoIdQuery(itemDto.AreaConhecimentoLegadoId));

            if (areaConhecimento == null)
                throw new Exception($"A area de conhecimento com o id legado: {itemDto.AreaConhecimentoLegadoId} não foi encontrada.");

            var disciplina = await mediator.Send(new ObterDisciplinaPorLegadoIdQuery(itemDto.DisciplinaLegadoId));

            if (disciplina == null)
                throw new Exception($"A disciplina com o id legado: {itemDto.DisciplinaLegadoId} não foi encontrada.");

            if (itemDto.Id == null || itemDto.Id <= 0)
                itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));

            var item = new Dominio.Entities.Item(
                itemDto?.Id, itemDto.CodigoItem, itemDto.AreaConhecimentoLegadoId, itemDto.MatrizLegadoId, itemDto.DisciplinaLegadoId);
            return await mediator.Send(new SalvarItemCommand(item));
        }

    }
}

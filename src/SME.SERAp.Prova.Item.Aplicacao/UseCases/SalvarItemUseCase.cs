using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
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
            var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorIdQuery(itemDto.AreaConhecimentoId));

            if (areaConhecimento == null)
                throw new Exception($"A area de conhecimento com o id: {itemDto.AreaConhecimentoId} não foi encontrada.");

            var disciplina = await mediator.Send(new ObterDisciplinaPorIdQuery(itemDto.DisciplinaId));

            if (disciplina == null)
                throw new Exception($"A disciplina com o id: {itemDto.DisciplinaId} não foi encontrada.");

            var matriz = await mediator.Send(new ObterMatrizPorIdQuery(itemDto.MatrizId));
            if (matriz == null)
                throw new Exception($"A matriz com o id: {itemDto.MatrizId} não foi encontrada.");

            if (itemDto.Id == null || itemDto.Id <= 0)
                itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));

            var item = new Dominio.Entities.Item(
                itemDto?.Id, itemDto.CodigoItem, areaConhecimento.LegadoId, matriz.LegadoId, disciplina.LegadoId);
            return await mediator.Send(new SalvarItemCommand(item));
        }

    }
}

using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
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

            if (itemDto.Id == null || itemDto.Id <= 0)
                itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));

            //Validar Range discriminacao dificuldade e AcertoCasual
            Dominio.Entities.Item item = MapItemDto(itemDto, areaConhecimento, disciplina);
            return await mediator.Send(new SalvarItemCommand(item));
        }

        private static Dominio.Entities.Item MapItemDto(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            return new Dominio.Entities.Item(
                            itemDto?.Id, itemDto.CodigoItem, 
                            areaConhecimento.Id, disciplina.Id, 
                            itemDto.MatrizId, itemDto.CompetenciaId,
                            itemDto.HabilidadeId, itemDto.AnoMatrizId,
                            itemDto.DificuldadeSugeridaId, itemDto.Discriminacao, 
                            itemDto.AcertoCasual, itemDto.Dificuldade);
        }

    }
}

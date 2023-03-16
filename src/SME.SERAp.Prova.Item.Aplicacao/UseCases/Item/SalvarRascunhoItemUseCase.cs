using MediatR;
using Nest;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class SalvarRascunhoItemUseCase : AbstractUseCase, ISalvarRascunhoItemUseCase
    {
        public SalvarRascunhoItemUseCase(IMediator mediator) : base(mediator)
        {
        }
        public async Task<long> Executar(ItemRascunhoDto itemDto)
        {
            
            var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorIdQuery(itemDto.AreaConhecimentoId));
            if (areaConhecimento == null)
                throw new Exception($"A area de conhecimento com o id: {itemDto.AreaConhecimentoId} não foi encontrada.");

            var disciplina = await mediator.Send(new ObterDisciplinaPorIdQuery(itemDto.DisciplinaId));
            if (disciplina == null)
                throw new Exception($"A disciplina com o id: {itemDto.DisciplinaId} não foi encontrada.");

            if (itemDto.Id == null || itemDto.Id <= 0)
                itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));

            if ((itemDto.Id != null || itemDto.Id <= 0) && itemDto.CodigoItem == 0)
                throw new Exception($"O codigo do item não pode ser zero, pois o item já existe na base de dados");

            Dominio.Entities.Item item = MapItemDto(itemDto, areaConhecimento, disciplina);

            return await mediator.Send(new SalvarItemCommand(item));

        }

        private static Dominio.Entities.Item MapItemDto(ItemRascunhoDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            // CRIAR QUERY PARA ISSO 
            string palavrasChave = string.Empty;

            if (itemDto.PalavrasChave?.Length > 0)
                palavrasChave = string.Join(";", itemDto.PalavrasChave);


            return new Dominio.Entities.Item(
                            itemDto?.Id, itemDto.CodigoItem,
                            areaConhecimento.Id, disciplina.Id,
                            itemDto.MatrizId, itemDto.CompetenciaId,
                            itemDto.HabilidadeId, itemDto.AnoMatrizId,
                            itemDto.DificuldadeSugeridaId, itemDto.Discriminacao,
                            itemDto.AcertoCasual, itemDto.Dificuldade, itemDto.AssuntoId,
                            itemDto.SubAssuntoId, itemDto.Situacao, itemDto.Tipo,
                            itemDto.QuantidadeAlternativasId, palavrasChave,
                            itemDto.ParametroBTransformado, itemDto.MediaEhDesvio,
                            itemDto.Observacao, DateTime.Now);
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Aplicacao.Commands;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class SalvarRascunhoItemUseCase : AbstractUseCase, ISalvarRascunhoItemUseCase
    {
        public SalvarRascunhoItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<long> Executar(ItemRascunhoDto itemRascunhoDto)
        {
            var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorIdQuery(itemRascunhoDto.AreaConhecimentoId));
            if (areaConhecimento == null)
                throw new Exception($"A area de conhecimento com o id: {itemRascunhoDto.AreaConhecimentoId} não foi encontrada.");

            var disciplina = await mediator.Send(new ObterDisciplinaPorIdQuery(itemRascunhoDto.DisciplinaId));
            if (disciplina == null)
                throw new Exception($"A disciplina com o id: {itemRascunhoDto.DisciplinaId} não foi encontrada.");

            if (itemRascunhoDto.Id == null || itemRascunhoDto.Id <= 0)
                itemRascunhoDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));

            if ((itemRascunhoDto.Id != null || itemRascunhoDto.Id <= 0) && itemRascunhoDto.CodigoItem == 0)
                throw new Exception($"O codigo do item não pode ser zero, pois o item já existe na base de dados");

            var item = MapItemDto(itemRascunhoDto, areaConhecimento, disciplina);
            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (itemRascunhoDto.AlternativasDto != null)
                await TrataAlternativas(itemRascunhoDto, itemId);
            
            if(itemRascunhoDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemRascunhoDto, itemId);

            if (itemRascunhoDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemRascunhoDto, itemId);            

            return itemId;
        }
        
        private async Task TrataArquivoAudio(ItemRascunhoDto itemRascunhoDto, long itemId)
        {
            var itemAudio = new ItemAudio(itemRascunhoDto.ArquivoAudioId, itemId, 1, DateTime.Now);
            await mediator.Send(new SalvarItemAudioCommand(itemAudio));
        }

        private async Task TrataArquivoVideo(ItemRascunhoDto itemRascunhoDto, long itemId)
        {
            var itemVideo = new ItemVideo(itemRascunhoDto.ArquivoVideoId, itemId, 1, DateTime.Now);
            await mediator.Send(new SalvarItemVideoCommand(itemVideo));
        }        

        private async Task TrataAlternativas(ItemRascunhoDto itemRascunhoDto, long itemId)
        {
            foreach (var altDto in itemRascunhoDto.AlternativasDto)
            {
                var alternativa = new Alternativa(altDto.Descricao, altDto.Justificativa, altDto.Numeracao,
                    altDto.Correta, altDto.Ordem, DateTime.Now, itemId);

                await mediator.Send(new SalvarAlternativaCommand(alternativa));
            }
        }

        private static Dominio.Entities.Item MapItemDto(ItemRascunhoDto itemRascunhoDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            // CRIAR QUERY PARA ISSO 
            var palavrasChave = string.Empty;
            if (itemRascunhoDto.PalavrasChave?.Length > 0)
                palavrasChave = string.Join(";", itemRascunhoDto.PalavrasChave);

            return new Dominio.Entities.Item(
                itemRascunhoDto.Id, itemRascunhoDto.CodigoItem,
                areaConhecimento.Id, disciplina.Id,
                itemRascunhoDto.MatrizId, itemRascunhoDto.CompetenciaId,
                itemRascunhoDto.HabilidadeId, itemRascunhoDto.AnoMatrizId,
                itemRascunhoDto.DificuldadeSugeridaId, itemRascunhoDto.Discriminacao,
                itemRascunhoDto.AcertoCasual, itemRascunhoDto.Dificuldade, itemRascunhoDto.AssuntoId,
                itemRascunhoDto.SubAssuntoId, itemRascunhoDto.Situacao, itemRascunhoDto.Tipo,
                itemRascunhoDto.QuantidadeAlternativasId, palavrasChave,
                itemRascunhoDto.ParametroBTransformado, itemRascunhoDto.MediaEhDesvio,
                itemRascunhoDto.Observacao, DateTime.Now, itemRascunhoDto.TextoBase, itemRascunhoDto.Fonte, itemRascunhoDto.Enunciado);
        }
    }
}

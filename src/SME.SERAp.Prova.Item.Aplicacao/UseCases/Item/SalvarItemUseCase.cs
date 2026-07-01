using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Commands.PublicarFilaRabbit;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
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

            itemDto.Id = null;

            if (!string.IsNullOrEmpty(itemDto.CodigoItem))
            {
                var ultimaVersao = await mediator.Send(
                    new ObterUltimaVersaoItemPorCodigoQuery(itemDto.CodigoItem));

                if (ultimaVersao != null)
                {
                    itemDto.VersaoItem = ultimaVersao.VersaoItem + 1;
                    itemDto.CodigoItem = ultimaVersao.CodigoItem;
                }
                else
                {
                    itemDto.VersaoItem = 1;
                }
            }
            else
            {
                itemDto.CodigoItem = await mediator.Send(
                    new GeraCodigoItemQuery(areaConhecimento, disciplina));
                itemDto.VersaoItem = 1;
            }

            var item = MapItemDto(itemDto, areaConhecimento, disciplina);

            item.DataCriacao = DateTime.Now;
            item.DataAlteracao = DateTime.Now;

            item.Id = 0;

            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (itemDto.AlternativasDto != null)
                await TrataAlternativas(itemDto, itemId);

            if (itemDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemDto, itemId);

            if (itemDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemDto, itemId);

            var mensagemLegado = new ItemSalvarLegadoDto
            {
                ItemId = itemId,
                ItemDto = itemDto
            };

            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.ItemSalvarLegado, mensagemLegado));

            return itemId;
        }

        private async Task TrataArquivoAudio(ItemDto itemDto, long itemId)
        {
            var itemAudio = new ItemAudio(itemDto.ArquivoAudioId, itemId, 1, DateTime.Now);
            itemAudio.Id = 0;
            await mediator.Send(new SalvarItemAudioCommand(itemAudio));
        }

        private async Task TrataArquivoVideo(ItemDto itemDto, long itemId)
        {
            var itemVideo = new ItemVideo(itemDto.ArquivoVideoId, itemId, 1, DateTime.Now);
            itemVideo.Id = 0;
            await mediator.Send(new SalvarItemVideoCommand(itemVideo));
        }

        private async Task TrataAlternativas(ItemDto itemDto, long itemId)
        {
            foreach (var altDto in itemDto?.AlternativasDto)
            {
                var alternativa = new Alternativa(
                    null,
                    altDto.Descricao,
                    altDto.Justificativa,
                    altDto.Numeracao,
                    altDto.Correta,
                    altDto.Ordem,
                    DateTime.Now,
                    itemId);
                await mediator.Send(new SalvarAlternativaCommand(alternativa));
            }
        }

        private static Dominio.Entities.Item MapItemDto(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            var palavrasChave = string.Empty;
            if (itemDto.PalavrasChave?.Length > 0)
                palavrasChave = string.Join(";", itemDto.PalavrasChave);

            return new Dominio.Entities.Item(
                itemDto.CodigoItem,
                areaConhecimento.Id,
                disciplina.Id,
                itemDto.MatrizId,
                itemDto.CompetenciaId,
                itemDto.HabilidadeId,
                itemDto.AnoMatrizId,
                itemDto.DificuldadeSugeridaId,
                itemDto.Discriminacao,
                itemDto.AcertoCasual,
                itemDto.Dificuldade,
                itemDto.AssuntoId,
                itemDto.SubAssuntoId,
                itemDto.Situacao,
                itemDto.Tipo,
                itemDto.QuantidadeAlternativasId,
                palavrasChave,
                itemDto.ParametroBTransformado,
                itemDto.MediaEhDesvio,
                itemDto.Observacao,
                itemDto.SentencaDescritora,
                itemDto.NivelItem,
                itemDto.VersaoItem,
                itemDto.TextoBase,
                itemDto.Fonte,
                itemDto.Enunciado);
        }
    }
}
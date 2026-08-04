using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.RemoverAlternativasAusentesDto;
using SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVersao;
using SME.SERAp.Prova.Item.Aplicacao.Commands.PublicarFilaRabbit;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Rascunho;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoNovaVersaoPorCodigo;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoPorCodigo;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Fila;
using System;
using System.Linq;
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

            if (itemDto.Situacao == SituacaoItem.Rascunho)
                return await TrataRascunho(itemDto, areaConhecimento, disciplina);
            else
                return await TrataNovaVersao(itemDto, areaConhecimento, disciplina);
        }

        private async Task<long> TrataRascunho(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            bool isNovoRascunho = itemDto.Id == null || itemDto.Id <= 0;
            Dominio.Entities.Item itemExistente = null;

            if (isNovoRascunho)
            {
                if (!string.IsNullOrEmpty(itemDto.CodigoItem))
                {
                    var rascunhoExistente = await mediator.Send(
                        new ObterRascunhoNovaVersaoPorCodigoQuery(itemDto.CodigoItem));

                    if (rascunhoExistente != null)
                        throw new Exception($"Já existe um rascunho de nova versão para o item {itemDto.CodigoItem}. Envie o id do rascunho para editá-lo.");
                }

                itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));
                itemDto.VersaoItem = 0;
            }
            else
            {
                itemExistente = await mediator.Send(new ObterItemPorIdQuery(itemDto.Id.Value));

                if (itemExistente == null)
                    throw new Exception($"Rascunho com id {itemDto.Id.Value} não encontrado.");

                if (itemExistente.Situacao != SituacaoItem.Rascunho)
                    throw new Exception("Não é permitido atualizar um item que não está em situação de rascunho.");

                if (!string.IsNullOrEmpty(itemDto.CodigoItem) &&
                    itemDto.CodigoItem != itemExistente.CodigoItem)
                    throw new Exception("O código do item informado não corresponde ao rascunho encontrado.");

                itemDto.CodigoItem = itemExistente.CodigoItem;
                itemDto.VersaoItem = itemExistente.VersaoItem;
            }

            var item = MapItemDto(itemDto, areaConhecimento, disciplina);

            if (isNovoRascunho)
            {
                item.Id = 0;
                item.DataCriacao = DateTime.Now;
                item.DataAlteracao = DateTime.Now;
            }
            else
            {
                item.Id = itemDto.Id.Value;
                item.DataCriacao = itemExistente.DataCriacao;
                item.DataAlteracao = DateTime.Now;
            }

            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (itemDto.AlternativasDto != null)
                await TrataAlternativasRascunho(itemDto, itemId, isNovoRascunho);

            if (itemDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemDto, itemId);

            if (itemDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemDto, itemId);

            return itemId;
        }

        private async Task<long> TrataNovaVersao(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            itemDto.Id = null;

            if (!string.IsNullOrEmpty(itemDto.CodigoItem))
            {
                var ultimaVersaoAtiva = await mediator.Send(
                    new ObterUltimaVersaoItemPorCodigoQuery(itemDto.CodigoItem));

                var rascunhoExistente = await mediator.Send(
                    new ObterRascunhoPorCodigoQuery(itemDto.CodigoItem));

                if (rascunhoExistente != null && ultimaVersaoAtiva == null)
                {
                    return await TrataAtivacaoRascunhoInicial(
                        itemDto, areaConhecimento, disciplina, rascunhoExistente);
                }
                else if (rascunhoExistente != null && ultimaVersaoAtiva != null)
                {
                    return await TrataAtivacaoRascunhoNovaVersao(
                        itemDto, areaConhecimento, disciplina, rascunhoExistente, ultimaVersaoAtiva);
                }
                else if (ultimaVersaoAtiva != null)
                {
                    itemDto.VersaoItem = ultimaVersaoAtiva.VersaoItem + 1;
                    itemDto.CodigoItem = ultimaVersaoAtiva.CodigoItem;
                    return await TrataRascunhoNovaVersao(itemDto, areaConhecimento, disciplina);
                }
                else
                {
                    throw new Exception($"Nenhum item ou rascunho encontrado com o código {itemDto.CodigoItem}.");
                }
            }
            else
            {
                throw new Exception("Não é possível ativar um item sem informar o código.");
            }
        }

        private async Task<long> TrataRascunhoNovaVersao(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            itemDto.Situacao = SituacaoItem.Rascunho;

            var item = MapItemDto(itemDto, areaConhecimento, disciplina);
            item.Id = 0;
            item.DataCriacao = DateTime.Now;
            item.DataAlteracao = DateTime.Now;

            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (itemDto.AlternativasDto != null)
                await TrataAlternativas(itemDto, itemId);

            if (itemDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemDto, itemId);

            if (itemDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemDto, itemId);

            return itemId;
        }

        private async Task<long> TrataAtivacaoRascunhoNovaVersao(
            ItemDto itemDto,
            AreaConhecimento areaConhecimento,
            Disciplina disciplina,
            Dominio.Entities.Item rascunhoNovaVersao,
            Dominio.Entities.Item ultimaVersaoAtiva)
        {
            itemDto.VersaoItem = rascunhoNovaVersao.VersaoItem;
            itemDto.CodigoItem = rascunhoNovaVersao.CodigoItem;
            itemDto.Situacao = SituacaoItem.Ativo;

            var item = MapItemDto(itemDto, areaConhecimento, disciplina);
            item.Id = rascunhoNovaVersao.Id;
            item.DataCriacao = rascunhoNovaVersao.DataCriacao;
            item.DataAlteracao = DateTime.Now;

            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (ultimaVersaoAtiva != null)
                await mediator.Send(new InativarVersoesAnterioresItemCommand(
                    itemDto.CodigoItem,
                    itemDto.VersaoItem));

            await mediator.Send(new InativarRascunhoPorCodigoItemCommand(itemDto.CodigoItem));

            if (itemDto.AlternativasDto != null)
                await TrataAlternativasRascunho(itemDto, itemId, isNovoRascunho: false);

            if (itemDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemDto, itemId);

            if (itemDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemDto, itemId);

            await mediator.Send(new PublicaFilaRabbitCommand(
                RotaRabbit.ItemSalvarLegado, new ItemSalvarLegadoDto
                {
                    ItemId = itemId,
                    ItemDto = itemDto
                }));

            return itemId;
        }

        private async Task<long> TrataAtivacaoRascunhoInicial(ItemDto itemDto, AreaConhecimento areaConhecimento, Disciplina disciplina, Dominio.Entities.Item rascunhoExistente)
        {
            itemDto.VersaoItem = 1;
            itemDto.CodigoItem = rascunhoExistente.CodigoItem;
            itemDto.Situacao = SituacaoItem.Ativo;

            var item = MapItemDto(itemDto, areaConhecimento, disciplina);
            item.Id = rascunhoExistente.Id;
            item.DataCriacao = rascunhoExistente.DataCriacao;
            item.DataAlteracao = DateTime.Now;

            var itemId = await mediator.Send(new SalvarItemCommand(item));

            if (itemDto.AlternativasDto != null)
                await TrataAlternativasRascunho(itemDto, itemId, isNovoRascunho: false);

            if (itemDto.ArquivoAudioId > 0)
                await TrataArquivoAudio(itemDto, itemId);

            if (itemDto.ArquivoVideoId > 0)
                await TrataArquivoVideo(itemDto, itemId);

            await mediator.Send(new PublicaFilaRabbitCommand(
                RotaRabbit.ItemSalvarLegado, new ItemSalvarLegadoDto
                {
                    ItemId = itemId,
                    ItemDto = itemDto
                }));

            return itemId;
        }

        private async Task TrataAlternativasRascunho(ItemDto itemDto, long itemId, bool isNovoRascunho)
        {
            if (itemDto.AlternativasDto == null) return;

            if (!isNovoRascunho)
            {
                var idsAlternativasManter = itemDto.AlternativasDto
                    .Where(a => a.Id > 0)
                    .Select(a => a.Id.Value)
                    .ToList();

                await mediator.Send(new RemoverAlternativasAusentesDtoCommand(itemId, idsAlternativasManter));
            }

            foreach (var altDto in itemDto.AlternativasDto)
            {
                var alternativa = new Alternativa(
                    altDto.Id > 0 ? altDto.Id : (long?)null,
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
            foreach (var altDto in itemDto.AlternativasDto)
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
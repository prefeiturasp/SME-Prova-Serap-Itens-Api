using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterAlternativasBrutasPorItemId;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Item
{
    public class ObterItemResumoPorIdUseCase : AbstractUseCase, IObterItemResumoPorIdUseCase
    {
        private readonly IRepositorioItem repositorioItem;

        public ObterItemResumoPorIdUseCase(IMediator mediator, IRepositorioItem repositorioItem) : base(mediator)
        {
            this.repositorioItem = repositorioItem;
        }

        public async Task<ItemResumoDto> Executar(long itemId)
        {
            var item = await repositorioItem.ObterUltimaVersaoItemPorId(itemId);

            if (item == null)
            {
                return null;
            }

            var alternativas = await mediator.Send(new ObterAlternativasBrutasPorItemIdQuery(item.Id));
            var versoesItens = (await mediator.Send(new ObterTodasVersoesPorCodigoItemQuery(item.CodigoItem))).ToList();

            var listaVersoesDto = versoesItens
                .Select(v => new ItemVersaoDto(v.Id, v.CodigoItem, v.VersaoItem, v.DataCriacao))
                .OrderBy(v => v.VersaoItem)
                .ToList();

            var quantidadeVersoes = versoesItens.Count;

            var itemResumoDto = new ItemResumoDto(
                item.Id,
                item.CodigoItem,
                item.TextoBase,
                item.Enunciado,
                item.Fonte,
                item.VersaoItem,
                quantidadeVersoes);

            itemResumoDto.VersoesDisponiveis = listaVersoesDto;

            itemResumoDto.Alternativas.AddRange(alternativas.Select(a => new AlternativaResumoDto(
                a.Id,
                a.ItemId,
                a.Descricao,
                (int)a.Ordem.GetValueOrDefault(),
                a.Numeracao)));

            return itemResumoDto;
        }
    }
}
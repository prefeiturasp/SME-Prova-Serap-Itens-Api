using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Item.AtualizarSituacaoItem;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo;
using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Item
{
    public class AtualizarSituacaoItemUseCase : AbstractUseCase, IAtualizarSituacaoItemUseCase
    {
        public AtualizarSituacaoItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(string codigoItem, long versaoItem, int situacao)
        {
            if (string.IsNullOrEmpty(codigoItem))
                throw new Exception("O código do item é obrigatório.");

            if (versaoItem <= 0)
                throw new Exception("A versão do item é obrigatória.");

            if (!Enum.IsDefined(typeof(SituacaoItem), situacao))
                throw new Exception("Situação informada é inválida.");

            var situacaoItem = (SituacaoItem)situacao;

            var itemExistente = await mediator.Send(
                new ObterUltimaVersaoItemPorCodigoQuery(codigoItem));

            if (itemExistente == null)
                throw new Exception($"Item com código {codigoItem} e versão {versaoItem} não encontrado.");

            if (itemExistente.Situacao == SituacaoItem.Rascunho)
                throw new Exception("Não é permitido alterar a situação de um rascunho por este endpoint.");

            return await mediator.Send(new AtualizarSituacaoItemCommand(
                codigoItem,
                versaoItem,
                situacaoItem));
        }
    }
}
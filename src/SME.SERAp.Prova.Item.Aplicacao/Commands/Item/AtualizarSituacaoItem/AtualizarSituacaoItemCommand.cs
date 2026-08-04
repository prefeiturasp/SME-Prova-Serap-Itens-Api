using MediatR;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Item.AtualizarSituacaoItem
{
    public class AtualizarSituacaoItemCommand : IRequest<bool>
    {
        public AtualizarSituacaoItemCommand(string codigoItem, long versaoItem, SituacaoItem situacao)
        {
            CodigoItem = codigoItem;
            VersaoItem = versaoItem;
            Situacao = situacao;
        }

        public string CodigoItem { get; }
        public long VersaoItem { get; }
        public SituacaoItem Situacao { get; }
    }
}
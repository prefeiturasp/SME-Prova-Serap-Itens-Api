using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVersao
{
    public class InativarVersoesAnterioresItemCommand : IRequest<bool>
    {
        public InativarVersoesAnterioresItemCommand(string codigoItem, long versaoAtual)
        {
            CodigoItem = codigoItem;
            VersaoAtual = versaoAtual;
        }

        public string CodigoItem { get; }
        public long VersaoAtual { get; }
    }
}
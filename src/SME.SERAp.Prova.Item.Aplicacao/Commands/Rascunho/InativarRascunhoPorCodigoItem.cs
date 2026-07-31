using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Rascunho
{
    public class InativarRascunhoPorCodigoItemCommand : IRequest<bool>
    {
        public InativarRascunhoPorCodigoItemCommand(string codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public string CodigoItem { get; }
    }
}
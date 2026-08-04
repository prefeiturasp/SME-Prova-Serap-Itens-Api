using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoNovaVersaoPorCodigo
{
    public class ObterRascunhoNovaVersaoPorCodigoQuery : IRequest<Dominio.Entities.Item>
    {
        public ObterRascunhoNovaVersaoPorCodigoQuery(string codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public string CodigoItem { get; }
    }
}
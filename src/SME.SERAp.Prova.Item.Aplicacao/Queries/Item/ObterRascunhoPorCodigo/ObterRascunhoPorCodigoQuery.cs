using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoPorCodigo
{
    public class ObterRascunhoPorCodigoQuery : IRequest<Dominio.Entities.Item>
    {
        public ObterRascunhoPorCodigoQuery(string codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public string CodigoItem { get; }
    }
}
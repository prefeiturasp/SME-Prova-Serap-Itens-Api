using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;
    public class ObterUltimaVersaoItemPorCodigoQuery : IRequest<DominioItem>
    {
        public string CodigoItem { get; set; }

        public ObterUltimaVersaoItemPorCodigoQuery(string codigoItem)
        {
            CodigoItem = codigoItem;
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Collections.Generic;


namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterListaCodigoItensQuery : IRequest<IEnumerable<CodigoItemDto>>
    {
        public ObterListaCodigoItensQuery(long? codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public long? CodigoItem { get; set; }

    }
}
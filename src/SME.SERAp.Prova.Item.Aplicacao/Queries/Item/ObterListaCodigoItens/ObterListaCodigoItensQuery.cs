using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Collections.Generic;


namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterListaCodigoItensQuery : IRequest<IEnumerable<CodigoItemDto>>
    {
        public ObterListaCodigoItensQuery(string codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public string CodigoItem { get; set; }

    }
}
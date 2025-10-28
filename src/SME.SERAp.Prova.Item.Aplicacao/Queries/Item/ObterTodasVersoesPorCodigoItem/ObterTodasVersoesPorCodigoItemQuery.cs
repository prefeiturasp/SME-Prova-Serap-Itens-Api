using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterTodasVersoesPorCodigoItem
{
    public class ObterTodasVersoesPorCodigoItemQuery : IRequest<IEnumerable<Dominio.Entities.Item>>
    {
        public ObterTodasVersoesPorCodigoItemQuery(long codigoItem)
        {
            CodigoItem = codigoItem;
        }

        public long CodigoItem { get; set; }
    }
}
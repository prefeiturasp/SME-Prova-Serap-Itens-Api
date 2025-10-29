using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemBasePorId
{
    public class ObterItemBasePorIdQuery : IRequest<Dominio.Entities.Item>
    {
        public ObterItemBasePorIdQuery(long itemId)
        {
            ItemId = itemId;
        }

        public long ItemId { get; set; }
    }
}
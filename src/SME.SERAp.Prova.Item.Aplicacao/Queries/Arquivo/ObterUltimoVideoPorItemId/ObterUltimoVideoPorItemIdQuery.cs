using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoVideoPorItemId
{
    public class ObterUltimoVideoPorItemIdQuery : IRequest<ItemVideoDto>
    {
        public ObterUltimoVideoPorItemIdQuery(long itemId)
        {
            ItemId = itemId;
        }
        public long ItemId { get; }
    }
}
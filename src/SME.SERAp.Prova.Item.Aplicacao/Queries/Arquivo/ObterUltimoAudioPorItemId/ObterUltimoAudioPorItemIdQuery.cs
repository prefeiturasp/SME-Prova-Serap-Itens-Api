using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoAudioPorItemId
{
    public class ObterUltimoAudioPorItemIdQuery : IRequest<ItemAudioDto>
    {
        public ObterUltimoAudioPorItemIdQuery(long itemId)
        {
            ItemId = itemId;
        }
        public long ItemId { get; }
    }
}
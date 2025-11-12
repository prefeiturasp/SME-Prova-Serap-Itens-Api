using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries;
public class ObterArquivosAudioVideoPorItemIdQuery : IRequest<ArquivosItemDto>
{
    public ObterArquivosAudioVideoPorItemIdQuery(long itemId)
    {
        ItemId = itemId;
    }
    public long ItemId { get; set; }
}

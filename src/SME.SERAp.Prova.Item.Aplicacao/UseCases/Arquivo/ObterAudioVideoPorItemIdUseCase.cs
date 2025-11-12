using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using System;

using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Arquivo
{
    public class ObterAudioVideoPorItemIdUseCase : AbstractUseCase, IObterAudioVideoPorItemIdUseCase
    {
        public ObterAudioVideoPorItemIdUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<ArquivosItemDto> ExecutarAsync(long itemId)
        {
            if (itemId == 0)
                throw new ArgumentException("ItemId inválido.");
            var arquivosItemDto = await mediator.Send(new ObterArquivosAudioVideoPorItemIdQuery(itemId));
            return arquivosItemDto;
        }
    }
}

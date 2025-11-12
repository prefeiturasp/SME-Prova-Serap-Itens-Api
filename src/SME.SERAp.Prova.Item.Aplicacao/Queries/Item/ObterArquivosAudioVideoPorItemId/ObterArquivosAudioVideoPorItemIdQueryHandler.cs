using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterArquivosAudioVideoPorItemIdQueryHandler : IRequestHandler<ObterArquivosAudioVideoPorItemIdQuery, ArquivosItemDto>
    {
        private readonly IRepositorioArquivo repositorioArquivo;

        public ObterArquivosAudioVideoPorItemIdQueryHandler(IRepositorioArquivo repositorioArquivo)
        {
            this.repositorioArquivo = repositorioArquivo ?? throw new ArgumentNullException(nameof(repositorioArquivo));
        }

        public async Task<ArquivosItemDto> Handle(ObterArquivosAudioVideoPorItemIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioArquivo.ObterArquivosAudioVideoPorItemId(request.ItemId);
        }
    }
}
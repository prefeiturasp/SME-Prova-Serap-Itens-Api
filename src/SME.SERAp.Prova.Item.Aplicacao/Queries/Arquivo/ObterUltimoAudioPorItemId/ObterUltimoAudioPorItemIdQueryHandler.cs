using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoAudioPorItemId
{
    public class ObterUltimoAudioPorItemIdQueryHandler : IRequestHandler<ObterUltimoAudioPorItemIdQuery, ItemAudioDto>
    {
        private readonly IRepositorioArquivo repositorioArquivo;

        public ObterUltimoAudioPorItemIdQueryHandler(IRepositorioArquivo repositorioArquivo)
        {
            this.repositorioArquivo = repositorioArquivo ?? throw new ArgumentNullException(nameof(repositorioArquivo));
        }

        public async Task<ItemAudioDto> Handle(ObterUltimoAudioPorItemIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioArquivo.ObterUltimoAudioPorItemId(request.ItemId);
        }
    }
}
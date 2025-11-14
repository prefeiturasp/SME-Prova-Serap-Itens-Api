using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Arquivo.ObterUltimoVideoPorItemId
{
    public class ObterUltimoVideoPorItemIdQueryHandler : IRequestHandler<ObterUltimoVideoPorItemIdQuery, ItemVideoDto>
    {
        private readonly IRepositorioArquivo repositorioArquivo;

        public ObterUltimoVideoPorItemIdQueryHandler(IRepositorioArquivo repositorioArquivo)
        {
            this.repositorioArquivo = repositorioArquivo ?? throw new ArgumentNullException(nameof(repositorioArquivo));
        }

        public async Task<ItemVideoDto> Handle(ObterUltimoVideoPorItemIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioArquivo.ObterUltimoVideoPorItemId(request.ItemId);
        }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVideo.Inserir
{
    public class SalvarItemVideoCommandHandler : IRequestHandler<SalvarItemVideoCommand, long>
    {
        private readonly IRepositorioItemVideo repositorioItemVideo;

        public SalvarItemVideoCommandHandler(IRepositorioItemVideo repositorioItemVideo)
        {
            this.repositorioItemVideo = repositorioItemVideo ?? throw new ArgumentNullException(nameof(repositorioItemVideo));
        }

        public async Task<long> Handle(SalvarItemVideoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await repositorioItemVideo.SalvarAsync(request.ItemVideo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

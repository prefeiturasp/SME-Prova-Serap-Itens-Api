using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemAudioCommandHandler : IRequestHandler<SalvarItemAudioCommand, long>
    {
        private readonly IRepositorioItemAudio repositorioItemAudio;

        public SalvarItemAudioCommandHandler(IRepositorioItemAudio repositorioItemAudio)
        {
            this.repositorioItemAudio = repositorioItemAudio ?? throw new ArgumentNullException(nameof(repositorioItemAudio));
        }

        public async Task<long> Handle(SalvarItemAudioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await repositorioItemAudio.SalvarAsync(request.ItemAudio);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

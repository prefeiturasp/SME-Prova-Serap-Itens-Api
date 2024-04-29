using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemAudioCommand : IRequest<long>
    {
        public SalvarItemAudioCommand(Dominio.Entities.ItemAudio itemAudio)
        {
            ItemAudio = itemAudio;
        }

        public Dominio.Entities.ItemAudio ItemAudio { get; }
    }
}
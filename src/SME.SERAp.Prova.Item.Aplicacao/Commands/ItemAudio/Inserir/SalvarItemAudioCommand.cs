using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemAudioCommand : IRequest<long>
    {
        public SalvarItemAudioCommand(Dominio.Entities.ItemAudio itemAudio)
        {
            ItemAudio = itemAudio;
        }
        public Dominio.Entities.ItemAudio ItemAudio { get; set; }
    }
}
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SalvarSequencialItemCommand : IRequest<long>
    {
        public SalvarSequencialItemCommand(Dominio.Entities.SequencialItem sequencialItem)
        {
            SequencialItem = sequencialItem;
        }
        public Dominio.Entities.SequencialItem SequencialItem { get; set; }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SalvarItemCommand : IRequest<long>
    {
        public SalvarItemCommand(Dominio.Entities.Item item)
        {
            Item = item;
        }
        public Dominio.Entities.Item Item { get; set; }
    }
}

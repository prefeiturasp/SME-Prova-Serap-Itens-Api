using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubAssuntoPorAssuntoIdQuery : IRequest<IEnumerable<SubAssunto>>
    {
        public ObterSubAssuntoPorAssuntoIdQuery(long assuntoId)
        {
            AssuntoId = assuntoId;
        }

        public long AssuntoId { get; set; }
    }
}


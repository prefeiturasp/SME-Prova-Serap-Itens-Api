using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterCodigoAreaConhecimentoPorIdQuery : IRequest<long>
    {
        public ObterCodigoAreaConhecimentoPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosQuery : IRequest<IEnumerable<Assunto>>
    {
        public ObterAssuntosQuery(long disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }

        public long DisciplinaId { get; set; }
    }
}


using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizesPorDisciplinaIdQuery : IRequest<IEnumerable<Matriz>>
    {
        public ObterMatrizesPorDisciplinaIdQuery(long disciplinaId)
        {
            DisciplinaId = disciplinaId;
        }

        public long DisciplinaId { get; set; }
    }
}
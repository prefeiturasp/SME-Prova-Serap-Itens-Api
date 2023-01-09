using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;

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
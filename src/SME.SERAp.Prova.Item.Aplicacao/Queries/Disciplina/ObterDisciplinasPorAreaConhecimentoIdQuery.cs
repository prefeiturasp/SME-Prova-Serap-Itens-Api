using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinasPorAreaConhecimentoIdQuery : IRequest<IEnumerable<Disciplina>>
    {
        public ObterDisciplinasPorAreaConhecimentoIdQuery(long areaConhecimentoId)
        {
            AreaConhecimentoId = areaConhecimentoId;
        }

        public long AreaConhecimentoId { get; set; }
    }
}

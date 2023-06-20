using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterQtdPorAreaConhecimentoEhDisciplinaQuery : IRequest<long?>
    {
        public ObterQtdPorAreaConhecimentoEhDisciplinaQuery(long areaConhecimentoId, long disciplinaId)
        {
            AreaConhecimentoId = areaConhecimentoId;
            DisciplinaId = disciplinaId;
        }

        public long AreaConhecimentoId { get; set; }
        public long DisciplinaId { get; set; }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSequencialItemPorAreaEDisciplinaQuery : IRequest<SequencialItem>
    {
        public ObterSequencialItemPorAreaEDisciplinaQuery(long areaConhecimentoCodigo, long disciplinaCodigo)
        {
            AreaConhecimentoCodigo = areaConhecimentoCodigo;
            DisciplinaCodigo = disciplinaCodigo;
        }

        public long AreaConhecimentoCodigo { get; set; }
        public long DisciplinaCodigo { get; set; }
    }
}

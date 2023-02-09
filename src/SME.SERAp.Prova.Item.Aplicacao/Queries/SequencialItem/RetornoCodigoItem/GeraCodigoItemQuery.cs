using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class GeraCodigoItemQuery : IRequest<long>
    {
        public GeraCodigoItemQuery(AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            AreaConhecimento = areaConhecimento;
            Disciplina = disciplina;
        }

        public AreaConhecimento AreaConhecimento { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}

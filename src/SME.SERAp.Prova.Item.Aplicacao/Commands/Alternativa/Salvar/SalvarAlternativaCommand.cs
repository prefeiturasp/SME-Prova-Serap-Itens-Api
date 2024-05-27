using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa
{
    public class SalvarAlternativaCommand : IRequest<long>
    {
        public SalvarAlternativaCommand(Dominio.Entities.Alternativa alternativa)
        {
            Alternativa = alternativa;
        }
        public Dominio.Entities.Alternativa Alternativa { get; set; }
    }
}
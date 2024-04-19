using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarArquivoCommand : IRequest<long>
    {
        public SalvarArquivoCommand(Dominio.Entities.Arquivo arquivo)
        {
            Arquivo = arquivo;
        }
        public Dominio.Entities.Arquivo Arquivo { get; set; }
    }
}

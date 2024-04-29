using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarArquivoCommand : IRequest<long>
    {
        public SalvarArquivoCommand(Dominio.Entities.Arquivo arquivo)
        {
            Arquivo = arquivo;
        }

        public Dominio.Entities.Arquivo Arquivo { get; }
    }
}

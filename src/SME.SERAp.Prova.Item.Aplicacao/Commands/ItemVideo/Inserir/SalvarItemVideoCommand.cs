using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemVideoCommand : IRequest<long>
    {
        public SalvarItemVideoCommand(Dominio.Entities.ItemVideo itemVideo)
        {
            ItemVideo = itemVideo;
        }
        public Dominio.Entities.ItemVideo ItemVideo { get; set; }
    }
}

using MediatR;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId
{
    public class ObterItemComAlternativaPorIdQuery : IRequest<ItemConsulta>
    {
        public ObterItemComAlternativaPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}

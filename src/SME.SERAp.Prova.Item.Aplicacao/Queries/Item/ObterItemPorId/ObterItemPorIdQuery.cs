using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterItemPorIdQuery : IRequest<ItemConsulta>
    {
        public ObterItemPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }
}

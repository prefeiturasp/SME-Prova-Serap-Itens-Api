using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.NivelItem.ObterIdNivelItemOrdem
{
    public class ObterNivelItemQuery : IRequest<IEnumerable<Dominio.Entities.NivelItem>>
    {
    }
}

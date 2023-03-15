using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterListaQuantidadeAlternativasQuery : IRequest<IEnumerable<SelectDto>>
    {
        public ObterListaQuantidadeAlternativasQuery()
        {

        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries
{
    public class ObterListaCodigoItensQueryValidator : AbstractValidator<ObterItemPorIdQuery>
    {
        public ObterListaCodigoItensQueryValidator()
        {
        }

    }
}

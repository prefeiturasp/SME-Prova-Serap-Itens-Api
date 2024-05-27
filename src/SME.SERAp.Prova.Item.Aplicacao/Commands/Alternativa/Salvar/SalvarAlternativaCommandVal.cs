using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.Inserir
{
    public class SalvarAlternativaCommandVal : AbstractValidator<SalvarAlternativaCommand>
    {
        public SalvarAlternativaCommandVal()
        {
            RuleFor(c => c.Alternativa.Ordem)
              .NotEmpty()
              .WithMessage("A ordem precisa ser informada");

            RuleFor(c => c.Alternativa.ItemId)
              .GreaterThan(0)
              .WithMessage("O itemId não pode ser 0");
        }
    }
}
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SalvarItemCommandValidator : AbstractValidator<SalvarItemCommand>
    {
        public SalvarItemCommandValidator()
        {
            RuleFor(c => c.Item.AreaconhecimentoId)
              .NotEmpty()
              .WithMessage("A Area de Conhecimento precisa ser informada");

            RuleFor(c => c.Item.DisciplinaId)
              .NotEmpty()
              .WithMessage("A Disciplina precisa ser informada");
        }
    }
}


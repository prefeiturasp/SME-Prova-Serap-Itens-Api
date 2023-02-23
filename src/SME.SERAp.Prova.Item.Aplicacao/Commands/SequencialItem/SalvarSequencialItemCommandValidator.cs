using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.SequencialItem
{
    public class SalvarSequencialItemCommandValidator : AbstractValidator<SalvarSequencialItemCommand>
    {
        public SalvarSequencialItemCommandValidator()
        {
            RuleFor(c => c.SequencialItem.CodigoAreaConhecimento)
              .NotEmpty()
              .WithMessage("O codigo da Area de conhecimento precisa ser informada");

            RuleFor(c => c.SequencialItem.CodigoDisciplina)
              .NotEmpty()
              .WithMessage("O codigo da Disciplina precisa ser informada");

            RuleFor(c => c.SequencialItem.Sequencial)
            .NotEmpty()
            .WithMessage("O sequencial é obrigatório e precisa ser informado");
        }
    }
}
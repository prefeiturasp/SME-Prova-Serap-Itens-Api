using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemAudioCommandValidator : AbstractValidator<SalvarItemAudioCommand>
    {
        public SalvarItemAudioCommandValidator()
        {
            RuleFor(c => c.ItemAudio.ArquivoId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                 .WithMessage("O ArquivoId deve ser informado");


            RuleFor(c => c.ItemAudio.ItemId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("O ItemId deve ser informado.");

        }
    }
}
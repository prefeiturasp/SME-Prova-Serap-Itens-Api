using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarArquivoCommandValidator : AbstractValidator<SalvarArquivoCommand>
    {
        public SalvarArquivoCommandValidator()
        {
            RuleFor(c => c.Arquivo.LegadoId)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                 .WithMessage("O LegadoId deve ser informado");


            RuleFor(c => c.Arquivo.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("O nome deve ser informado.");

            RuleFor(c => c.Arquivo.Caminho)
                .NotNull()
                .NotEmpty()
                .WithMessage("O Caminho deve ser informado.");
        }
    }
}

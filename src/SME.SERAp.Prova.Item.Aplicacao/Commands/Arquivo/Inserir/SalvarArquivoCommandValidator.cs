using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarArquivoCommandValidator : AbstractValidator<SalvarArquivoCommand>
    {
        public SalvarArquivoCommandValidator()
        {
            RuleFor(c => c.Arquivo)
                .NotNull()
                .WithMessage("Os dados do arquivo devem ser informados.")
                .DependentRules(() =>
                {
                    RuleFor(c => c.Arquivo.LegadoId)
                        .GreaterThan(0)
                        .WithMessage("O LegadoId do arquivo deve ser informado");

                    RuleFor(c => c.Arquivo.Nome)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O nome do arquivo deve ser informado.");

                    RuleFor(c => c.Arquivo.Caminho)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O caminho do arquivo deve ser informado.");
                });
        }
    }
}

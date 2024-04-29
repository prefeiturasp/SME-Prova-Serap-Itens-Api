using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommandValidator : AbstractValidator<UploadArquivoCommand>
    {
        public UploadArquivoCommandValidator()
        {
            RuleFor(c => c.Arquivo)
                .NotNull()
                .WithMessage("O arquivo deve ser informado.");

            RuleFor(c => c.Tipo)
                .IsInEnum()
                .WithMessage("Informe um tipo de arquivo válido");
        }
    }
}
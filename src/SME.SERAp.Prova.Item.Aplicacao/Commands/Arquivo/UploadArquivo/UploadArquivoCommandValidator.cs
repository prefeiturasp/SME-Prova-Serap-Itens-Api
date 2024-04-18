using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommandValidator : AbstractValidator<UploadArquivoCommand>
    {
        public UploadArquivoCommandValidator()
        {
            RuleFor(c => c.UploadArquivo)
                .NotNull()
                .WithMessage("Os dados para realizar o upload do arquivo devem ser informados.")
                .DependentRules(() =>
                {
                    RuleFor(c => c.UploadArquivo.ContentLength)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O content length deve ser informado.");

                    RuleFor(c => c.UploadArquivo.ContentType)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O content type deve ser informado.");

                    RuleFor(c => c.UploadArquivo.FileName)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O file name deve ser informado.");

                    RuleFor(c => c.UploadArquivo.InputStream)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("O input stream deve ser informado.");

                    RuleFor(c => c.UploadArquivo.FileType)
                        .IsInEnum()
                        .WithMessage("Um file type válido deve ser informado.");
                });          
        }
    }
}
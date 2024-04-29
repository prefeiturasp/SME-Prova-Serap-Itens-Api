using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemVideoCommandValidator : AbstractValidator<SalvarItemVideoCommand>
    {
        public SalvarItemVideoCommandValidator()
        {
            RuleFor(c => c.ItemVideo)
                .NotNull()
                .WithMessage("Os dados de vídeo devem ser informados.")
                .DependentRules(() =>
                {
                    RuleFor(c => c.ItemVideo.ArquivoId)
                        .GreaterThan(0)
                        .WithMessage("O ArquivoId deve ser informado");
                    
                    RuleFor(c => c.ItemVideo.ItemId)
                        .GreaterThan(0)
                        .WithMessage("O ItemId deve ser informado.");
                });
        }
    }
}

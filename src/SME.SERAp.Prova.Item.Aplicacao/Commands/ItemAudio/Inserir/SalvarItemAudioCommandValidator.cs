using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands
{
    public class SalvarItemAudioCommandValidator : AbstractValidator<SalvarItemAudioCommand>
    {
        public SalvarItemAudioCommandValidator()
        {
            RuleFor(c => c.ItemAudio)
                .NotNull()
                .WithMessage("Os dados de áudio devem ser informados.")
                .DependentRules(() =>
                {
                    RuleFor(c => c.ItemAudio.ArquivoId)
                        .GreaterThan(0)
                        .WithMessage("O ArquivoId deve ser informado");
                    
                    RuleFor(c => c.ItemAudio.ItemId)
                        .GreaterThan(0)
                        .WithMessage("O ItemId deve ser informado.");
                });
        }
    }
}
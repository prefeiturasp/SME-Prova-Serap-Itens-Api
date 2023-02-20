using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterItemPorIdQueryValidator : AbstractValidator<ObterItemPorIdQuery>
    {
        public ObterItemPorIdQueryValidator()
        {
            RuleFor(a => a.Id)
               .NotEmpty()
               .WithMessage("O Id do item é obrigatório.");
        }
    }
}

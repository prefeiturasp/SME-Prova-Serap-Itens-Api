using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterItemComAlternativaPorId
{
    public class ObterItemComAlternativaPorIdQueryValidator : AbstractValidator<ObterItemComAlternativaPorIdQuery>
    {
        public ObterItemComAlternativaPorIdQueryValidator()
        {
            RuleFor(a => a.Id)
               .NotEmpty()
               .WithMessage("O Id do item é obrigatório.");
        }
    }
}

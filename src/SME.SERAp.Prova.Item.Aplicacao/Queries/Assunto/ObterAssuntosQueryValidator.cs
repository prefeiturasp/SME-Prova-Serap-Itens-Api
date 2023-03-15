using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAssuntosQueryValidator : AbstractValidator<ObterAssuntosQuery>
    {
        public ObterAssuntosQueryValidator()
        {
            RuleFor(a => a.DisciplinaId)
               .NotEmpty()
               .WithMessage("DisciplinaId é obrigatório.");
        }
    }
}

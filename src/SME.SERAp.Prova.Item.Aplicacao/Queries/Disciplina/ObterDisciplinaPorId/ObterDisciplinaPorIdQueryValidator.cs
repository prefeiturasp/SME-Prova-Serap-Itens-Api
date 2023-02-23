using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinaPorIdQueryValidator : AbstractValidator<ObterDisciplinaPorIdQuery>
    {
        public ObterDisciplinaPorIdQueryValidator()
        {
            RuleFor(a => a.Id)
               .NotEmpty()
               .WithMessage("O Id da disciplina é obrigatório.");
        }
    }
}

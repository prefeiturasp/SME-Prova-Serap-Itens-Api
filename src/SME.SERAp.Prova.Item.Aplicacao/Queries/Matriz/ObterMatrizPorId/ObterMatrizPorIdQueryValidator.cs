using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorIdQueryValidator : AbstractValidator<ObterMatrizPorIdQuery>
    {
        public ObterMatrizPorIdQueryValidator()
        {
            RuleFor(a => a.MatrizId)
               .NotEmpty()
               .WithMessage("O Id da matriz é obrigatório.");
        }
    }
}

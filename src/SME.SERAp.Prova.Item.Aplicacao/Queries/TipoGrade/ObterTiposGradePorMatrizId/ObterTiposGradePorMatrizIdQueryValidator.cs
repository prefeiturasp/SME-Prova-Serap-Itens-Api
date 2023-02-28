using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTiposGradePorMatrizIdQueryValidator : AbstractValidator<ObterTiposGradePorMatrizIdQuery>
    {
        public ObterTiposGradePorMatrizIdQueryValidator()
        {
            RuleFor(c => c.MatrizId)
                .GreaterThan(0)
                .WithMessage("A matriz deve ser informada para obter os tipos de grade.");            
        }
    }
}
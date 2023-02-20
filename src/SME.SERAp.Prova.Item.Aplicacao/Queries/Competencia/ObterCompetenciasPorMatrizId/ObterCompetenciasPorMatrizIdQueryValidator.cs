using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterCompetenciasPorMatrizIdQueryValidator : AbstractValidator<ObterCompetenciasPorMatrizIdQuery>
    {
        public ObterCompetenciasPorMatrizIdQueryValidator()
        {
            RuleFor(c => c.MatrizId)
                .GreaterThan(0)
                .WithMessage("A matriz deve ser informada para obter as competências.");
        }
    }
}
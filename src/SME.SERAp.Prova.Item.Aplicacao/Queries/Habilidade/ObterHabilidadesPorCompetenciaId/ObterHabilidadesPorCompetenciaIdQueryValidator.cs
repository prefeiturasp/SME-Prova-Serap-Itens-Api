using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaIdQueryValidator : AbstractValidator<ObterHabilidadesPorCompetenciaIdQuery>
    {
        public ObterHabilidadesPorCompetenciaIdQueryValidator()
        {
            RuleFor(c => c.CompetenciaId)
                .GreaterThan(0)
                .WithMessage("A competência deve ser informada para obter as habilidades.");            
        }
    }
}
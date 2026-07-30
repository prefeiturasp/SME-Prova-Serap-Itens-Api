using FluentValidation;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class SalvarItemCommandValidator : AbstractValidator<SalvarItemCommand>
    {
        public SalvarItemCommandValidator()
        {
            RuleFor(c => c.Item.AreaconhecimentoId)
                .GreaterThan(0)
                .WithMessage("A Area de Conhecimento precisa ser informada.");

            RuleFor(c => c.Item.DisciplinaId)
                .GreaterThan(0)
                .WithMessage("A Disciplina precisa ser informada.");

            RuleFor(c => c.Item.Observacao)
                .MaximumLength(100)
                .WithMessage("A observação pode ter no máximo 100 caracteres.");

            RuleFor(c => c.Item.SentencaDescritora)
                .MaximumLength(100)
                .WithMessage("A Sentença Descritora pode ter no máximo 100 caracteres.");

            When(c => c.Item.Situacao != SituacaoItem.Rascunho, () =>
            {
                RuleFor(c => c.Item.MatrizId)
                    .GreaterThan(0)
                    .WithMessage("A Matriz precisa ser informada.");

                RuleFor(c => c.Item.CompetenciaId)
                    .NotNull()
                    .WithMessage("A Competencia precisa ser informada.")
                    .GreaterThan(0)
                    .WithMessage("CompetenciaId tem que ser maior que zero.");

                RuleFor(c => c.Item.HabilidadeId)
                    .NotNull()
                    .WithMessage("A Habilidade precisa ser informada.")
                    .GreaterThan(0)
                    .WithMessage("HabilidadeId tem que ser maior que zero.");

                RuleFor(c => c.Item.AnoMatrizId)
                    .NotNull()
                    .WithMessage("O AnoMatriz precisa ser informado.")
                    .GreaterThan(0)
                    .WithMessage("AnoMatrizId tem que ser maior que zero.");

                RuleFor(c => c.Item.DificuldadeSugeridaId)
                    .NotNull()
                    .WithMessage("A Dificuldade Sugerida precisa ser informada.")
                    .GreaterThan(0)
                    .WithMessage("DificuldadeSugeridaId tem que ser maior que zero.");

                RuleFor(c => c.Item.QuantidadeAlternativasId)
                    .GreaterThan(0)
                    .WithMessage("A Quantidade de Alternativas precisa ser informada.");

                RuleFor(c => c.Item.PalavrasChave)
                    .NotEmpty()
                    .WithMessage("É necessário informar pelo menos uma palavra chave.");

                RuleFor(c => c.Item.Enunciado)
                    .NotEmpty()
                    .WithMessage("É necessário informar o enunciado do Item.");
            });
        }
    }
}
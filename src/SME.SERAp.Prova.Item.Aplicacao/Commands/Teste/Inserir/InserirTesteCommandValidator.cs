using FluentValidation;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Teste.Inserir
{
    public class InserirTesteCommandValidator : AbstractValidator<InserirTesteCommand>
    {
        public InserirTesteCommandValidator()
        {
            RuleFor(c => c.Descricao)
              .NotEmpty()
              .WithMessage("A Descrição precisa ser informado");
        }
    }
}

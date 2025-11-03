using SME.SERAp.Prova.Item.Aplicacao.Commands.SequencialItem;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.SequencialItem
{
    public class SalvarSequencialItemCommandValidatorTeste
    {
        private readonly SalvarSequencialItemCommandValidator validator;

        public SalvarSequencialItemCommandValidatorTeste()
        {
            validator = new SalvarSequencialItemCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_CodigoAreaConhecimento_For_Vazio()
        {
            var command = new SalvarSequencialItemCommand(new Dominio.Entities.SequencialItem
            {
                CodigoAreaConhecimento = 0,
                CodigoDisciplina = 123,
                Sequencial = 1
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "SequencialItem.CodigoAreaConhecimento" &&
                e.ErrorMessage == "O codigo da Area de conhecimento precisa ser informada");
        }

        [Fact]
        public void Deve_Falhar_Quando_CodigoDisciplina_For_Vazio()
        {
            var command = new SalvarSequencialItemCommand(new Dominio.Entities.SequencialItem
            {
                CodigoAreaConhecimento = 1,
                CodigoDisciplina = 0,
                Sequencial = 1
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "SequencialItem.CodigoDisciplina" &&
                e.ErrorMessage == "O codigo da Disciplina precisa ser informada");
        }

        [Fact]
        public void Deve_Falhar_Quando_Sequencial_For_Vazio()
        {
            var command = new SalvarSequencialItemCommand(new Dominio.Entities.SequencialItem
            {
                CodigoAreaConhecimento = 1,
                CodigoDisciplina = 123,
                Sequencial = 0
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "SequencialItem.Sequencial" &&
                e.ErrorMessage == "O sequencial é obrigatório e precisa ser informado");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarSequencialItemCommand(new Dominio.Entities.SequencialItem
            {
                CodigoAreaConhecimento = 1,
                CodigoDisciplina = 123,
                Sequencial = 1
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

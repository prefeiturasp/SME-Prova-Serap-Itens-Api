namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Item
{
    public class SalvarItemCommandValidatorTeste
    {
        private readonly SalvarItemCommandValidator validator;

        public SalvarItemCommandValidatorTeste()
        {
            validator = new SalvarItemCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_AreaConhecimentoId_For_Vazio()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 0,
                DisciplinaId = 1
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Item.AreaconhecimentoId" &&
                                                e.ErrorMessage == "A Area de Conhecimento precisa ser informada");
        }

        [Fact]
        public void Deve_Falhar_Quando_DisciplinaId_For_Vazio()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 1,
                DisciplinaId = 0
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Item.DisciplinaId" &&
                                                e.ErrorMessage == "A Disciplina precisa ser informada");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarItemCommand(new Dominio.Entities.Item
            {
                AreaconhecimentoId = 2,
                DisciplinaId = 3
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Disciplina
{
    public class ObterDisciplinaPorIdQueryValidatorTeste
    {
        private readonly ObterDisciplinaPorIdQueryValidator validator;

        public ObterDisciplinaPorIdQueryValidatorTeste()
        {
            validator = new ObterDisciplinaPorIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_Id_For_Informado()
        {
            var query = new ObterDisciplinaPorIdQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Deve_Ser_Valido_Para_Diferentes_Ids_Validos(long id)
        {
            var query = new ObterDisciplinaPorIdQuery(id);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_Id_For_Zero()
        {
            var query = new ObterDisciplinaPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("Id", resultado.Errors[0].PropertyName);
            Assert.Equal("O Id da disciplina é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_Id_For_Invalido()
        {
            var query = new ObterDisciplinaPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "O Id da disciplina é obrigatório.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_Id_Corretamente()
        {
            var query = new ObterDisciplinaPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("Id", erro.PropertyName));
        }

        [Fact]
        public void Deve_Ter_Apenas_Um_Erro_Quando_Id_For_Invalido()
        {
            var query = new ObterDisciplinaPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-999)]
        public void Deve_Ser_Valido_Para_Ids_Negativos(long id)
        {
            var query = new ObterDisciplinaPorIdQuery(id);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }
    }
}

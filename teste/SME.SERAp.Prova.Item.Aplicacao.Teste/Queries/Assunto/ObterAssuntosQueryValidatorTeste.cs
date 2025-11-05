namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Assunto
{
    public class ObterAssuntosQueryValidatorTeste
    {
        private readonly ObterAssuntosQueryValidator validator;

        public ObterAssuntosQueryValidatorTeste()
        {
            validator = new ObterAssuntosQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_DisciplinaId_For_Informado()
        {
            var query = new ObterAssuntosQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Deve_Ser_Valido_Para_Diferentes_DisciplinaIds_Validos(long disciplinaId)
        {
            var query = new ObterAssuntosQuery(disciplinaId);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_DisciplinaId_For_Zero()
        {
            var query = new ObterAssuntosQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("DisciplinaId", resultado.Errors[0].PropertyName);
            Assert.Equal("DisciplinaId é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_DisciplinaId_For_Invalido()
        {
            var query = new ObterAssuntosQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "DisciplinaId é obrigatório.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_DisciplinaId_Corretamente()
        {
            var query = new ObterAssuntosQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("DisciplinaId", erro.PropertyName));
        }
    }
}

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterItemPorIdQueryValidatorTeste
    {
        private readonly ObterItemPorIdQueryValidator validator;

        public ObterItemPorIdQueryValidatorTeste()
        {
            validator = new ObterItemPorIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_Id_For_Informado()
        {
            var query = new ObterItemPorIdQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(500)]
        [InlineData(99999)]
        public void Deve_Ser_Valido_Para_Diferentes_Ids_Validos(long id)
        {
            var query = new ObterItemPorIdQuery(id);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_Id_For_Zero()
        {
            var query = new ObterItemPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("Id", resultado.Errors[0].PropertyName);
            Assert.Equal("O Id do item é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_Id_For_Invalido()
        {
            var query = new ObterItemPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "O Id do item é obrigatório.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_Id_Corretamente()
        {
            var query = new ObterItemPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("Id", erro.PropertyName));
        }
    }
}

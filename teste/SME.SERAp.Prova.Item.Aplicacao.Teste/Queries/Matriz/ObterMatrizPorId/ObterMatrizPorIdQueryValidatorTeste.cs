namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Matriz
{
    public class ObterMatrizPorIdQueryValidatorTeste
    {
        private readonly ObterMatrizPorIdQueryValidator validator;

        public ObterMatrizPorIdQueryValidatorTeste()
        {
            validator = new ObterMatrizPorIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_MatrizId_For_Informado()
        {
            var query = new ObterMatrizPorIdQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Deve_Ser_Valido_Para_Diferentes_MatrizIds_Validos(long matrizId)
        {
            var query = new ObterMatrizPorIdQuery(matrizId);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_MatrizId_For_Zero()
        {
            var query = new ObterMatrizPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("MatrizId", resultado.Errors[0].PropertyName);
            Assert.Equal("O Id da matriz é obrigatório.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterMatrizPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "O Id da matriz é obrigatório.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_MatrizId_Corretamente()
        {
            var query = new ObterMatrizPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("MatrizId", erro.PropertyName));
        }

        [Fact]
        public void Deve_Ter_Apenas_Um_Erro_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterMatrizPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }
    }
}

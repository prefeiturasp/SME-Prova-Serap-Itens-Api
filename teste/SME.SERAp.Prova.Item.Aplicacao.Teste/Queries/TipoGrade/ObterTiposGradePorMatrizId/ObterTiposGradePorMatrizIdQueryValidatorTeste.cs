namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.TipoGrade
{
    public class ObterTiposGradePorMatrizIdQueryValidatorTeste
    {
        private readonly ObterTiposGradePorMatrizIdQueryValidator validator;

        public ObterTiposGradePorMatrizIdQueryValidatorTeste()
        {
            validator = new ObterTiposGradePorMatrizIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_MatrizId_For_Maior_Que_Zero()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(1);
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
            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_MatrizId_For_Zero()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("MatrizId", resultado.Errors[0].PropertyName);
            Assert.Equal("A matriz deve ser informada para obter os tipos de grade.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_MatrizId_For_Negativo()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(-1);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("MatrizId", resultado.Errors[0].PropertyName);
            Assert.Equal("A matriz deve ser informada para obter os tipos de grade.", resultado.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-999)]
        public void Deve_Ser_Invalido_Para_MatrizIds_Menores_Ou_Iguais_A_Zero(long matrizId)
        {
            var query = new ObterTiposGradePorMatrizIdQuery(matrizId);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "A matriz deve ser informada para obter os tipos de grade.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_MatrizId_Corretamente()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(-5);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("MatrizId", erro.PropertyName));
        }

        [Fact]
        public void Deve_Ter_Apenas_Um_Erro_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterTiposGradePorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }
    }
}

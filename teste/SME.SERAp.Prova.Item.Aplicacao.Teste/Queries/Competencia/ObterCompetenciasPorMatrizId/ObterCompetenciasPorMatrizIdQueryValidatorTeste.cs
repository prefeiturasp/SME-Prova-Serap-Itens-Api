namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Competencia
{
    public class ObterCompetenciasPorMatrizIdQueryValidatorTeste
    {
        private readonly ObterCompetenciasPorMatrizIdQueryValidator validator;

        public ObterCompetenciasPorMatrizIdQueryValidatorTeste()
        {
            validator = new ObterCompetenciasPorMatrizIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_MatrizId_For_Maior_Que_Zero()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(1);
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
            var query = new ObterCompetenciasPorMatrizIdQuery(matrizId);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_MatrizId_For_Zero()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("MatrizId", resultado.Errors[0].PropertyName);
            Assert.Equal("A matriz deve ser informada para obter as competências.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_MatrizId_For_Negativo()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(-1);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("MatrizId", resultado.Errors[0].PropertyName);
            Assert.Equal("A matriz deve ser informada para obter as competências.", resultado.Errors[0].ErrorMessage);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-999)]
        public void Deve_Ser_Invalido_Para_MatrizIds_Menores_Ou_Iguais_A_Zero(long matrizId)
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(matrizId);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "A matriz deve ser informada para obter as competências.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_MatrizId_Corretamente()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(-5);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("MatrizId", erro.PropertyName));
        }

        [Fact]
        public void Deve_Ter_Apenas_Um_Erro_Quando_MatrizId_For_Invalido()
        {
            var query = new ObterCompetenciasPorMatrizIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }
    }
}

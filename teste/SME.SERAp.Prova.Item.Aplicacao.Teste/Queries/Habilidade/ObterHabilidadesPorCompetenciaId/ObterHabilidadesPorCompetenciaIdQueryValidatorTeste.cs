namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Habilidade
{
    public class ObterHabilidadesPorCompetenciaIdQueryValidatorTeste
    {
        private readonly ObterHabilidadesPorCompetenciaIdQueryValidator validator;

        public ObterHabilidadesPorCompetenciaIdQueryValidatorTeste()
        {
            validator = new ObterHabilidadesPorCompetenciaIdQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_CompetenciaId_For_Informado()
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        [InlineData(999999)]
        public void Deve_Ser_Valido_Para_Diferentes_CompetenciaIds_Validos(long competenciaId)
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(competenciaId);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Invalido_Quando_CompetenciaId_For_Zero()
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("CompetenciaId", resultado.Errors[0].PropertyName);
            Assert.Equal("A competência deve ser informada para obter as habilidades.", resultado.Errors[0].ErrorMessage);
        }

        [Fact]
        public void Deve_Conter_Mensagem_Especifica_Quando_CompetenciaId_For_Invalido()
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.Contains(resultado.Errors, e => e.ErrorMessage == "A competência deve ser informada para obter as habilidades.");
        }

        [Fact]
        public void Deve_Validar_Propriedade_CompetenciaId_Corretamente()
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.All(resultado.Errors, erro => Assert.Equal("CompetenciaId", erro.PropertyName));
        }

        [Fact]
        public void Deve_Ter_Apenas_Um_Erro_Quando_CompetenciaId_For_Invalido()
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-999)]
        public void Deve_Ser_Invalido_Para_CompetenciaIds_Negativos(long competenciaId)
        {
            var query = new ObterHabilidadesPorCompetenciaIdQuery(competenciaId);
            var resultado = validator.Validate(query);

            Assert.False(resultado.IsValid);
            Assert.Single(resultado.Errors);
            Assert.Equal("A competência deve ser informada para obter as habilidades.", resultado.Errors[0].ErrorMessage);
        }
    }
}

using SME.SERAp.Prova.Item.Aplicacao.Queries;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterListaCodigoItensQueryValidatorTeste
    {
        private readonly ObterListaCodigoItensQueryValidator validator;

        public ObterListaCodigoItensQueryValidatorTeste()
        {
            validator = new ObterListaCodigoItensQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_Query_For_Valida()
        {
            var query = new ObterItemPorIdQuery(1);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Valido_Mesmo_Sem_Regras_Definidas()
        {
            var query = new ObterItemPorIdQuery(0);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }
    }
}

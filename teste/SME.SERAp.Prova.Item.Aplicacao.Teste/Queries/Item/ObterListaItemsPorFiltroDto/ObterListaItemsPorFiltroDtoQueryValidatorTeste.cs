using SME.SERAp.Prova.Item.Aplicacao.Queries;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterListaItemsPorFiltroDtoQueryValidatorTeste
    {
        private readonly ObterListaItemsPorFiltroDtoQueryValidator validator;

        public ObterListaItemsPorFiltroDtoQueryValidatorTeste()
        {
            validator = new ObterListaItemsPorFiltroDtoQueryValidator();
        }

        [Fact]
        public void Deve_Ser_Valido_Quando_Query_For_Valida()
        {
            var filtro = new FiltroItemsDto { CodigoItem = "Item Teste" };
            var query = new ObterListaItemsPorFiltroDtoQuery(filtro);

            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }

        [Fact]
        public void Deve_Ser_Valido_Mesmo_Sem_Regras_Definidas()
        {
            var query = new ObterListaItemsPorFiltroDtoQuery(null);
            var resultado = validator.Validate(query);

            Assert.True(resultado.IsValid);
            Assert.Empty(resultado.Errors);
        }
    }
}

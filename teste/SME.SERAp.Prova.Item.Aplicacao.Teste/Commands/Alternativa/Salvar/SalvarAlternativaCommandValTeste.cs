using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.Inserir;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Alternativa
{
    public class SalvarAlternativaCommandValTeste
    {
        private readonly SalvarAlternativaCommandVal validator;

        public SalvarAlternativaCommandValTeste()
        {
            validator = new SalvarAlternativaCommandVal();
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarAlternativaCommand(new Dominio.Entities.Alternativa
            {
                Ordem = 1,
                ItemId = 10
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Deve_Falhar_Quando_Ordem_For_Vazia()
        {
            var command = new SalvarAlternativaCommand(new Dominio.Entities.Alternativa
            {
                Ordem = null,
                ItemId = 10
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Alternativa.Ordem" &&
                                                e.ErrorMessage == "A ordem precisa ser informada");
        }

        [Fact]
        public void Deve_Falhar_Quando_ItemId_For_Zero()
        {
            var command = new SalvarAlternativaCommand(new Dominio.Entities.Alternativa
            {
                Ordem = 1,
                ItemId = 0
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Alternativa.ItemId" &&
                                                e.ErrorMessage == "O itemId não pode ser 0");
        }
    }
}

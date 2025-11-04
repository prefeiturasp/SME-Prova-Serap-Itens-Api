using SME.SERAp.Prova.Item.Aplicacao.Commands;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.ItemVideo
{
    public class SalvarItemVideoCommandValidatorTeste
    {
        private readonly SalvarItemVideoCommandValidator validator;

        public SalvarItemVideoCommandValidatorTeste()
        {
            validator = new SalvarItemVideoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_ItemVideo_For_Nulo()
        {
            var command = new SalvarItemVideoCommand(null);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "ItemVideo" &&
                e.ErrorMessage == "Os dados de vídeo devem ser informados.");
        }

        [Fact]
        public void Deve_Falhar_Quando_ArquivoId_For_Menor_Ou_Igual_A_Zero()
        {
            var command = new SalvarItemVideoCommand(new Dominio.Entities.ItemVideo
            {
                ArquivoId = 0,
                ItemId = 1
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "ItemVideo.ArquivoId" &&
                e.ErrorMessage == "O ArquivoId deve ser informado");
        }

        [Fact]
        public void Deve_Falhar_Quando_ItemId_For_Menor_Ou_Igual_A_Zero()
        {
            var command = new SalvarItemVideoCommand(new Dominio.Entities.ItemVideo
            {
                ArquivoId = 1,
                ItemId = 0
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e =>
                e.PropertyName == "ItemVideo.ItemId" &&
                e.ErrorMessage == "O ItemId deve ser informado.");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarItemVideoCommand(new Dominio.Entities.ItemVideo
            {
                ArquivoId = 1,
                ItemId = 2
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

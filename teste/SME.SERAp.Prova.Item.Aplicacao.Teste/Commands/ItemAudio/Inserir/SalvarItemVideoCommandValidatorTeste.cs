using SME.SERAp.Prova.Item.Aplicacao.Commands;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.ItemAudio
{
    public class SalvarItemVideoCommandValidatorTeste
    {
        private readonly SalvarItemAudioCommandValidator validator;

        public SalvarItemVideoCommandValidatorTeste()
        {
            validator = new SalvarItemAudioCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_ItemAudio_For_Nulo()
        {
            var command = new SalvarItemAudioCommand(null);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ItemAudio" &&
                                                e.ErrorMessage == "Os dados de áudio devem ser informados.");
        }

        [Fact]
        public void Deve_Falhar_Quando_ArquivoId_For_Menor_Ou_Igual_A_Zero()
        {
            var command = new SalvarItemAudioCommand(new Dominio.Entities.ItemAudio
            {
                ArquivoId = 0,
                ItemId = 1
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ItemAudio.ArquivoId" &&
                                                e.ErrorMessage == "O ArquivoId deve ser informado");
        }

        [Fact]
        public void Deve_Falhar_Quando_ItemId_For_Menor_Ou_Igual_A_Zero()
        {
            var command = new SalvarItemAudioCommand(new Dominio.Entities.ItemAudio
            {
                ArquivoId = 1,
                ItemId = 0
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "ItemAudio.ItemId" &&
                                                e.ErrorMessage == "O ItemId deve ser informado.");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarItemAudioCommand(new Dominio.Entities.ItemAudio
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

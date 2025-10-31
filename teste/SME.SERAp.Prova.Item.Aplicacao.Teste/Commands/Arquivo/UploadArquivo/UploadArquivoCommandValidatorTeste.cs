using Microsoft.AspNetCore.Http;
using Moq;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Arquivo
{
    public class UploadArquivoCommandValidatorTeste
    {
        private readonly UploadArquivoCommandValidator validator;

        public UploadArquivoCommandValidatorTeste()
        {
            validator = new UploadArquivoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_Arquivo_For_Nulo()
        {
            var command = new UploadArquivoCommand(null, TipoArquivo.File);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Arquivo" &&
                                                e.ErrorMessage == "O arquivo deve ser informado.");
        }

        [Fact]
        public void Deve_Falhar_Quando_Tipo_For_Invalido()
        {
            var mockFile = new Mock<IFormFile>().Object;
            var command = new UploadArquivoCommand(mockFile, (TipoArquivo)999);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Tipo" &&
                                                e.ErrorMessage == "Informe um tipo de arquivo válido");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var mockFile = new Mock<IFormFile>().Object;
            var command = new UploadArquivoCommand(mockFile, TipoArquivo.File);

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

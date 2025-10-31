using SME.SERAp.Prova.Item.Aplicacao.Commands;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Arquivo
{
    public class SalvarArquivoCommandValidatorTeste
    {
        private readonly SalvarArquivoCommandValidator validator;

        public SalvarArquivoCommandValidatorTeste()
        {
            validator = new SalvarArquivoCommandValidator();
        }

        [Fact]
        public void Deve_Falhar_Quando_Arquivo_For_Nulo()
        {
            var command = new SalvarArquivoCommand(null);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Arquivo" &&
                                                e.ErrorMessage == "Os dados do arquivo devem ser informados.");
        }

        [Fact]
        public void Deve_Falhar_Quando_LegadoId_For_Menor_Ou_Igual_A_Zero()
        {
            var command = new SalvarArquivoCommand(new Dominio.Entities.Arquivo
            {
                LegadoId = 0,
                Nome = "arquivo.pdf",
                Caminho = "/uploads/arquivo.pdf"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Arquivo.LegadoId" &&
                                                e.ErrorMessage == "O LegadoId do arquivo deve ser informado");
        }

        [Fact]
        public void Deve_Falhar_Quando_Nome_For_Nulo_Ou_Vazio()
        {
            var command = new SalvarArquivoCommand(new Dominio.Entities.Arquivo
            {
                LegadoId = 1,
                Nome = "",
                Caminho = "/uploads/arquivo.pdf"
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Arquivo.Nome" &&
                                                e.ErrorMessage == "O nome do arquivo deve ser informado.");
        }

        [Fact]
        public void Deve_Falhar_Quando_Caminho_For_Nulo_Ou_Vazio()
        {
            var command = new SalvarArquivoCommand(new Dominio.Entities.Arquivo
            {
                LegadoId = 1,
                Nome = "arquivo.pdf",
                Caminho = ""
            });

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Arquivo.Caminho" &&
                                                e.ErrorMessage == "O caminho do arquivo deve ser informado.");
        }

        [Fact]
        public void Deve_Passar_Quando_Dados_Forem_Validos()
        {
            var command = new SalvarArquivoCommand(new Dominio.Entities.Arquivo
            {
                LegadoId = 10,
                Nome = "arquivo.pdf",
                Caminho = "/uploads/arquivo.pdf"
            });

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}

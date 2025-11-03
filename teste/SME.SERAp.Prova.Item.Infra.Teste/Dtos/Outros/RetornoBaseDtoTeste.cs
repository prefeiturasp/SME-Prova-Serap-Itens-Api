using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using SME.SERAp.Prova.Item.Infra.Dtos;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Outros
{
    public class RetornoBaseDtoTeste
    {
        [Fact]
        public void Deve_Criar_RetornoBaseDto_Com_Construtor_Padrao()
        {
            var dto = new RetornoBaseDto();

            Assert.NotNull(dto.Mensagens);
            Assert.Empty(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Criar_RetornoBaseDto_Com_Mensagem_Unica()
        {
            var mensagem = "Erro ao processar requisição";

            var dto = new RetornoBaseDto(mensagem);

            Assert.NotNull(dto.Mensagens);
            Assert.Single(dto.Mensagens);
            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Criar_RetornoBaseDto_Com_ValidationFailures()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", "Erro no campo 1"),
                new ValidationFailure("Campo2", "Erro no campo 2")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.NotNull(dto.Mensagens);
            Assert.Equal(2, dto.Mensagens.Count);
            Assert.Contains("Erro no campo 1", dto.Mensagens);
            Assert.Contains("Erro no campo 2", dto.Mensagens);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Criar_RetornoBaseDto_Com_ValidationFailures_Vazia()
        {
            var validationFailures = new List<ValidationFailure>();

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Null(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Criar_RetornoBaseDto_Com_ValidationFailures_Null()
        {
            IEnumerable<ValidationFailure> validationFailures = null;

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Null(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Retornar_ExistemErros_True_Quando_Tem_Mensagens()
        {
            var dto = new RetornoBaseDto("Erro de validação");

            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Retornar_ExistemErros_False_Quando_Nao_Tem_Mensagens()
        {
            var dto = new RetornoBaseDto();

            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Retornar_ExistemErros_False_Quando_Mensagens_Null()
        {
            var dto = new RetornoBaseDto
            {
                Mensagens = null
            };

            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagem_Vazia()
        {
            var mensagem = string.Empty;

            var dto = new RetornoBaseDto(mensagem);

            Assert.Single(dto.Mensagens);
            Assert.Equal(string.Empty, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagem_Nula()
        {
            string mensagem = null;

            var dto = new RetornoBaseDto(mensagem);

            Assert.Single(dto.Mensagens);
            Assert.Null(dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagem_Longa()
        {
            var mensagemLonga = new string('A', 5000);

            var dto = new RetornoBaseDto(mensagemLonga);

            Assert.Single(dto.Mensagens);
            Assert.Equal(5000, dto.Mensagens[0].Length);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var mensagens = new List<string> { "Erro 1", "Erro 2", "Erro 3" };

            var dto = new RetornoBaseDto
            {
                Mensagens = mensagens
            };

            Assert.Equal(3, dto.Mensagens.Count);
            Assert.Equal(mensagens, dto.Mensagens);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Mensagens_Apos_Criacao()
        {
            var dto = new RetornoBaseDto();

            dto.Mensagens.Add("Nova mensagem");

            Assert.Single(dto.Mensagens);
            Assert.Equal("Nova mensagem", dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Extrair_ErrorMessage_De_ValidationFailures()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Propriedade1", "Mensagem de erro 1"),
                new ValidationFailure("Propriedade2", "Mensagem de erro 2"),
                new ValidationFailure("Propriedade3", "Mensagem de erro 3")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal(3, dto.Mensagens.Count);
            Assert.Equal("Mensagem de erro 1", dto.Mensagens[0]);
            Assert.Equal("Mensagem de erro 2", dto.Mensagens[1]);
            Assert.Equal("Mensagem de erro 3", dto.Mensagens[2]);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailures_Com_Mensagens_Vazias()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", string.Empty),
                new ValidationFailure("Campo2", string.Empty)
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal(2, dto.Mensagens.Count);
            Assert.All(dto.Mensagens, m => Assert.Equal(string.Empty, m));
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailures_Com_Mensagens_Nulas()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", null),
                new ValidationFailure("Campo2", null)
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal(2, dto.Mensagens.Count);
            Assert.All(dto.Mensagens, m => Assert.Null(m));
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Multiplas_ValidationFailures()
        {
            var validationFailures = new List<ValidationFailure>();
            for (int i = 1; i <= 10; i++)
            {
                validationFailures.Add(new ValidationFailure($"Campo{i}", $"Erro no campo {i}"));
            }

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal(10, dto.Mensagens.Count);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailure_Com_AttemptedValue()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", "Valor inválido", "valorTentado")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Single(dto.Mensagens);
            Assert.Equal("Valor inválido", dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagens_Com_Caracteres_Especiais()
        {
            var mensagem = "Erro: @#$%&*()_+-=[]{}|;':\"<>,.?/\\~`";

            var dto = new RetornoBaseDto(mensagem);

            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagens_Com_Quebras_De_Linha()
        {
            var mensagem = "Erro\nna\nlinha\n1";

            var dto = new RetornoBaseDto(mensagem);

            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagens_Com_Tabs()
        {
            var mensagem = "Erro\tcom\ttabs";

            var dto = new RetornoBaseDto(mensagem);

            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Mensagens_Unicode()
        {
            var mensagem = "Erro: çãõáéíóú 🔐🔑";

            var dto = new RetornoBaseDto(mensagem);

            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Permitir_Limpar_Mensagens()
        {
            var dto = new RetornoBaseDto("Erro inicial");

            dto.Mensagens.Clear();

            Assert.Empty(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Permitir_Adicionar_Multiplas_Mensagens()
        {
            var dto = new RetornoBaseDto();

            dto.Mensagens.Add("Erro 1");
            dto.Mensagens.Add("Erro 2");
            dto.Mensagens.Add("Erro 3");

            Assert.Equal(3, dto.Mensagens.Count);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Permitir_Remover_Mensagens()
        {
            var dto = new RetornoBaseDto("Erro a ser removido");

            dto.Mensagens.RemoveAt(0);

            Assert.Empty(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Manter_Ordem_Das_Mensagens_De_ValidationFailures()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", "Primeira mensagem"),
                new ValidationFailure("Campo2", "Segunda mensagem"),
                new ValidationFailure("Campo3", "Terceira mensagem")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal("Primeira mensagem", dto.Mensagens[0]);
            Assert.Equal("Segunda mensagem", dto.Mensagens[1]);
            Assert.Equal("Terceira mensagem", dto.Mensagens[2]);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailures_Com_Mesma_Mensagem()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Campo1", "Mesma mensagem"),
                new ValidationFailure("Campo2", "Mesma mensagem"),
                new ValidationFailure("Campo3", "Mesma mensagem")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Equal(3, dto.Mensagens.Count);
            Assert.All(dto.Mensagens, m => Assert.Equal("Mesma mensagem", m));
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Lista_Mensagens_Vazia_Por_Atribuicao()
        {
            var dto = new RetornoBaseDto("Mensagem inicial")
            {
                Mensagens = new List<string>()
            };

            Assert.Empty(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_Lista_Mensagens_Null_Por_Atribuicao()
        {
            var dto = new RetornoBaseDto("Mensagem inicial")
            {
                Mensagens = null
            };

            Assert.Null(dto.Mensagens);
            Assert.False(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Independentes()
        {
            var dto1 = new RetornoBaseDto("Erro 1");
            var dto2 = new RetornoBaseDto("Erro 2");

            dto1.Mensagens.Add("Erro adicional 1");

            Assert.Equal(2, dto1.Mensagens.Count);
            Assert.Single(dto2.Mensagens);
            Assert.Contains("Erro 1", dto1.Mensagens);
            Assert.Contains("Erro 2", dto2.Mensagens);
        }

        [Fact]
        public void Deve_Aceitar_Mensagens_Com_Espacos_No_Inicio_E_Fim()
        {
            var mensagem = "  Mensagem com espaços  ";

            var dto = new RetornoBaseDto(mensagem);

            Assert.Equal(mensagem, dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailure_Sem_PropertyName()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure(null, "Erro sem propriedade")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Single(dto.Mensagens);
            Assert.Equal("Erro sem propriedade", dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }

        [Fact]
        public void Deve_Aceitar_ValidationFailure_Com_PropertyName_Vazio()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure(string.Empty, "Erro com propriedade vazia")
            };

            var dto = new RetornoBaseDto(validationFailures);

            Assert.Single(dto.Mensagens);
            Assert.Equal("Erro com propriedade vazia", dto.Mensagens[0]);
            Assert.True(dto.ExistemErros);
        }
    }
}
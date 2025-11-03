using SME.SERAp.Prova.Item.Infra.Dtos;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Alternativa
{
    public class QuantidadeAlternativasDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var valor = 1L;
            var descricao = "Quatro alternativas";
            var quantidade = 4;

            var dto = new QuantidadeAlternativasDto
            {
                Valor = valor,
                Descricao = descricao,
                Quantidade = quantidade
            };

            Assert.Equal(valor, dto.Valor);
            Assert.Equal(descricao, dto.Descricao);
            Assert.Equal(quantidade, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Duas_Alternativas()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 1,
                Descricao = "Duas alternativas",
                Quantidade = 2
            };

            Assert.Equal(1, dto.Valor);
            Assert.Equal("Duas alternativas", dto.Descricao);
            Assert.Equal(2, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Tres_Alternativas()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 2,
                Descricao = "Três alternativas",
                Quantidade = 3
            };

            Assert.Equal(2, dto.Valor);
            Assert.Equal("Três alternativas", dto.Descricao);
            Assert.Equal(3, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Quatro_Alternativas()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 3,
                Descricao = "Quatro alternativas",
                Quantidade = 4
            };

            Assert.Equal(3, dto.Valor);
            Assert.Equal("Quatro alternativas", dto.Descricao);
            Assert.Equal(4, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Cinco_Alternativas()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 4,
                Descricao = "Cinco alternativas",
                Quantidade = 5
            };

            Assert.Equal(4, dto.Valor);
            Assert.Equal("Cinco alternativas", dto.Descricao);
            Assert.Equal(5, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Descricao_Nula()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 1,
                Descricao = null,
                Quantidade = 4
            };

            Assert.Equal(1, dto.Valor);
            Assert.Null(dto.Descricao);
            Assert.Equal(4, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Descricao_Vazia()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 2,
                Descricao = string.Empty,
                Quantidade = 3
            };

            Assert.Equal(2, dto.Valor);
            Assert.Equal(string.Empty, dto.Descricao);
            Assert.Equal(3, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Valor_Zero()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 0,
                Descricao = "Zero alternativas",
                Quantidade = 0
            };

            Assert.Equal(0, dto.Valor);
            Assert.Equal("Zero alternativas", dto.Descricao);
            Assert.Equal(0, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Vazio()
        {
            var dto = new QuantidadeAlternativasDto();

            Assert.Equal(0, dto.Valor);
            Assert.Null(dto.Descricao);
            Assert.Equal(0, dto.Quantidade);
        }

        [Fact]
        public void Deve_Permitir_Modificacao_Das_Propriedades()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 1,
                Descricao = "Duas alternativas",
                Quantidade = 2
            };

            Assert.Equal(1, dto.Valor);
            Assert.Equal("Duas alternativas", dto.Descricao);
            Assert.Equal(2, dto.Quantidade);

            dto.Valor = 3;
            dto.Descricao = "Quatro alternativas";
            dto.Quantidade = 4;

            Assert.Equal(3, dto.Valor);
            Assert.Equal("Quatro alternativas", dto.Descricao);
            Assert.Equal(4, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Valor_Grande()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = long.MaxValue,
                Descricao = "Valor máximo",
                Quantidade = int.MaxValue
            };

            Assert.Equal(long.MaxValue, dto.Valor);
            Assert.Equal("Valor máximo", dto.Descricao);
            Assert.Equal(int.MaxValue, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Quantidade_Negativa()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 1,
                Descricao = "Quantidade negativa",
                Quantidade = -1
            };

            Assert.Equal(1, dto.Valor);
            Assert.Equal("Quantidade negativa", dto.Descricao);
            Assert.Equal(-1, dto.Quantidade);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Descricoes_Diferentes()
        {
            var dto1 = new QuantidadeAlternativasDto { Valor = 1, Descricao = "Alternativas A e B", Quantidade = 2 };
            var dto2 = new QuantidadeAlternativasDto { Valor = 2, Descricao = "Alternativas A, B e C", Quantidade = 3 };
            var dto3 = new QuantidadeAlternativasDto { Valor = 3, Descricao = "Alternativas A, B, C e D", Quantidade = 4 };
            var dto4 = new QuantidadeAlternativasDto { Valor = 4, Descricao = "Alternativas A, B, C, D e E", Quantidade = 5 };

            Assert.Equal("Alternativas A e B", dto1.Descricao);
            Assert.Equal("Alternativas A, B e C", dto2.Descricao);
            Assert.Equal("Alternativas A, B, C e D", dto3.Descricao);
            Assert.Equal("Alternativas A, B, C, D e E", dto4.Descricao);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativasDto_Com_Valor_E_Quantidade_Diferentes()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = 10,
                Descricao = "Teste",
                Quantidade = 4
            };

            Assert.NotEqual(dto.Valor, dto.Quantidade);
            Assert.Equal(10, dto.Valor);
            Assert.Equal(4, dto.Quantidade);
        }
    }
}
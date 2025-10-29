using System;
using Xunit;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class QuantidadeAlternativasDtoTeste
    {

        private const long ValorValido = 5L;
        private const string DescricaoValida = "Múltipla Escolha - 5 Alternativas";
        private const int QuantidadeValida = 5;

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var dto = new QuantidadeAlternativasDto
            {
                Valor = ValorValido,
                Descricao = DescricaoValida,
                Quantidade = QuantidadeValida
            };

            Assert.Equal(ValorValido, dto.Valor);
            Assert.Equal(DescricaoValida, dto.Descricao);
            Assert.Equal(QuantidadeValida, dto.Quantidade);
        }

        [Fact]
        public void Deve_Atribuir_Valores_Default_E_Nulos_Corretamente()
        {
            const long valorZero = 0L;
            const string descricaoNula = null;
            const int quantidadeZero = 0;

            var dto = new QuantidadeAlternativasDto
            {
                Valor = valorZero,
                Descricao = descricaoNula,
                Quantidade = quantidadeZero
            };

            Assert.Equal(valorZero, dto.Valor);
            Assert.Null(dto.Descricao);
            Assert.Equal(quantidadeZero, dto.Quantidade);
        }
    }
}
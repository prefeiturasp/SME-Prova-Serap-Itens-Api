using System;
using Xunit;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Alternativa
{
    public class AlternativaResumoDtoTeste
    {
        private const long IdValido = 301;
        private const long ItemIdValido = 150;
        private const string DescricaoValida = "A descrição da alternativa A.";
        private const int OrdemValida = 1;
        private const string NumeracaoValida = "A";

        [Fact]
        public void Deve_Criar_Dto_Com_Construtor_E_Atribuir_Propriedades_Corretamente()
        {
            var dto = new AlternativaResumoDto(
                IdValido,
                ItemIdValido,
                DescricaoValida,
                OrdemValida,
                NumeracaoValida
            );

            Assert.Equal(IdValido, dto.Id);
            Assert.Equal(ItemIdValido, dto.ItemId);
            Assert.Equal(DescricaoValida, dto.Descricao);
            Assert.Equal(OrdemValida, dto.Ordem);
            Assert.Equal(NumeracaoValida, dto.Numeracao);
        }

        [Fact]
        public void Deve_Criar_Dto_Com_Descricao_E_Numeracao_Nulas()
        {
            const string descricaoNula = null;
            const string numeracaoVazia = "";

            var dto = new AlternativaResumoDto(
                IdValido,
                ItemIdValido,
                descricaoNula,
                OrdemValida,
                numeracaoVazia
            );

            Assert.Null(dto.Descricao);
            Assert.Equal(numeracaoVazia, dto.Numeracao);
        }

        [Fact]
        public void Deve_Criar_Dto_Com_Valores_Numericos_Zero()
        {
            const long idZero = 0;
            const long itemIdZero = 0;
            const int ordemZero = 0;

            var dto = new AlternativaResumoDto(
                idZero,
                itemIdZero,
                DescricaoValida,
                ordemZero,
                NumeracaoValida
            );

            Assert.Equal(idZero, dto.Id);
            Assert.Equal(itemIdZero, dto.ItemId);
            Assert.Equal(ordemZero, dto.Ordem);
        }
    }
}
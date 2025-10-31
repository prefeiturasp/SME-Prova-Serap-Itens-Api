using System;
using Xunit;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class ItemResumoDtoTeste
    {
        private readonly long IdValido = 200;
        private readonly string CodigoItemValido = "IT-A-2025";
        private readonly string TextoBaseValido = "A floresta amazônica...";
        private readonly string EnunciadoValido = "O que causou o desmatamento?";
        private readonly string FonteValida = "Revista Exemplo";
        private readonly long VersaoItemValida = 5;
        private readonly long QuantidadeVersoesValida = 8;

        [Fact]
        public void Deve_Criar_Dto_Com_Construtor_Parametros_E_Atribuir_Corretamente()
        {
            var dto = new ItemResumoDto(
                IdValido,
                CodigoItemValido,
                TextoBaseValido,
                EnunciadoValido,
                FonteValida,
                VersaoItemValida,
                QuantidadeVersoesValida
            );

            Assert.Equal(IdValido, dto.Id);
            Assert.Equal(CodigoItemValido, dto.CodigoItem);
            Assert.Equal(TextoBaseValido, dto.TextoBase);
            Assert.Equal(EnunciadoValido, dto.Enunciado);
            Assert.Equal(FonteValida, dto.Fonte);
            Assert.Equal(VersaoItemValida, dto.VersaoItem);
            Assert.Equal(QuantidadeVersoesValida, dto.QuantidadeVersoes);
        }

        [Fact]
        public void Construtor_Parametros_Deve_Inicializar_Listas_Vazias()
        {
            var dto = new ItemResumoDto(
                IdValido,
                CodigoItemValido,
                TextoBaseValido,
                EnunciadoValido,
                FonteValida,
                VersaoItemValida,
                QuantidadeVersoesValida
            );

            Assert.NotNull(dto.VersoesDisponiveis);
            Assert.NotNull(dto.Alternativas);
            Assert.Empty(dto.VersoesDisponiveis);
            Assert.Empty(dto.Alternativas);
        }

        [Fact]
        public void Deve_Criar_Dto_Com_Construtor_Padrao_E_Atribuir_Propriedades()
        {
            var versoesMock = new List<ItemVersaoDto> {  };
            var alternativasMock = new List<AlternativaResumoDto> {  };

            var dto = new ItemResumoDto
            {
                Id = 1,
                CodigoItem = "PADRAO-01",
                TextoBase = "Texto.",
                VersaoItem = 1,
                QuantidadeVersoes = 1,
                VersoesDisponiveis = versoesMock,
                Alternativas = alternativasMock
            };

            Assert.Equal(1, dto.Id);
            Assert.Equal("PADRAO-01", dto.CodigoItem);
            Assert.Equal(versoesMock, dto.VersoesDisponiveis);
            Assert.Equal(alternativasMock, dto.Alternativas);
            Assert.True(dto.VersoesDisponiveis.Any() == versoesMock.Any());
        }
    }
}
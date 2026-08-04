using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class ItemVersaoDtoTeste
    {
        private readonly long IdValido = 1001;
        private readonly string CodigoItemValido = "IT-REF-X99";
        private readonly long VersaoItemValida = 3;
        private readonly DateTime DataCriacaoValida = new DateTime(2025, 08, 15, 14, 30, 0);
        private const string DataCriacaoFormatadaEsperada = "15/08/2025";
        private readonly SituacaoItem? SituacaoValida = SituacaoItem.Ativo;

        [Fact]
        public void Deve_Criar_Dto_Com_Construtor_E_Atribuir_Propriedades_Corretamente()
        {
            var dto = new ItemVersaoDto(
                IdValido,
                CodigoItemValido,
                VersaoItemValida,
                DataCriacaoValida,
                SituacaoValida
            );

            Assert.Equal(IdValido, dto.Id);
            Assert.Equal(CodigoItemValido, dto.CodigoItem);
            Assert.Equal(VersaoItemValida, dto.VersaoItem);
            Assert.Equal(DataCriacaoFormatadaEsperada, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Criar_Dto_Com_Construtor_Padrao_E_Propriedades_Setadas()
        {
            var dataCriacaoParaSet = "2024-01-20";

            var dto = new ItemVersaoDto(
                50,
                "IT-001",
                1,
                DateTime.Parse(dataCriacaoParaSet),
                SituacaoValida
            );

            Assert.Equal(50, dto.Id);
            Assert.Equal("IT-001", dto.CodigoItem);
            Assert.Equal(1, dto.VersaoItem);
            Assert.Equal("20/01/2024", dto.DataCriacao);
            Assert.Equal(dto.Situacao, SituacaoValida);
        }

        [Fact]
        public void Deve_Formatar_Data_Corretamente_Em_Cenario_Limite()
        {
            var dataLimite = new DateTime(2026, 01, 01, 00, 0, 0);
            const string dataLimiteEsperada = "01/01/2026";

            var dto = new ItemVersaoDto(
                IdValido,
                CodigoItemValido,
                VersaoItemValida,
                dataLimite,
                SituacaoValida
            );

            Assert.Equal(dataLimiteEsperada, dto.DataCriacao);
            Assert.Equal(dto.Situacao, SituacaoValida);
        }
    }
}
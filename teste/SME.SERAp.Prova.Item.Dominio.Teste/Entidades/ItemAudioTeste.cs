using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class ItemAudioTeste
    {
        private const long ArquivoIdValido = 500L;
        private const long ItemIdValido = 100L;
        private const int SituacaoValida = 1;
        private readonly DateTime DataCriacaoValida = new DateTime(2023, 10, 25, 10, 0, 0);

        [Fact(DisplayName = "Deve criar ItemAudio com construtor padrão")]
        public void Deve_Criar_ItemAudio_Com_Construtor_Padrao()
        {
            var itemAudio = new ItemAudio
            {
                Id = 1,
                ArquivoId = ArquivoIdValido,
                ItemId = ItemIdValido,
                Situacao = SituacaoValida,
                CriadoEm = DataCriacaoValida,
                AlteradoEm = DateTime.Now
            };

            Assert.Equal(1, itemAudio.Id);
            Assert.Equal(ArquivoIdValido, itemAudio.ArquivoId);
            Assert.Equal(ItemIdValido, itemAudio.ItemId);
            Assert.Equal(SituacaoValida, itemAudio.Situacao);
            Assert.Equal(DataCriacaoValida, itemAudio.CriadoEm);
            Assert.NotNull(itemAudio.AlteradoEm);
        }

        [Fact(DisplayName = "Deve criar ItemAudio com construtor de parâmetros e Id=0")]
        public void Deve_Criar_ItemAudio_Com_Construtor_Parametros_E_Id_Zero()
        {
            var itemAudio = new ItemAudio(
                ArquivoIdValido,
                ItemIdValido,
                SituacaoValida,
                DataCriacaoValida);

            Assert.Equal(0, itemAudio.Id);
            Assert.Equal(ArquivoIdValido, itemAudio.ArquivoId);
            Assert.Equal(ItemIdValido, itemAudio.ItemId);
            Assert.Equal(SituacaoValida, itemAudio.Situacao);
            Assert.Equal(DataCriacaoValida, itemAudio.CriadoEm);
            Assert.Null(itemAudio.AlteradoEm);
        }

        [Fact(DisplayName = "Deve manter AlteradoEm como null se Id for setado após a criação")]
        public void Deve_Manter_AlteradoEm_Nulo_Quando_Id_Maior_Que_Zero()
        {
            var itemAudio = new ItemAudio(
                ArquivoIdValido,
                ItemIdValido,
                SituacaoValida,
                DataCriacaoValida);

            Assert.Equal(0, itemAudio.Id);
            Assert.Null(itemAudio.AlteradoEm);

            itemAudio.Id = 99;

            Assert.Equal(99, itemAudio.Id);
            Assert.Null(itemAudio.AlteradoEm);
        }
    }
}
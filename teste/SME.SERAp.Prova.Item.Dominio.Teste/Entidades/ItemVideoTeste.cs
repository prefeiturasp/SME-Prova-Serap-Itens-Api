using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class ItemVideoTeste
    {
        private const long ArquivoIdValido = 600L;
        private const long ItemIdValido = 200L;
        private const int SituacaoValida = 2;
        private readonly DateTime DataCriacaoValida = new DateTime(2023, 10, 25, 11, 0, 0);

        [Fact]
        public void Deve_Criar_ItemVideo_Com_Construtor_Padrao()
        {
            var itemVideo = new ItemVideo
            {
                Id = 5,
                ArquivoId = ArquivoIdValido,
                ItemId = ItemIdValido,
                Situacao = SituacaoValida,
                CriadoEm = DataCriacaoValida,
                AlteradoEm = DateTime.Now
            };

            Assert.Equal(5, itemVideo.Id);
            Assert.Equal(ArquivoIdValido, itemVideo.ArquivoId);
            Assert.Equal(ItemIdValido, itemVideo.ItemId);
            Assert.Equal(SituacaoValida, itemVideo.Situacao);
            Assert.Equal(DataCriacaoValida, itemVideo.CriadoEm);
            Assert.NotNull(itemVideo.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_ItemVideo_Com_Construtor_Parametros_E_AlteradoEm_Nulo_Quando_Id_Zero()
        {
            var itemVideo = new ItemVideo(
                ArquivoIdValido,
                ItemIdValido,
                SituacaoValida,
                DataCriacaoValida);

            Assert.Equal(0, itemVideo.Id);
            Assert.Equal(ArquivoIdValido, itemVideo.ArquivoId);
            Assert.Equal(ItemIdValido, itemVideo.ItemId);
            Assert.Equal(SituacaoValida, itemVideo.Situacao);
            Assert.Equal(DataCriacaoValida, itemVideo.CriadoEm);
            Assert.Null(itemVideo.AlteradoEm);
        }

        [Fact]
        public void Deve_Manter_AlteradoEm_Como_Valor_Inicial_Quando_Id_Maior_Que_Zero()
        {
            var dataAlteracaoInicial = new DateTime(2023, 1, 1);

            var itemVideo = new ItemVideo
            {
                Id = 99,
                AlteradoEm = dataAlteracaoInicial
            };

            var itemVideoComParametros = new ItemVideo(
                ArquivoIdValido,
                ItemIdValido,
                SituacaoValida,
                DataCriacaoValida);

            itemVideoComParametros.Id = 99;

            Assert.Equal(99, itemVideoComParametros.Id);
            Assert.Null(itemVideoComParametros.AlteradoEm);
        }
    }
}
using System;
using Xunit;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class AlternativaTeste
    {
        private readonly string DescricaoValida = "Descrição da alternativa.";
        private readonly string JustificativaValida = "Justificativa da resposta correta.";
        private readonly string NumeracaoValida = "A";
        private readonly bool CorretaValida = true;
        private readonly int OrdemValida = 1;
        private readonly DateTime CriadoEmValida = DateTime.Today;
        private readonly long ItemIdValida = 98765;

        [Fact]
        public void Deve_Criar_Alternativa_Com_Construtor_Padrao_E_Propriedades()
        {
            var dataAlteracao = DateTime.Today.AddDays(1);

            var alternativa = new Alternativa
            {
                Id = 1,
                Descricao = DescricaoValida,
                Justificativa = JustificativaValida,
                Numeracao = NumeracaoValida,
                Correta = CorretaValida,
                Ordem = OrdemValida,
                CriadoEm = CriadoEmValida.AddDays(-1),
                AlteradoEm = dataAlteracao,
                ItemId = ItemIdValida
            };

            Assert.Equal(1, alternativa.Id);
            Assert.Equal(DescricaoValida, alternativa.Descricao);
            Assert.Equal(JustificativaValida, alternativa.Justificativa);
            Assert.Equal(NumeracaoValida, alternativa.Numeracao);
            Assert.Equal(CorretaValida, alternativa.Correta);
            Assert.Equal(OrdemValida, alternativa.Ordem);
            Assert.Equal(CriadoEmValida.AddDays(-1), alternativa.CriadoEm);
            Assert.Equal(dataAlteracao, alternativa.AlteradoEm);
            Assert.Equal(ItemIdValida, alternativa.ItemId);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Construtor_Parametros_E_Setar_AlteradoEm_Como_Null()
        {
            var alternativa = new Alternativa(
                DescricaoValida,
                JustificativaValida,
                NumeracaoValida,
                CorretaValida,
                OrdemValida,
                CriadoEmValida,
                ItemIdValida
            );

            Assert.Equal(0, alternativa.Id);

            Assert.Null(alternativa.AlteradoEm);

            Assert.Equal(DescricaoValida, alternativa.Descricao);
            Assert.Equal(JustificativaValida, alternativa.Justificativa);
            Assert.Equal(NumeracaoValida, alternativa.Numeracao);
            Assert.Equal(CorretaValida, alternativa.Correta);
            Assert.Equal(OrdemValida, alternativa.Ordem);
            Assert.Equal(CriadoEmValida, alternativa.CriadoEm);
            Assert.Equal(ItemIdValida, alternativa.ItemId);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Incorreta()
        {
            var alternativa = new Alternativa(
                DescricaoValida,
                JustificativaValida,
                NumeracaoValida,
                correta: false,
                OrdemValida,
                CriadoEmValida,
                ItemIdValida
            );

            Assert.False(alternativa.Correta);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Campos_Nulos_Ou_Vazios()
        {
            string descricaoVazia = string.Empty;
            string justificativaNula = null;
            string numeracaoNula = null;

            var alternativa = new Alternativa(
                descricaoVazia,
                justificativaNula,
                numeracaoNula,
                CorretaValida,
                OrdemValida,
                CriadoEmValida,
                ItemIdValida
            );

            Assert.Equal(string.Empty, alternativa.Descricao);
            Assert.Null(alternativa.Justificativa);
            Assert.Null(alternativa.Numeracao);
        }
    }
}
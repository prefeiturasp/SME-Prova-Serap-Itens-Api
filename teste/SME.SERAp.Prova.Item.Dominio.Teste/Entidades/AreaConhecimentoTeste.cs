using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class AreaConhecimentoTeste
    {
        [Fact]
        public void Deve_Criar_AreaConhecimento_Com_Construtor_Padrao()
        {
            var areaConhecimento = new AreaConhecimento
            {
                LegadoId = 100,
                Descricao = "Matemática",
                Status = 1,
                Codigo = 10,
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20)
            };

            Assert.Equal(100, areaConhecimento.LegadoId);
            Assert.Equal("Matemática", areaConhecimento.Descricao);
            Assert.Equal(1, areaConhecimento.Status);
            Assert.Equal(10, areaConhecimento.Codigo);
            Assert.Equal(new DateTime(2024, 1, 15), areaConhecimento.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), areaConhecimento.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_AreaConhecimento_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;

            var areaConhecimento = new AreaConhecimento(null, 200, "Língua Portuguesa", StatusGeral.Ativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(200, areaConhecimento.LegadoId);
            Assert.Equal("Língua Portuguesa", areaConhecimento.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, areaConhecimento.Status);
            Assert.InRange(areaConhecimento.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(areaConhecimento.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(areaConhecimento.CriadoEm, areaConhecimento.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_AreaConhecimento_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var areaConhecimento = new AreaConhecimento(50, 300, "Ciências da Natureza", StatusGeral.Inativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(50, areaConhecimento.Id);
            Assert.Equal(300, areaConhecimento.LegadoId);
            Assert.Equal("Ciências da Natureza", areaConhecimento.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, areaConhecimento.Status);
            Assert.InRange(areaConhecimento.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var areaConhecimento = new AreaConhecimento(75, 600, "Ensino Religioso", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), areaConhecimento.CriadoEm);
            Assert.NotEqual(default(DateTime), areaConhecimento.AlteradoEm);
        }
    }
}
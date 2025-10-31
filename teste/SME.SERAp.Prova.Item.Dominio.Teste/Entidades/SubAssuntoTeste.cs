using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class SubAssuntoTeste
    {
        [Fact]
        public void Deve_Criar_SubAssunto_Com_Construtor_Padrao()
        {
            var subAssunto = new SubAssunto
            {
                LegadoId = 100,
                AssuntoId = 200,
                Descricao = "Subtópico de Matemática",
                Status = 1,
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20)
            };

            Assert.Equal(100, subAssunto.LegadoId);
            Assert.Equal(200, subAssunto.AssuntoId);
            Assert.Equal("Subtópico de Matemática", subAssunto.Descricao);
            Assert.Equal(1, subAssunto.Status);
            Assert.Equal(new DateTime(2024, 1, 15), subAssunto.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), subAssunto.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SubAssunto_Com_Construtor_Parametros_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var subAssunto = new SubAssunto(null, 100, 200, "Geometria Plana", StatusGeral.Ativo);

            Assert.Equal(100, subAssunto.LegadoId);
            Assert.Equal(200, subAssunto.AssuntoId);
            Assert.Equal("Geometria Plana", subAssunto.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, subAssunto.Status);
            Assert.True(subAssunto.CriadoEm >= dataAntes);
            Assert.True(subAssunto.AlteradoEm >= dataAntes);
            Assert.Equal(subAssunto.CriadoEm, subAssunto.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SubAssunto_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var subAssunto = new SubAssunto(50, 100, 200, "Álgebra Linear", StatusGeral.Inativo);

            Assert.Equal(50, subAssunto.Id);
            Assert.Equal(100, subAssunto.LegadoId);
            Assert.Equal(200, subAssunto.AssuntoId);
            Assert.Equal("Álgebra Linear", subAssunto.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, subAssunto.Status);
            Assert.True(subAssunto.AlteradoEm >= dataAntes);
        }

        [Fact]
        public void Deve_Criar_SubAssunto_Com_Status_Informado_Mesmo_Quando_Id_Null()
        {
            var subAssuntoAtivo = new SubAssunto(null, 100, 200, "Frações Ativo", StatusGeral.Ativo);
            var subAssuntoInativo = new SubAssunto(null, 100, 200, "Frações Inativo", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Ativo, subAssuntoAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, subAssuntoInativo.Status);
        }

        [Fact]
        public void Deve_Criar_SubAssunto_Com_Status_Informado_Quando_Id_Preenchido()
        {
            var subAssunto = new SubAssunto(10, 100, 200, "Trigonometria", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, subAssunto.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var subAssunto = new SubAssunto(null, 100, 200, "Estatística", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.True(subAssunto.CriadoEm >= dataAntes && subAssunto.CriadoEm <= dataDepois);
        }

        [Fact]
        public void Nao_Deve_Definir_CriadoEm_Quando_Id_Informado()
        {
            var dataDefault = default(DateTime);

            var subAssunto = new SubAssunto(25, 100, 200, "Probabilidade", StatusGeral.Ativo);

            Assert.Equal(dataDefault, subAssunto.CriadoEm);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Sempre()
        {
            var dataAntes = DateTime.Now;
            var subAssuntoNovo = new SubAssunto(null, 100, 200, "Teste 1", StatusGeral.Ativo);
            var subAssuntoEditado = new SubAssunto(15, 100, 200, "Teste 2", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.True(subAssuntoNovo.AlteradoEm >= dataAntes && subAssuntoNovo.AlteradoEm <= dataDepois);
            Assert.True(subAssuntoEditado.AlteradoEm >= dataAntes && subAssuntoEditado.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var subAssunto = new SubAssunto(null, 100, 200, "", StatusGeral.Ativo);

            Assert.Equal("", subAssunto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero()
        {
            var subAssunto = new SubAssunto(null, 0, 200, "Teste", StatusGeral.Ativo);

            Assert.Equal(0, subAssunto.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_AssuntoId_Zero()
        {
            var subAssunto = new SubAssunto(null, 100, 0, "Teste", StatusGeral.Ativo);

            Assert.Equal(0, subAssunto.AssuntoId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_LegadoId()
        {
            var subAssunto = new SubAssunto(null, -100, 200, "Teste", StatusGeral.Ativo);

            Assert.Equal(-100, subAssunto.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_AssuntoId()
        {
            var subAssunto = new SubAssunto(null, 100, -200, "Teste", StatusGeral.Ativo);

            Assert.Equal(-200, subAssunto.AssuntoId);
        }

        [Fact]
        public void Deve_Criar_SubAssunto_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('A', 1000);

            var subAssunto = new SubAssunto(null, 100, 200, descricaoLonga, StatusGeral.Ativo);

            Assert.Equal(descricaoLonga, subAssunto.Descricao);
            Assert.Equal(1000, subAssunto.Descricao.Length);
        }
    }
}
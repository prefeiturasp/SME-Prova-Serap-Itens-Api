using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class TipoGradeTeste
    {
        [Fact]
        public void Deve_Criar_TipoGrade_Com_Construtor_Parametros_Id_Null()
        {
            var dataAntes = DateTime.Now;
            var tipoGrade = new TipoGrade(null, 100, 200, "Grade Regular", 1, StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.Equal(100, tipoGrade.LegadoId);
            Assert.Equal(200, tipoGrade.MatrizId);
            Assert.Equal("Grade Regular", tipoGrade.Descricao);
            Assert.Equal(1, tipoGrade.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, tipoGrade.Status);
            Assert.True(tipoGrade.CriadoEm >= dataAntes && tipoGrade.CriadoEm <= dataDepois);
            Assert.True(tipoGrade.AlteradoEm >= dataAntes && tipoGrade.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Criar_TipoGrade_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var tipoGrade = new TipoGrade(50, 100, 200, "Grade Especial", 2, StatusGeral.Inativo);

            Assert.Equal(50, tipoGrade.Id);
            Assert.Equal(100, tipoGrade.LegadoId);
            Assert.Equal(200, tipoGrade.MatrizId);
            Assert.Equal("Grade Especial", tipoGrade.Descricao);
            Assert.Equal(2, tipoGrade.Ordem);
            Assert.Equal((int)StatusGeral.Inativo, tipoGrade.Status);
            Assert.True(tipoGrade.AlteradoEm >= dataAntes);
        }

        [Fact]
        public void Deve_Criar_TipoGrade_Com_Status_Informado_Mesmo_Quando_Id_Null()
        {
            var tipoGradeAtivo = new TipoGrade(null, 100, 200, "Grade Ativo", 1, StatusGeral.Ativo);
            var tipoGradeInativo = new TipoGrade(null, 100, 200, "Grade Inativo", 1, StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Ativo, tipoGradeAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, tipoGradeInativo.Status);
        }

        [Fact]
        public void Deve_Criar_TipoGrade_Com_Status_Informado_Quando_Id_Preenchido()
        {
            var tipoGrade = new TipoGrade(10, 100, 200, "Grade Teste", 1, StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, tipoGrade.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var tipoGrade = new TipoGrade(null, 100, 200, "Grade Nova", 1, StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.True(tipoGrade.CriadoEm >= dataAntes && tipoGrade.CriadoEm <= dataDepois);
        }

        [Fact]
        public void Nao_Deve_Definir_CriadoEm_Quando_Id_Informado()
        {
            var dataDefault = default(DateTime);

            var tipoGrade = new TipoGrade(25, 100, 200, "Grade Editada", 1, StatusGeral.Ativo);

            Assert.Equal(dataDefault, tipoGrade.CriadoEm);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Sempre()
        {
            var dataAntes = DateTime.Now;

            var tipoGradeNovo = new TipoGrade(null, 100, 200, "Teste 1", 1, StatusGeral.Ativo);
            var tipoGradeEditado = new TipoGrade(15, 100, 200, "Teste 2", 1, StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.True(tipoGradeNovo.AlteradoEm >= dataAntes && tipoGradeNovo.AlteradoEm <= dataDepois);
            Assert.True(tipoGradeEditado.AlteradoEm >= dataAntes && tipoGradeEditado.AlteradoEm <= dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var tipoGrade = new TipoGrade(null, 100, 200, "", 1, StatusGeral.Ativo);

            Assert.Equal("", tipoGrade.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero()
        {
            var tipoGrade = new TipoGrade(null, 0, 200, "Teste", 1, StatusGeral.Ativo);

            Assert.Equal(0, tipoGrade.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_MatrizId_Zero()
        {
            var tipoGrade = new TipoGrade(null, 100, 0, "Teste", 1, StatusGeral.Ativo);

            Assert.Equal(0, tipoGrade.MatrizId);
        }

        [Fact]
        public void Deve_Aceitar_Ordem_Zero()
        {
            var tipoGrade = new TipoGrade(null, 100, 200, "Teste", 0, StatusGeral.Ativo);

            Assert.Equal(0, tipoGrade.Ordem);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_LegadoId()
        {
            var tipoGrade = new TipoGrade(null, -100, 200, "Teste", 1, StatusGeral.Ativo);

            Assert.Equal(-100, tipoGrade.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_MatrizId()
        {
            var tipoGrade = new TipoGrade(null, 100, -200, "Teste", 1, StatusGeral.Ativo);

            Assert.Equal(-200, tipoGrade.MatrizId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_Ordem()
        {
            var tipoGrade = new TipoGrade(null, 100, 200, "Teste", -1, StatusGeral.Ativo);

            Assert.Equal(-1, tipoGrade.Ordem);
        }

        [Fact]
        public void Deve_Criar_TipoGrade_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('A', 1000);

            var tipoGrade = new TipoGrade(null, 100, 200, descricaoLonga, 1, StatusGeral.Ativo);

            Assert.Equal(descricaoLonga, tipoGrade.Descricao);
            Assert.Equal(1000, tipoGrade.Descricao.Length);
        }

        [Fact]
        public void Deve_Criar_TipoGrade_Com_Ordem_Grande()
        {
            var tipoGrade = new TipoGrade(null, 100, 200, "Teste", 999999, StatusGeral.Ativo);

            Assert.Equal(999999, tipoGrade.Ordem);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Null()
        {
            var tipoGrade = new TipoGrade(null, 123, 456, "Grade Completa", 5, StatusGeral.Ativo);

            Assert.Equal(123, tipoGrade.LegadoId);
            Assert.Equal(456, tipoGrade.MatrizId);
            Assert.Equal("Grade Completa", tipoGrade.Descricao);
            Assert.Equal(5, tipoGrade.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, tipoGrade.Status);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Informado()
        {
            var tipoGrade = new TipoGrade(99, 123, 456, "Grade Completa", 5, StatusGeral.Inativo);

            Assert.Equal(99, tipoGrade.Id);
            Assert.Equal(123, tipoGrade.LegadoId);
            Assert.Equal(456, tipoGrade.MatrizId);
            Assert.Equal("Grade Completa", tipoGrade.Descricao);
            Assert.Equal(5, tipoGrade.Ordem);
            Assert.Equal((int)StatusGeral.Inativo, tipoGrade.Status);
        }
    }
}
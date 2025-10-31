using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class CompetenciaTeste
    {
        [Fact]
        public void Deve_Criar_Competencia_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;
            var competencia = new Competencia(null, "C01", 100, 50, "Competência de Leitura", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.Equal("C01", competencia.Codigo);
            Assert.Equal(100, competencia.LegadoId);
            Assert.Equal(50, competencia.MatrizId);
            Assert.Equal("Competência de Leitura", competencia.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, competencia.Status);
            Assert.InRange(competencia.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(competencia.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;
            var competencia = new Competencia(75, "C02", 200, 100, "Competência de Escrita", StatusGeral.Inativo);
            var dataDepois = DateTime.Now;

            Assert.Equal(75, competencia.Id);
            Assert.Equal("C02", competencia.Codigo);
            Assert.Equal(200, competencia.LegadoId);
            Assert.Equal(100, competencia.MatrizId);
            Assert.Equal("Competência de Escrita", competencia.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, competencia.Status);
            Assert.InRange(competencia.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var competencia = new Competencia(50, "C03", 300, 150, "Competência Matemática", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), competencia.CriadoEm);
            Assert.NotEqual(default(DateTime), competencia.AlteradoEm);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Quando_Id_Nulo()
        {
            var dataAntes = DateTime.Now;
            var competencia = new Competencia(null, "C04", 400, 200, "Competência Científica", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.InRange(competencia.CriadoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Nao_Deve_Definir_CriadoEm_Quando_Id_Informado()
        {
            var competencia = new Competencia(60, "C05", 500, 250, "Competência Histórica", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), competencia.CriadoEm);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Sempre()
        {
            var dataAntes = DateTime.Now;
            var competenciaNova = new Competencia(null, "C06", 600, 300, "Competência Nova", StatusGeral.Ativo);
            var competenciaEditada = new Competencia(70, "C07", 700, 350, "Competência Editada", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.InRange(competenciaNova.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(competenciaEditada.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Status_Informado_Mesmo_Quando_Id_Nulo()
        {
            var competenciaAtiva = new Competencia(null, "C08", 800, 400, "Competência Ativa", StatusGeral.Ativo);
            var competenciaInativa = new Competencia(null, "C09", 900, 450, "Competência Inativa", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Ativo, competenciaAtiva.Status);
            Assert.Equal((int)StatusGeral.Inativo, competenciaInativa.Status);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Status_Informado_Quando_Id_Preenchido()
        {
            var competencia = new Competencia(99, "C10", 1000, 500, "Competência Histórica", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, competencia.Status);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Vazio()
        {
            var competencia = new Competencia(null, "", 100, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal("", competencia.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Nulo()
        {
            var competencia = new Competencia(null, null, 100, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Null(competencia.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var competencia = new Competencia(null, "C11", 100, 50, "", StatusGeral.Ativo);

            Assert.Equal("", competencia.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Nula()
        {
            var competencia = new Competencia(null, "C12", 100, 50, null, StatusGeral.Ativo);

            Assert.Null(competencia.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero()
        {
            var competencia = new Competencia(null, "C13", 0, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(0, competencia.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_MatrizId_Zero()
        {
            var competencia = new Competencia(null, "C14", 100, 0, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(0, competencia.MatrizId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_LegadoId()
        {
            var competencia = new Competencia(null, "C15", -100, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(-100, competencia.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_MatrizId()
        {
            var competencia = new Competencia(null, "C16", 100, -50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(-50, competencia.MatrizId);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Codigo_Longo()
        {
            var codigoLongo = new string('C', 500);
            var competencia = new Competencia(null, codigoLongo, 100, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(codigoLongo, competencia.Codigo);
            Assert.Equal(500, competencia.Codigo.Length);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('D', 1000);
            var competencia = new Competencia(null, "C17", 100, 50, descricaoLonga, StatusGeral.Ativo);

            Assert.Equal(descricaoLonga, competencia.Descricao);
            Assert.Equal(1000, competencia.Descricao.Length);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Codigo_Alfanumerico()
        {
            var competencia = new Competencia(null, "COMP-001-A1", 100, 50, "Competência Alfanumérica", StatusGeral.Ativo);

            Assert.Equal("COMP-001-A1", competencia.Codigo);
        }

        [Fact]
        public void Deve_Criar_Competencia_Com_Codigo_Com_Caracteres_Especiais()
        {
            var competencia = new Competencia(null, "C@#$%", 100, 50, "Competência Especial", StatusGeral.Ativo);

            Assert.Equal("C@#$%", competencia.Codigo);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Nulo()
        {
            var competencia = new Competencia(null, "C18", 123, 456, "Competência Completa", StatusGeral.Ativo);

            Assert.Equal("C18", competencia.Codigo);
            Assert.Equal(123, competencia.LegadoId);
            Assert.Equal(456, competencia.MatrizId);
            Assert.Equal("Competência Completa", competencia.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, competencia.Status);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Informado()
        {
            var competencia = new Competencia(88, "C19", 789, 321, "Competência Completa", StatusGeral.Inativo);

            Assert.Equal(88, competencia.Id);
            Assert.Equal("C19", competencia.Codigo);
            Assert.Equal(789, competencia.LegadoId);
            Assert.Equal(321, competencia.MatrizId);
            Assert.Equal("Competência Completa", competencia.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, competencia.Status);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Muito_Grande()
        {
            var competencia = new Competencia(null, "C20", long.MaxValue, 50, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(long.MaxValue, competencia.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_MatrizId_Muito_Grande()
        {
            var competencia = new Competencia(null, "C21", 100, long.MaxValue, "Competência Teste", StatusGeral.Ativo);

            Assert.Equal(long.MaxValue, competencia.MatrizId);
        }
    }
}
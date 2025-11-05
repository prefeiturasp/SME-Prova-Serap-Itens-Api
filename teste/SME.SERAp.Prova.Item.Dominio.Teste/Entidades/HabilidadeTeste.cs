using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class HabilidadeTeste
    {
        [Fact]
        public void Deve_Criar_Habilidade_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;
            var habilidade = new Habilidade(null, "H01", 100, 50, "Habilidade de Interpretação de Texto", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.Equal("H01", habilidade.Codigo);
            Assert.Equal(100, habilidade.LegadoId);
            Assert.Equal(50, habilidade.CompetenciaId);
            Assert.Equal("Habilidade de Interpretação de Texto", habilidade.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, habilidade.Status);
            Assert.InRange(habilidade.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(habilidade.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;
            var habilidade = new Habilidade(75, "H02", 200, 100, "Habilidade de Resolução de Problemas", StatusGeral.Inativo);
            var dataDepois = DateTime.Now;

            Assert.Equal(75, habilidade.Id);
            Assert.Equal("H02", habilidade.Codigo);
            Assert.Equal(200, habilidade.LegadoId);
            Assert.Equal(100, habilidade.CompetenciaId);
            Assert.Equal("Habilidade de Resolução de Problemas", habilidade.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, habilidade.Status);
            Assert.InRange(habilidade.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var habilidade = new Habilidade(50, "H03", 300, 150, "Habilidade de Análise Crítica", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), habilidade.CriadoEm);
            Assert.NotEqual(default(DateTime), habilidade.AlteradoEm);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Quando_Id_Nulo()
        {
            var dataAntes = DateTime.Now;
            var habilidade = new Habilidade(null, "H04", 400, 200, "Habilidade de Comunicação", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.InRange(habilidade.CriadoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Nao_Deve_Definir_CriadoEm_Quando_Id_Informado()
        {
            var habilidade = new Habilidade(60, "H05", 500, 250, "Habilidade de Raciocínio Lógico", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), habilidade.CriadoEm);
        }

        [Fact]
        public void Deve_Definir_AlteradoEm_Sempre()
        {
            var dataAntes = DateTime.Now;
            var habilidadeNova = new Habilidade(null, "H06", 600, 300, "Habilidade Nova", StatusGeral.Ativo);
            var habilidadeEditada = new Habilidade(70, "H07", 700, 350, "Habilidade Editada", StatusGeral.Ativo);
            var dataDepois = DateTime.Now;

            Assert.InRange(habilidadeNova.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(habilidadeEditada.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Status_Informado_Mesmo_Quando_Id_Nulo()
        {
            var habilidadeAtiva = new Habilidade(null, "H08", 800, 400, "Habilidade Ativa", StatusGeral.Ativo);
            var habilidadeInativa = new Habilidade(null, "H09", 900, 450, "Habilidade Inativa", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Ativo, habilidadeAtiva.Status);
            Assert.Equal((int)StatusGeral.Inativo, habilidadeInativa.Status);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Status_Informado_Quando_Id_Preenchido()
        {
            var habilidade = new Habilidade(99, "H10", 1000, 500, "Habilidade Teste", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, habilidade.Status);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Vazio()
        {
            var habilidade = new Habilidade(null, "", 100, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal("", habilidade.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Nulo()
        {
            var habilidade = new Habilidade(null, null, 100, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Null(habilidade.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var habilidade = new Habilidade(null, "H11", 100, 50, "", StatusGeral.Ativo);

            Assert.Equal("", habilidade.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Nula()
        {
            var habilidade = new Habilidade(null, "H12", 100, 50, null, StatusGeral.Ativo);

            Assert.Null(habilidade.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero()
        {
            var habilidade = new Habilidade(null, "H13", 0, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(0, habilidade.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_CompetenciaId_Zero()
        {
            var habilidade = new Habilidade(null, "H14", 100, 0, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(0, habilidade.CompetenciaId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_LegadoId()
        {
            var habilidade = new Habilidade(null, "H15", -100, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(-100, habilidade.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Negativos_Para_CompetenciaId()
        {
            var habilidade = new Habilidade(null, "H16", 100, -50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(-50, habilidade.CompetenciaId);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Codigo_Longo()
        {
            var codigoLongo = new string('H', 500);
            var habilidade = new Habilidade(null, codigoLongo, 100, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(codigoLongo, habilidade.Codigo);
            Assert.Equal(500, habilidade.Codigo.Length);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('D', 1000);
            var habilidade = new Habilidade(null, "H17", 100, 50, descricaoLonga, StatusGeral.Ativo);

            Assert.Equal(descricaoLonga, habilidade.Descricao);
            Assert.Equal(1000, habilidade.Descricao.Length);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Codigo_Alfanumerico()
        {
            var habilidade = new Habilidade(null, "HAB-001-A1", 100, 50, "Habilidade Alfanumérica", StatusGeral.Ativo);

            Assert.Equal("HAB-001-A1", habilidade.Codigo);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Codigo_Com_Caracteres_Especiais()
        {
            var habilidade = new Habilidade(null, "H@#$%", 100, 50, "Habilidade Especial", StatusGeral.Ativo);

            Assert.Equal("H@#$%", habilidade.Codigo);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Diferentes_Codigos()
        {
            var habilidade1 = new Habilidade(null, "HAB-001", 600, 300, "Habilidade A", StatusGeral.Ativo);
            var habilidade2 = new Habilidade(null, "HAB-002", 700, 350, "Habilidade B", StatusGeral.Ativo);
            var habilidade3 = new Habilidade(null, "H-XYZ", 800, 400, "Habilidade C", StatusGeral.Ativo);

            Assert.Equal("HAB-001", habilidade1.Codigo);
            Assert.Equal("HAB-002", habilidade2.Codigo);
            Assert.Equal("H-XYZ", habilidade3.Codigo);
        }

        [Fact]
        public void Deve_Criar_Habilidade_Com_Diferentes_CompetenciaIds()
        {
            var habilidade1 = new Habilidade(null, "H10", 100, 10, "Descrição 1", StatusGeral.Ativo);
            var habilidade2 = new Habilidade(null, "H20", 200, 20, "Descrição 2", StatusGeral.Ativo);
            var habilidade3 = new Habilidade(null, "H30", 300, 30, "Descrição 3", StatusGeral.Ativo);

            Assert.Equal(10, habilidade1.CompetenciaId);
            Assert.Equal(20, habilidade2.CompetenciaId);
            Assert.Equal(30, habilidade3.CompetenciaId);
        }

        [Fact]
        public void Deve_Criar_Multiplas_Habilidades_Para_Mesma_Competencia()
        {
            var habilidade1 = new Habilidade(null, "H-C1-01", 1000, 50, "Habilidade 1 da Competência 50", StatusGeral.Ativo);
            var habilidade2 = new Habilidade(null, "H-C1-02", 1100, 50, "Habilidade 2 da Competência 50", StatusGeral.Ativo);
            var habilidade3 = new Habilidade(null, "H-C1-03", 1200, 50, "Habilidade 3 da Competência 50", StatusGeral.Ativo);

            Assert.Equal(50, habilidade1.CompetenciaId);
            Assert.Equal(50, habilidade2.CompetenciaId);
            Assert.Equal(50, habilidade3.CompetenciaId);
            Assert.Equal("H-C1-01", habilidade1.Codigo);
            Assert.Equal("H-C1-02", habilidade2.Codigo);
            Assert.Equal("H-C1-03", habilidade3.Codigo);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Nulo()
        {
            var habilidade = new Habilidade(null, "H18", 123, 456, "Habilidade Completa", StatusGeral.Ativo);

            Assert.Equal("H18", habilidade.Codigo);
            Assert.Equal(123, habilidade.LegadoId);
            Assert.Equal(456, habilidade.CompetenciaId);
            Assert.Equal("Habilidade Completa", habilidade.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, habilidade.Status);
        }

        [Fact]
        public void Deve_Manter_Todos_Os_Parametros_Quando_Id_Informado()
        {
            var habilidade = new Habilidade(88, "H19", 789, 321, "Habilidade Completa", StatusGeral.Inativo);

            Assert.Equal(88, habilidade.Id);
            Assert.Equal("H19", habilidade.Codigo);
            Assert.Equal(789, habilidade.LegadoId);
            Assert.Equal(321, habilidade.CompetenciaId);
            Assert.Equal("Habilidade Completa", habilidade.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, habilidade.Status);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Muito_Grande()
        {
            var habilidade = new Habilidade(null, "H20", long.MaxValue, 50, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(long.MaxValue, habilidade.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_CompetenciaId_Muito_Grande()
        {
            var habilidade = new Habilidade(null, "H21", 100, long.MaxValue, "Habilidade Teste", StatusGeral.Ativo);

            Assert.Equal(long.MaxValue, habilidade.CompetenciaId);
        }
    }
}
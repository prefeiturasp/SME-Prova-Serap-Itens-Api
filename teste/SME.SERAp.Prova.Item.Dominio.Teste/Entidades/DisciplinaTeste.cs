using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class DisciplinaTeste
    {
        [Fact]
        public void Deve_Criar_Disciplina_Com_Construtor_Padrao()
        {
            var disciplina = new Disciplina
            {
                LegadoId = 100,
                Descricao = "Matemática",
                NivelEnsino = "Fundamental II",
                Status = 1,
                Codigo = 10,
                AreaConhecimentoId = 5,
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20)
            };

            Assert.Equal(100, disciplina.LegadoId);
            Assert.Equal("Matemática", disciplina.Descricao);
            Assert.Equal("Fundamental II", disciplina.NivelEnsino);
            Assert.Equal(1, disciplina.Status);
            Assert.Equal(10, disciplina.Codigo);
            Assert.Equal(5, disciplina.AreaConhecimentoId);
            Assert.Equal(new DateTime(2024, 1, 15), disciplina.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), disciplina.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;

            var disciplina = new Disciplina(null, 200, 10, "Língua Portuguesa", "Ensino Médio", StatusGeral.Ativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(200, disciplina.LegadoId);
            Assert.Equal(10, disciplina.AreaConhecimentoId);
            Assert.Equal("Língua Portuguesa", disciplina.Descricao);
            Assert.Equal("Ensino Médio", disciplina.NivelEnsino);
            Assert.Equal((int)StatusGeral.Ativo, disciplina.Status);
            Assert.InRange(disciplina.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(disciplina.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(disciplina.CriadoEm, disciplina.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var disciplina = new Disciplina(50, 300, 15, "História", "Fundamental I", StatusGeral.Inativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(50, disciplina.Id);
            Assert.Equal(300, disciplina.LegadoId);
            Assert.Equal(15, disciplina.AreaConhecimentoId);
            Assert.Equal("História", disciplina.Descricao);
            Assert.Equal("Fundamental I", disciplina.NivelEnsino);
            Assert.Equal((int)StatusGeral.Inativo, disciplina.Status);
            Assert.InRange(disciplina.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var disciplina = new Disciplina(75, 400, 20, "Geografia", "Ensino Médio", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), disciplina.CriadoEm);
            Assert.NotEqual(default(DateTime), disciplina.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Status_Ativo()
        {
            var disciplina = new Disciplina(null, 500, 25, "Ciências", "Fundamental II", StatusGeral.Ativo);

            Assert.Equal((int)StatusGeral.Ativo, disciplina.Status);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Status_Inativo()
        {
            var disciplina = new Disciplina(99, 600, 30, "Física", "Ensino Médio", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, disciplina.Status);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Diferentes_Niveis_Ensino()
        {
            var disciplina1 = new Disciplina(null, 100, 5, "Arte", "Fundamental I", StatusGeral.Ativo);
            var disciplina2 = new Disciplina(null, 200, 10, "Química", "Fundamental II", StatusGeral.Ativo);
            var disciplina3 = new Disciplina(null, 300, 15, "Biologia", "Ensino Médio", StatusGeral.Ativo);

            Assert.Equal("Fundamental I", disciplina1.NivelEnsino);
            Assert.Equal("Fundamental II", disciplina2.NivelEnsino);
            Assert.Equal("Ensino Médio", disciplina3.NivelEnsino);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Diferentes_Areas_Conhecimento()
        {
            var disciplina1 = new Disciplina(null, 700, 1, "Matemática", "Ensino Médio", StatusGeral.Ativo);
            var disciplina2 = new Disciplina(null, 800, 2, "Português", "Ensino Médio", StatusGeral.Ativo);
            var disciplina3 = new Disciplina(null, 900, 3, "Inglês", "Ensino Médio", StatusGeral.Ativo);

            Assert.Equal(1, disciplina1.AreaConhecimentoId);
            Assert.Equal(2, disciplina2.AreaConhecimentoId);
            Assert.Equal(3, disciplina3.AreaConhecimentoId);
        }

        [Fact]
        public void Deve_Criar_Disciplina_Com_Mesma_Area_Conhecimento_Niveis_Diferentes()
        {
            var disciplina1 = new Disciplina(null, 1000, 5, "Matemática", "Fundamental I", StatusGeral.Ativo);
            var disciplina2 = new Disciplina(null, 1100, 5, "Matemática", "Fundamental II", StatusGeral.Ativo);
            var disciplina3 = new Disciplina(null, 1200, 5, "Matemática", "Ensino Médio", StatusGeral.Ativo);

            Assert.Equal(5, disciplina1.AreaConhecimentoId);
            Assert.Equal(5, disciplina2.AreaConhecimentoId);
            Assert.Equal(5, disciplina3.AreaConhecimentoId);
            Assert.Equal("Fundamental I", disciplina1.NivelEnsino);
            Assert.Equal("Fundamental II", disciplina2.NivelEnsino);
            Assert.Equal("Ensino Médio", disciplina3.NivelEnsino);
        }
    }
}
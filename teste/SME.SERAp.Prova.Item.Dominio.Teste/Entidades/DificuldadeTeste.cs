using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class DificuldadeTeste
    {
        [Fact]
        public void Deve_Criar_Dificuldade_Com_Construtor_Padrao()
        {
            var dificuldade = new Dificuldade
            {
                LegadoId = 100,
                Descricao = "Fácil",
                Ordem = 1,
                Status = 1,
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20)
            };

            Assert.Equal(100, dificuldade.LegadoId);
            Assert.Equal("Fácil", dificuldade.Descricao);
            Assert.Equal(1, dificuldade.Ordem);
            Assert.Equal(1, dificuldade.Status);
            Assert.Equal(new DateTime(2024, 1, 15), dificuldade.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), dificuldade.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;

            var dificuldade = new Dificuldade(null, 200, "Médio", 2, StatusGeral.Ativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(200, dificuldade.LegadoId);
            Assert.Equal("Médio", dificuldade.Descricao);
            Assert.Equal(2, dificuldade.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, dificuldade.Status);
            Assert.InRange(dificuldade.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(dificuldade.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(dificuldade.CriadoEm, dificuldade.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var dificuldade = new Dificuldade(50, 300, "Difícil", 3, StatusGeral.Inativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(50, dificuldade.Id);
            Assert.Equal(300, dificuldade.LegadoId);
            Assert.Equal("Difícil", dificuldade.Descricao);
            Assert.Equal(3, dificuldade.Ordem);
            Assert.Equal((int)StatusGeral.Inativo, dificuldade.Status);
            Assert.InRange(dificuldade.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var dificuldade = new Dificuldade(75, 400, "Muito Difícil", 4, StatusGeral.Ativo);

            Assert.Equal(default(DateTime), dificuldade.CriadoEm);
            Assert.NotEqual(default(DateTime), dificuldade.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Status_Ativo()
        {
            var dificuldade = new Dificuldade(null, 500, "Básico", 1, StatusGeral.Ativo);

            Assert.Equal((int)StatusGeral.Ativo, dificuldade.Status);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Status_Inativo()
        {
            var dificuldade = new Dificuldade(99, 600, "Avançado", 5, StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, dificuldade.Status);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Diferentes_Ordens()
        {
            var dificuldade1 = new Dificuldade(null, 100, "Nível 1", 1, StatusGeral.Ativo);
            var dificuldade2 = new Dificuldade(null, 200, "Nível 2", 2, StatusGeral.Ativo);
            var dificuldade3 = new Dificuldade(null, 300, "Nível 3", 3, StatusGeral.Ativo);
            var dificuldade4 = new Dificuldade(null, 400, "Nível 4", 4, StatusGeral.Ativo);

            Assert.Equal(1, dificuldade1.Ordem);
            Assert.Equal(2, dificuldade2.Ordem);
            Assert.Equal(3, dificuldade3.Ordem);
            Assert.Equal(4, dificuldade4.Ordem);
        }

        [Fact]
        public void Deve_Criar_Dificuldade_Com_Diferentes_Descricoes()
        {
            var dificuldade1 = new Dificuldade(null, 700, "Iniciante", 1, StatusGeral.Ativo);
            var dificuldade2 = new Dificuldade(null, 800, "Intermediário", 2, StatusGeral.Ativo);
            var dificuldade3 = new Dificuldade(null, 900, "Especialista", 3, StatusGeral.Ativo);

            Assert.Equal("Iniciante", dificuldade1.Descricao);
            Assert.Equal("Intermediário", dificuldade2.Descricao);
            Assert.Equal("Especialista", dificuldade3.Descricao);
        }
    }
}
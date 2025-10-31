using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class AssuntoTeste
    {
        [Fact]
        public void Deve_Criar_Assunto_Com_Construtor_Padrao()
        {
            var assunto = new Assunto
            {
                LegadoId = 100,
                DisciplinaId = 50,
                Descricao = "Álgebra",
                Status = 1,
                CriadoEm = new DateTime(2024, 1, 15),
                AlteradoEm = new DateTime(2024, 1, 20)
            };

            Assert.Equal(100, assunto.LegadoId);
            Assert.Equal(50, assunto.DisciplinaId);
            Assert.Equal("Álgebra", assunto.Descricao);
            Assert.Equal(1, assunto.Status);
            Assert.Equal(new DateTime(2024, 1, 15), assunto.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 20), assunto.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Assunto_Com_Construtor_Parametros_Id_Nulo()
        {
            var dataAntes = DateTime.Now;

            var assunto = new Assunto(null, 200, 75, "Geometria", StatusGeral.Ativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(200, assunto.LegadoId);
            Assert.Equal(75, assunto.DisciplinaId);
            Assert.Equal("Geometria", assunto.Descricao);
            Assert.Equal((int)StatusGeral.Ativo, assunto.Status);
            Assert.InRange(assunto.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(assunto.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(assunto.CriadoEm, assunto.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Assunto_Com_Construtor_Parametros_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var assunto = new Assunto(50, 300, 100, "Fonética", StatusGeral.Inativo);

            var dataDepois = DateTime.Now;

            Assert.Equal(50, assunto.Id);
            Assert.Equal(300, assunto.LegadoId);
            Assert.Equal(100, assunto.DisciplinaId);
            Assert.Equal("Fonética", assunto.Descricao);
            Assert.Equal((int)StatusGeral.Inativo, assunto.Status);
            Assert.InRange(assunto.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Atualizar_Apenas_AlteradoEm_Quando_Id_Informado()
        {
            var assunto = new Assunto(75, 400, 125, "Sintaxe", StatusGeral.Ativo);

            Assert.Equal(default(DateTime), assunto.CriadoEm);
            Assert.NotEqual(default(DateTime), assunto.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Assunto_Com_Status_Ativo()
        {
            var assunto = new Assunto(null, 500, 150, "Biologia Celular", StatusGeral.Ativo);

            Assert.Equal((int)StatusGeral.Ativo, assunto.Status);
        }

        [Fact]
        public void Deve_Criar_Assunto_Com_Status_Inativo()
        {
            var assunto = new Assunto(99, 600, 175, "Física Quântica", StatusGeral.Inativo);

            Assert.Equal((int)StatusGeral.Inativo, assunto.Status);
        }
    }
}
using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class NivelItemTeste
    {
        [Fact]
        public void Deve_Criar_NivelItem_Com_Construtor_Padrao()
        {
            var nivelItem = new NivelItem
            {
                Descricao = "Nível Básico",
                Ordem = 1,
                Status = 1,
                CriadoEm = new DateTime(2024, 1, 1),
                AlteradoEm = new DateTime(2024, 1, 15)
            };

            Assert.Equal("Nível Básico", nivelItem.Descricao);
            Assert.Equal(1, nivelItem.Ordem);
            Assert.Equal(1, nivelItem.Status);
            Assert.Equal(new DateTime(2024, 1, 1), nivelItem.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 15), nivelItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_NivelItem_Com_Construtor_Parametros_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var nivelItem = new NivelItem(
                id: null,
                descricao: "Nível Intermediário",
                ordem: 2,
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal("Nível Intermediário", nivelItem.Descricao);
            Assert.Equal(2, nivelItem.Ordem);
            Assert.Equal((int)StatusGeral.Ativo, nivelItem.Status);
            Assert.InRange(nivelItem.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(nivelItem.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(nivelItem.CriadoEm, nivelItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_NivelItem_Com_Construtor_Parametros_Quando_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var nivelItem = new NivelItem(
                id: 10,
                descricao: "Nível Avançado",
                ordem: 3,
                status: StatusGeral.Inativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(10, nivelItem.Id);
            Assert.Equal("Nível Avançado", nivelItem.Descricao);
            Assert.Equal(3, nivelItem.Ordem);
            Assert.Equal((int)StatusGeral.Inativo, nivelItem.Status);
            Assert.InRange(nivelItem.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Independente_De_Id_Null()
        {
            var nivelItemAtivo = new NivelItem(
                id: null,
                descricao: "Nível Fácil",
                ordem: 1,
                status: StatusGeral.Ativo
            );

            var nivelItemInativo = new NivelItem(
                id: null,
                descricao: "Nível Difícil",
                ordem: 4,
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, nivelItemAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, nivelItemInativo.Status);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Quando_Id_Informado()
        {
            var nivelItemAtivo = new NivelItem(
                id: 20,
                descricao: "Nível Médio",
                ordem: 2,
                status: StatusGeral.Ativo
            );

            var nivelItemInativo = new NivelItem(
                id: 21,
                descricao: "Nível Superior",
                ordem: 5,
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, nivelItemAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, nivelItemInativo.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Somente_Quando_Id_Null()
        {
            var nivelItemNovo = new NivelItem(
                id: null,
                descricao: "Novo Nível",
                ordem: 1,
                status: StatusGeral.Ativo
            );

            var nivelItemExistente = new NivelItem(
                id: 30,
                descricao: "Nível Existente",
                ordem: 2,
                status: StatusGeral.Ativo
            );

            Assert.NotEqual(default(DateTime), nivelItemNovo.CriadoEm);
            Assert.Equal(default(DateTime), nivelItemExistente.CriadoEm);
        }

        [Fact]
        public void Deve_Sempre_Definir_AlteradoEm_No_Construtor_Parametros()
        {
            var dataAntes = DateTime.Now;

            var nivelItemNovo = new NivelItem(
                id: null,
                descricao: "Nível Teste 1",
                ordem: 1,
                status: StatusGeral.Ativo
            );

            var nivelItemExistente = new NivelItem(
                id: 40,
                descricao: "Nível Teste 2",
                ordem: 2,
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.InRange(nivelItemNovo.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(nivelItemExistente.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia_Ou_Nula()
        {
            var nivelItemDescricaoVazia = new NivelItem(
                id: null,
                descricao: string.Empty,
                ordem: 1,
                status: StatusGeral.Ativo
            );

            var nivelItemDescricaoNula = new NivelItem(
                id: null,
                descricao: null,
                ordem: 2,
                status: StatusGeral.Ativo
            );

            Assert.Equal(string.Empty, nivelItemDescricaoVazia.Descricao);
            Assert.Null(nivelItemDescricaoNula.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Ordem_Zero()
        {
            var nivelItem = new NivelItem(
                id: null,
                descricao: "Nível Zero",
                ordem: 0,
                status: StatusGeral.Ativo
            );

            Assert.Equal(0, nivelItem.Ordem);
        }

        [Fact]
        public void Deve_Aceitar_Ordem_Negativa()
        {
            var nivelItem = new NivelItem(
                id: null,
                descricao: "Nível Negativo",
                ordem: -1,
                status: StatusGeral.Ativo
            );

            Assert.Equal(-1, nivelItem.Ordem);
        }

        [Fact]
        public void Deve_Aceitar_Ordem_Positiva_Grande()
        {
            var nivelItem = new NivelItem(
                id: null,
                descricao: "Nível Alto",
                ordem: 999999,
                status: StatusGeral.Ativo
            );

            Assert.Equal(999999, nivelItem.Ordem);
        }

        [Fact]
        public void Deve_Permitir_Mesma_Ordem_Para_Diferentes_Niveis()
        {
            var nivelItem1 = new NivelItem(
                id: null,
                descricao: "Primeiro Nível",
                ordem: 1,
                status: StatusGeral.Ativo
            );

            var nivelItem2 = new NivelItem(
                id: null,
                descricao: "Segundo Nível",
                ordem: 1,
                status: StatusGeral.Ativo
            );

            Assert.Equal(1, nivelItem1.Ordem);
            Assert.Equal(1, nivelItem2.Ordem);
        }

        [Fact]
        public void Deve_Criar_NivelItem_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('A', 1000);

            var nivelItem = new NivelItem(
                id: null,
                descricao: descricaoLonga,
                ordem: 1,
                status: StatusGeral.Ativo
            );

            Assert.Equal(descricaoLonga, nivelItem.Descricao);
            Assert.Equal(1000, nivelItem.Descricao.Length);
        }

        [Fact]
        public void Deve_Criar_NivelItem_Com_Caracteres_Especiais_Na_Descricao()
        {
            var descricaoEspecial = "Nível @#$%¨&*()_+-=[]{}|;':\"<>,.?/\\~`";

            var nivelItem = new NivelItem(
                id: null,
                descricao: descricaoEspecial,
                ordem: 1,
                status: StatusGeral.Ativo
            );

            Assert.Equal(descricaoEspecial, nivelItem.Descricao);
        }
    }
}
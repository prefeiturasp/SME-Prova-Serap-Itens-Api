using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class QuantidadeAlternativasTeste
    {
        [Fact]
        public void Deve_Criar_QuantidadeAlternativas_Com_Construtor_Padrao()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas
            {
                LegadoId = 100,
                Descricao = "Quatro alternativas",
                EhPadrao = true,
                QtdAlternativas = 4,
                Status = 1,
                Codigo = 1001,
                CriadoEm = new DateTime(2024, 1, 1),
                AlteradoEm = new DateTime(2024, 1, 15)
            };

            Assert.Equal(100, quantidadeAlternativas.LegadoId);
            Assert.Equal("Quatro alternativas", quantidadeAlternativas.Descricao);
            Assert.True(quantidadeAlternativas.EhPadrao);
            Assert.Equal(4, quantidadeAlternativas.QtdAlternativas);
            Assert.Equal(1, quantidadeAlternativas.Status);
            Assert.Equal(1001, quantidadeAlternativas.Codigo);
            Assert.Equal(new DateTime(2024, 1, 1), quantidadeAlternativas.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 15), quantidadeAlternativas.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativas_Com_Construtor_Parametros_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 200,
                descricao: "Cinco alternativas",
                ehPadrao: true,
                qtdAlternativas: 5,
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(200, quantidadeAlternativas.LegadoId);
            Assert.Equal("Cinco alternativas", quantidadeAlternativas.Descricao);
            Assert.True(quantidadeAlternativas.EhPadrao);
            Assert.Equal(5, quantidadeAlternativas.QtdAlternativas);
            Assert.Equal((int)StatusGeral.Ativo, quantidadeAlternativas.Status);
            Assert.InRange(quantidadeAlternativas.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(quantidadeAlternativas.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(quantidadeAlternativas.CriadoEm, quantidadeAlternativas.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativas_Com_Construtor_Parametros_Quando_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: 10,
                legadoId: 300,
                descricao: "Três alternativas",
                ehPadrao: false,
                qtdAlternativas: 3,
                status: StatusGeral.Inativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(10, quantidadeAlternativas.Id);
            Assert.Equal(300, quantidadeAlternativas.LegadoId);
            Assert.Equal("Três alternativas", quantidadeAlternativas.Descricao);
            Assert.False(quantidadeAlternativas.EhPadrao);
            Assert.Equal(3, quantidadeAlternativas.QtdAlternativas);
            Assert.Equal((int)StatusGeral.Inativo, quantidadeAlternativas.Status);
            Assert.InRange(quantidadeAlternativas.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Independente_De_Id_Null()
        {
            var quantidadeAlternativasAtivo = new QuantidadeAlternativas(
                id: null,
                legadoId: 400,
                descricao: "Duas alternativas",
                ehPadrao: true,
                qtdAlternativas: 2,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasInativo = new QuantidadeAlternativas(
                id: null,
                legadoId: 450,
                descricao: "Seis alternativas",
                ehPadrao: false,
                qtdAlternativas: 6,
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, quantidadeAlternativasAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, quantidadeAlternativasInativo.Status);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Quando_Id_Informado()
        {
            var quantidadeAlternativasAtivo = new QuantidadeAlternativas(
                id: 20,
                legadoId: 500,
                descricao: "Quatro alternativas",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasInativo = new QuantidadeAlternativas(
                id: 21,
                legadoId: 550,
                descricao: "Cinco alternativas",
                ehPadrao: false,
                qtdAlternativas: 5,
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, quantidadeAlternativasAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, quantidadeAlternativasInativo.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Somente_Quando_Id_Null()
        {
            var quantidadeAlternativasNova = new QuantidadeAlternativas(
                id: null,
                legadoId: 600,
                descricao: "Nova quantidade",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasExistente = new QuantidadeAlternativas(
                id: 30,
                legadoId: 700,
                descricao: "Quantidade existente",
                ehPadrao: false,
                qtdAlternativas: 3,
                status: StatusGeral.Ativo
            );

            Assert.NotEqual(default(DateTime), quantidadeAlternativasNova.CriadoEm);
            Assert.Equal(default(DateTime), quantidadeAlternativasExistente.CriadoEm);
        }

        [Fact]
        public void Deve_Sempre_Definir_AlteradoEm_No_Construtor_Parametros()
        {
            var dataAntes = DateTime.Now;

            var quantidadeAlternativasNova = new QuantidadeAlternativas(
                id: null,
                legadoId: 800,
                descricao: "Teste 1",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasExistente = new QuantidadeAlternativas(
                id: 40,
                legadoId: 900,
                descricao: "Teste 2",
                ehPadrao: false,
                qtdAlternativas: 5,
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.InRange(quantidadeAlternativasNova.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(quantidadeAlternativasExistente.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia_Ou_Nula()
        {
            var quantidadeAlternativasDescricaoVazia = new QuantidadeAlternativas(
                id: null,
                legadoId: 1000,
                descricao: string.Empty,
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasDescricaoNula = new QuantidadeAlternativas(
                id: null,
                legadoId: 1100,
                descricao: null,
                ehPadrao: false,
                qtdAlternativas: 3,
                status: StatusGeral.Ativo
            );

            Assert.Equal(string.Empty, quantidadeAlternativasDescricaoVazia.Descricao);
            Assert.Null(quantidadeAlternativasDescricaoNula.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_EhPadrao_True()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1200,
                descricao: "Padrão verdadeiro",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            Assert.True(quantidadeAlternativas.EhPadrao);
        }

        [Fact]
        public void Deve_Aceitar_EhPadrao_False()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1300,
                descricao: "Padrão falso",
                ehPadrao: false,
                qtdAlternativas: 5,
                status: StatusGeral.Ativo
            );

            Assert.False(quantidadeAlternativas.EhPadrao);
        }

        [Fact]
        public void Deve_Aceitar_QtdAlternativas_Zero()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1400,
                descricao: "Zero alternativas",
                ehPadrao: false,
                qtdAlternativas: 0,
                status: StatusGeral.Ativo
            );

            Assert.Equal(0, quantidadeAlternativas.QtdAlternativas);
        }

        [Fact]
        public void Deve_Aceitar_QtdAlternativas_Negativa()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1500,
                descricao: "Alternativas negativas",
                ehPadrao: false,
                qtdAlternativas: -1,
                status: StatusGeral.Ativo
            );

            Assert.Equal(-1, quantidadeAlternativas.QtdAlternativas);
        }

        [Fact]
        public void Deve_Aceitar_QtdAlternativas_Positiva_Grande()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1600,
                descricao: "Muitas alternativas",
                ehPadrao: false,
                qtdAlternativas: 999999,
                status: StatusGeral.Ativo
            );

            Assert.Equal(999999, quantidadeAlternativas.QtdAlternativas);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero_Ou_Negativo()
        {
            var quantidadeAlternativasLegadoZero = new QuantidadeAlternativas(
                id: null,
                legadoId: 0,
                descricao: "Legado zero",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativasLegadoNegativo = new QuantidadeAlternativas(
                id: null,
                legadoId: -100,
                descricao: "Legado negativo",
                ehPadrao: false,
                qtdAlternativas: 5,
                status: StatusGeral.Ativo
            );

            Assert.Equal(0, quantidadeAlternativasLegadoZero.LegadoId);
            Assert.Equal(-100, quantidadeAlternativasLegadoNegativo.LegadoId);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativas_Com_Descricao_Longa()
        {
            var descricaoLonga = new string('A', 1000);

            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1700,
                descricao: descricaoLonga,
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            Assert.Equal(descricaoLonga, quantidadeAlternativas.Descricao);
            Assert.Equal(1000, quantidadeAlternativas.Descricao.Length);
        }

        [Fact]
        public void Deve_Criar_QuantidadeAlternativas_Com_Caracteres_Especiais_Na_Descricao()
        {
            var descricaoEspecial = "Alternativas @#$%¨&*()_+-=[]{}|;':\"<>,.?/\\~`";

            var quantidadeAlternativas = new QuantidadeAlternativas(
                id: null,
                legadoId: 1800,
                descricao: descricaoEspecial,
                ehPadrao: false,
                qtdAlternativas: 3,
                status: StatusGeral.Ativo
            );

            Assert.Equal(descricaoEspecial, quantidadeAlternativas.Descricao);
        }

        [Fact]
        public void Deve_Permitir_Codigo_Zero_No_Construtor_Padrao()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas
            {
                Codigo = 0
            };

            Assert.Equal(0, quantidadeAlternativas.Codigo);
        }

        [Fact]
        public void Deve_Permitir_Codigo_Negativo_No_Construtor_Padrao()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas
            {
                Codigo = -500
            };

            Assert.Equal(-500, quantidadeAlternativas.Codigo);
        }

        [Fact]
        public void Deve_Permitir_Codigo_Positivo_Grande_No_Construtor_Padrao()
        {
            var quantidadeAlternativas = new QuantidadeAlternativas
            {
                Codigo = 999999999
            };

            Assert.Equal(999999999, quantidadeAlternativas.Codigo);
        }

        [Fact]
        public void Deve_Criar_Multiplas_QuantidadeAlternativas_Com_Mesmo_EhPadrao_True()
        {
            var quantidadeAlternativas1 = new QuantidadeAlternativas(
                id: null,
                legadoId: 1900,
                descricao: "Primeira padrão",
                ehPadrao: true,
                qtdAlternativas: 4,
                status: StatusGeral.Ativo
            );

            var quantidadeAlternativas2 = new QuantidadeAlternativas(
                id: null,
                legadoId: 2000,
                descricao: "Segunda padrão",
                ehPadrao: true,
                qtdAlternativas: 5,
                status: StatusGeral.Ativo
            );

            Assert.True(quantidadeAlternativas1.EhPadrao);
            Assert.True(quantidadeAlternativas2.EhPadrao);
        }
    }
}
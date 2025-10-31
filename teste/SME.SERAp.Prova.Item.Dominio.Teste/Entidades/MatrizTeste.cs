using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class MatrizTeste
    {
        [Fact]
        public void Deve_Criar_Matriz_Com_Construtor_Padrao()
        {
            var matriz = new Matriz
            {
                LegadoId = 100,
                Descricao = "Matriz de Matemática",
                Modelo = "Modelo A",
                DisciplinaId = 1,
                Status = 1,
                CriadoEm = new DateTime(2024, 1, 1),
                AlteradoEm = new DateTime(2024, 1, 15)
            };

            Assert.Equal(100, matriz.LegadoId);
            Assert.Equal("Matriz de Matemática", matriz.Descricao);
            Assert.Equal("Modelo A", matriz.Modelo);
            Assert.Equal(1, matriz.DisciplinaId);
            Assert.Equal(1, matriz.Status);
            Assert.Equal(new DateTime(2024, 1, 1), matriz.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 15), matriz.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Matriz_Com_Construtor_Parametros_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var matriz = new Matriz(
                id: null,
                legadoId: 200,
                disciplinaId: 2,
                descricao: "Matriz de Português",
                modelo: "Modelo B",
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(200, matriz.LegadoId);
            Assert.Equal("Matriz de Português", matriz.Descricao);
            Assert.Equal("Modelo B", matriz.Modelo);
            Assert.Equal(2, matriz.DisciplinaId);
            Assert.Equal((int)StatusGeral.Ativo, matriz.Status);
            Assert.InRange(matriz.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(matriz.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(matriz.CriadoEm, matriz.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_Matriz_Com_Construtor_Parametros_Quando_Id_Informado()
        {
            var dataAntes = DateTime.Now;

            var matriz = new Matriz(
                id: 10,
                legadoId: 300,
                disciplinaId: 3,
                descricao: "Matriz de Ciências",
                modelo: "Modelo C",
                status: StatusGeral.Inativo
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(10, matriz.Id);
            Assert.Equal(300, matriz.LegadoId);
            Assert.Equal("Matriz de Ciências", matriz.Descricao);
            Assert.Equal("Modelo C", matriz.Modelo);
            Assert.Equal(3, matriz.DisciplinaId);
            Assert.Equal((int)StatusGeral.Inativo, matriz.Status);
            Assert.InRange(matriz.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Independente_De_Id_Null()
        {
            var matrizAtivo = new Matriz(
                id: null,
                legadoId: 400,
                disciplinaId: 4,
                descricao: "Matriz de História",
                modelo: "Modelo D",
                status: StatusGeral.Ativo
            );

            var matrizInativo = new Matriz(
                id: null,
                legadoId: 450,
                disciplinaId: 4,
                descricao: "Matriz de Geografia",
                modelo: "Modelo D2",
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, matrizAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, matrizInativo.Status);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Quando_Id_Informado()
        {
            var matrizAtivo = new Matriz(
                id: 20,
                legadoId: 500,
                disciplinaId: 5,
                descricao: "Matriz de Ciências",
                modelo: "Modelo E",
                status: StatusGeral.Ativo
            );

            var matrizInativo = new Matriz(
                id: 21,
                legadoId: 550,
                disciplinaId: 5,
                descricao: "Matriz de Artes",
                modelo: "Modelo E2",
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Ativo, matrizAtivo.Status);
            Assert.Equal((int)StatusGeral.Inativo, matrizInativo.Status);
        }

        [Fact]
        public void Deve_Respeitar_Status_Informado_Quando_Id_Informado_No_Construtor_Parametros()
        {
            var matriz = new Matriz(
                id: 20,
                legadoId: 500,
                disciplinaId: 5,
                descricao: "Matriz de Geografia",
                modelo: "Modelo E",
                status: StatusGeral.Inativo
            );

            Assert.Equal((int)StatusGeral.Inativo, matriz.Status);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_Somente_Quando_Id_Null()
        {
            var matrizNova = new Matriz(
                id: null,
                legadoId: 600,
                disciplinaId: 6,
                descricao: "Matriz Nova",
                modelo: "Modelo F",
                status: StatusGeral.Ativo
            );

            var matrizExistente = new Matriz(
                id: 30,
                legadoId: 700,
                disciplinaId: 7,
                descricao: "Matriz Existente",
                modelo: "Modelo G",
                status: StatusGeral.Ativo
            );

            Assert.NotEqual(default(DateTime), matrizNova.CriadoEm);
            Assert.Equal(default(DateTime), matrizExistente.CriadoEm);
        }

        [Fact]
        public void Deve_Sempre_Definir_AlteradoEm_No_Construtor_Parametros()
        {
            var dataAntes = DateTime.Now;

            var matrizNova = new Matriz(
                id: null,
                legadoId: 800,
                disciplinaId: 8,
                descricao: "Matriz Teste 1",
                modelo: "Modelo H",
                status: StatusGeral.Ativo
            );

            var matrizExistente = new Matriz(
                id: 40,
                legadoId: 900,
                disciplinaId: 9,
                descricao: "Matriz Teste 2",
                modelo: "Modelo I",
                status: StatusGeral.Ativo
            );

            var dataDepois = DateTime.Now;

            Assert.InRange(matrizNova.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(matrizExistente.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia_Ou_Nula()
        {
            var matrizDescricaoVazia = new Matriz(
                id: null,
                legadoId: 1000,
                disciplinaId: 10,
                descricao: string.Empty,
                modelo: "Modelo J",
                status: StatusGeral.Ativo
            );

            var matrizDescricaoNula = new Matriz(
                id: null,
                legadoId: 1100,
                disciplinaId: 11,
                descricao: null,
                modelo: "Modelo K",
                status: StatusGeral.Ativo
            );

            Assert.Equal(string.Empty, matrizDescricaoVazia.Descricao);
            Assert.Null(matrizDescricaoNula.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Modelo_Vazio_Ou_Nulo()
        {
            var matrizModeloVazio = new Matriz(
                id: null,
                legadoId: 1200,
                disciplinaId: 12,
                descricao: "Descrição Teste",
                modelo: string.Empty,
                status: StatusGeral.Ativo
            );

            var matrizModeloNulo = new Matriz(
                id: null,
                legadoId: 1300,
                disciplinaId: 13,
                descricao: "Descrição Teste",
                modelo: null,
                status: StatusGeral.Ativo
            );

            Assert.Equal(string.Empty, matrizModeloVazio.Modelo);
            Assert.Null(matrizModeloNulo.Modelo);
        }

        [Fact]
        public void Deve_Aceitar_LegadoId_Zero_Ou_Negativo()
        {
            var matrizLegadoZero = new Matriz(
                id: null,
                legadoId: 0,
                disciplinaId: 14,
                descricao: "Descrição",
                modelo: "Modelo",
                status: StatusGeral.Ativo
            );

            var matrizLegadoNegativo = new Matriz(
                id: null,
                legadoId: -100,
                disciplinaId: 15,
                descricao: "Descrição",
                modelo: "Modelo",
                status: StatusGeral.Ativo
            );

            Assert.Equal(0, matrizLegadoZero.LegadoId);
            Assert.Equal(-100, matrizLegadoNegativo.LegadoId);
        }

        [Fact]
        public void Deve_Aceitar_DisciplinaId_Zero_Ou_Negativo()
        {
            var matrizDisciplinaZero = new Matriz(
                id: null,
                legadoId: 1400,
                disciplinaId: 0,
                descricao: "Descrição",
                modelo: "Modelo",
                status: StatusGeral.Ativo
            );

            var matrizDisciplinaNegativo = new Matriz(
                id: null,
                legadoId: 1500,
                disciplinaId: -50,
                descricao: "Descrição",
                modelo: "Modelo",
                status: StatusGeral.Ativo
            );

            Assert.Equal(0, matrizDisciplinaZero.DisciplinaId);
            Assert.Equal(-50, matrizDisciplinaNegativo.DisciplinaId);
        }
    }
}
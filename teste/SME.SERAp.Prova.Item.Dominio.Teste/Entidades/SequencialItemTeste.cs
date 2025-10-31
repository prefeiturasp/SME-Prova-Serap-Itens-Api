using System;
using SME.SERAp.Prova.Item.Dominio.Entities;
using Xunit;

namespace SME.SERAp.Prova.Item.Dominio.Teste.Entidades
{
    public class SequencialItemTeste
    {
        [Fact]
        public void Deve_Criar_SequencialItem_Com_Construtor_Padrao()
        {
            var sequencialItem = new SequencialItem
            {
                CodigoAreaConhecimento = 100,
                CodigoDisciplina = 200,
                Sequencial = 1,
                CriadoEm = new DateTime(2024, 1, 1),
                AlteradoEm = new DateTime(2024, 1, 15)
            };

            Assert.Equal(100, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(200, sequencialItem.CodigoDisciplina);
            Assert.Equal(1, sequencialItem.Sequencial);
            Assert.Equal(new DateTime(2024, 1, 1), sequencialItem.CriadoEm);
            Assert.Equal(new DateTime(2024, 1, 15), sequencialItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Construtor_Parametros_Quando_Id_Null()
        {
            var dataAntes = DateTime.Now;

            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(100, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(200, sequencialItem.CodigoDisciplina);
            Assert.Equal(1, sequencialItem.Sequencial);
            Assert.InRange(sequencialItem.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(sequencialItem.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(sequencialItem.CriadoEm, sequencialItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Construtor_Parametros_Quando_Id_Zero()
        {
            var dataAntes = DateTime.Now;

            var sequencialItem = new SequencialItem(
                id: 0,
                codigoAreaConhecimento: 150,
                codigoDisciplina: 250,
                sequencial: 2
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(150, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(250, sequencialItem.CodigoDisciplina);
            Assert.Equal(2, sequencialItem.Sequencial);
            Assert.InRange(sequencialItem.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(sequencialItem.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(sequencialItem.CriadoEm, sequencialItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Construtor_Parametros_Quando_Id_Negativo()
        {
            var dataAntes = DateTime.Now;

            var sequencialItem = new SequencialItem(
                id: -5,
                codigoAreaConhecimento: 175,
                codigoDisciplina: 275,
                sequencial: 3
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(175, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(275, sequencialItem.CodigoDisciplina);
            Assert.Equal(3, sequencialItem.Sequencial);
            Assert.InRange(sequencialItem.CriadoEm, dataAntes, dataDepois);
            Assert.InRange(sequencialItem.AlteradoEm, dataAntes, dataDepois);
            Assert.Equal(sequencialItem.CriadoEm, sequencialItem.AlteradoEm);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Construtor_Parametros_Quando_Id_Informado_Positivo()
        {
            var dataCriacao = new DateTime(2024, 1, 1);
            var dataAntes = DateTime.Now;

            var sequencialItem = new SequencialItem(
                id: 10,
                codigoAreaConhecimento: 300,
                codigoDisciplina: 400,
                sequencial: 5,
                criadoEm: dataCriacao
            );

            var dataDepois = DateTime.Now;

            Assert.Equal(10, sequencialItem.Id);
            Assert.Equal(300, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(400, sequencialItem.CodigoDisciplina);
            Assert.Equal(5, sequencialItem.Sequencial);
            Assert.Equal(dataCriacao, sequencialItem.CriadoEm);
            Assert.InRange(sequencialItem.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Sempre_Definir_AlteradoEm_No_Construtor_Parametros()
        {
            var dataAntes = DateTime.Now;

            var sequencialItemNovo = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 500,
                codigoDisciplina: 600,
                sequencial: 10
            );

            var dataCriacao = new DateTime(2024, 1, 1);
            var sequencialItemExistente = new SequencialItem(
                id: 20,
                codigoAreaConhecimento: 700,
                codigoDisciplina: 800,
                sequencial: 15,
                criadoEm: dataCriacao
            );

            var dataDepois = DateTime.Now;

            Assert.InRange(sequencialItemNovo.AlteradoEm, dataAntes, dataDepois);
            Assert.InRange(sequencialItemExistente.AlteradoEm, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Usar_CriadoEm_Informado_Quando_Id_Positivo()
        {
            var dataCriacaoEsperada = new DateTime(2023, 6, 15, 10, 30, 0);

            var sequencialItem = new SequencialItem(
                id: 25,
                codigoAreaConhecimento: 900,
                codigoDisciplina: 1000,
                sequencial: 20,
                criadoEm: dataCriacaoEsperada
            );

            Assert.Equal(dataCriacaoEsperada, sequencialItem.CriadoEm);
        }

        [Fact]
        public void Deve_Aceitar_CodigoAreaConhecimento_Zero()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 0,
                codigoDisciplina: 100,
                sequencial: 1
            );

            Assert.Equal(0, sequencialItem.CodigoAreaConhecimento);
        }

        [Fact]
        public void Deve_Aceitar_CodigoAreaConhecimento_Negativo()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: -100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            Assert.Equal(-100, sequencialItem.CodigoAreaConhecimento);
        }

        [Fact]
        public void Deve_Aceitar_CodigoAreaConhecimento_Positivo_Grande()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 999999999,
                codigoDisciplina: 300,
                sequencial: 1
            );

            Assert.Equal(999999999, sequencialItem.CodigoAreaConhecimento);
        }

        [Fact]
        public void Deve_Aceitar_CodigoDisciplina_Zero()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 0,
                sequencial: 1
            );

            Assert.Equal(0, sequencialItem.CodigoDisciplina);
        }

        [Fact]
        public void Deve_Aceitar_CodigoDisciplina_Negativo()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 200,
                codigoDisciplina: -200,
                sequencial: 1
            );

            Assert.Equal(-200, sequencialItem.CodigoDisciplina);
        }

        [Fact]
        public void Deve_Aceitar_CodigoDisciplina_Positivo_Grande()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 300,
                codigoDisciplina: 888888888,
                sequencial: 1
            );

            Assert.Equal(888888888, sequencialItem.CodigoDisciplina);
        }

        [Fact]
        public void Deve_Aceitar_Sequencial_Zero()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 0
            );

            Assert.Equal(0, sequencialItem.Sequencial);
        }

        [Fact]
        public void Deve_Aceitar_Sequencial_Negativo()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: -50
            );

            Assert.Equal(-50, sequencialItem.Sequencial);
        }

        [Fact]
        public void Deve_Aceitar_Sequencial_Positivo_Grande()
        {
            var sequencialItem = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 777777777
            );

            Assert.Equal(777777777, sequencialItem.Sequencial);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Id_Positivo_E_CriadoEm_Null()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var sequencialItem = new SequencialItem(
                    id: 30,
                    codigoAreaConhecimento: 400,
                    codigoDisciplina: 500,
                    sequencial: 25,
                    criadoEm: null
                );
            });
        }

        [Fact]
        public void Deve_Usar_CriadoEm_Informado_Quando_Id_Positivo_E_CriadoEm_Fornecido()
        {
            var dataCriacaoEsperada = new DateTime(2023, 6, 15, 10, 30, 0);

            var sequencialItem = new SequencialItem(
                id: 30,
                codigoAreaConhecimento: 400,
                codigoDisciplina: 500,
                sequencial: 25,
                criadoEm: dataCriacaoEsperada
            );

            Assert.Equal(30, sequencialItem.Id);
            Assert.Equal(dataCriacaoEsperada, sequencialItem.CriadoEm);
            Assert.NotEqual(default(DateTime), sequencialItem.CriadoEm);
        }

        [Fact]
        public void Deve_Permitir_Mesmos_Codigos_Em_Diferentes_Sequenciais()
        {
            var sequencialItem1 = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItem2 = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 2
            );

            Assert.Equal(100, sequencialItem1.CodigoAreaConhecimento);
            Assert.Equal(100, sequencialItem2.CodigoAreaConhecimento);
            Assert.Equal(200, sequencialItem1.CodigoDisciplina);
            Assert.Equal(200, sequencialItem2.CodigoDisciplina);
            Assert.NotEqual(sequencialItem1.Sequencial, sequencialItem2.Sequencial);
        }

        [Fact]
        public void Deve_Permitir_Mesmo_Sequencial_Com_Codigos_Diferentes()
        {
            var sequencialItem1 = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItem2 = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 300,
                codigoDisciplina: 400,
                sequencial: 1
            );

            Assert.Equal(1, sequencialItem1.Sequencial);
            Assert.Equal(1, sequencialItem2.Sequencial);
            Assert.NotEqual(sequencialItem1.CodigoAreaConhecimento, sequencialItem2.CodigoAreaConhecimento);
            Assert.NotEqual(sequencialItem1.CodigoDisciplina, sequencialItem2.CodigoDisciplina);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Todos_Valores_Zero()
        {
            var sequencialItem = new SequencialItem(
                id: 0,
                codigoAreaConhecimento: 0,
                codigoDisciplina: 0,
                sequencial: 0
            );

            Assert.Equal(0, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(0, sequencialItem.CodigoDisciplina);
            Assert.Equal(0, sequencialItem.Sequencial);
        }

        [Fact]
        public void Deve_Criar_SequencialItem_Com_Todos_Valores_Negativos()
        {
            var sequencialItem = new SequencialItem(
                id: -1,
                codigoAreaConhecimento: -100,
                codigoDisciplina: -200,
                sequencial: -300
            );

            Assert.Equal(-100, sequencialItem.CodigoAreaConhecimento);
            Assert.Equal(-200, sequencialItem.CodigoDisciplina);
            Assert.Equal(-300, sequencialItem.Sequencial);
        }

        [Fact]
        public void Deve_Definir_CriadoEm_E_AlteradoEm_Iguais_Quando_Id_Zero_Ou_Negativo_Ou_Null()
        {
            var sequencialItemNull = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItemZero = new SequencialItem(
                id: 0,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItemNegativo = new SequencialItem(
                id: -1,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            Assert.Equal(sequencialItemNull.CriadoEm, sequencialItemNull.AlteradoEm);
            Assert.Equal(sequencialItemZero.CriadoEm, sequencialItemZero.AlteradoEm);
            Assert.Equal(sequencialItemNegativo.CriadoEm, sequencialItemNegativo.AlteradoEm);
        }

        [Fact]
        public void Deve_Definir_Id_Quando_Id_Positivo()
        {
            var sequencialItem = new SequencialItem(
                id: 50,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1,
                criadoEm: DateTime.Now
            );

            Assert.Equal(50, sequencialItem.Id);
        }

        [Fact]
        public void Nao_Deve_Definir_Id_Quando_Id_Zero_Ou_Negativo_Ou_Null()
        {
            var sequencialItemNull = new SequencialItem(
                id: null,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItemZero = new SequencialItem(
                id: 0,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            var sequencialItemNegativo = new SequencialItem(
                id: -1,
                codigoAreaConhecimento: 100,
                codigoDisciplina: 200,
                sequencial: 1
            );

            Assert.Equal(0, sequencialItemNull.Id);
            Assert.Equal(0, sequencialItemZero.Id);
            Assert.Equal(0, sequencialItemNegativo.Id);
        }
    }
}
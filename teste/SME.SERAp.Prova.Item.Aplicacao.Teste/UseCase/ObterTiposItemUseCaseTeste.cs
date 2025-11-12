using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Enums;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterTiposItemUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterTiposItemUseCase useCase;

        public ObterTiposItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterTiposItemUseCase(mediatorMock.Object);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterTiposItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Com_Todos_TiposItem_Do_Enum()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var enumValues = Enum.GetValues(typeof(TipoItem));
            Assert.Equal(enumValues.Length, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Contains(resultadoLista, t => t.Valor == (int)TipoItem.Dicotômico && t.Descricao == "Dicotômico");
            Assert.Contains(resultadoLista, t => t.Valor == (int)TipoItem.Politômico && t.Descricao == "Politômico");
        }

        [Fact]
        public async Task Deve_Retornar_Tipo_Dicotomico_Com_Valor_Um()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var tipoDicotomico = resultado.FirstOrDefault(t => t.Descricao == "Dicotômico");

            Assert.NotNull(tipoDicotomico);
            Assert.Equal(1, tipoDicotomico.Valor);
            Assert.Equal("Dicotômico", tipoDicotomico.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Tipo_Politomico_Com_Valor_Dois()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var tipoPolitomico = resultado.FirstOrDefault(t => t.Descricao == "Politômico");

            Assert.NotNull(tipoPolitomico);
            Assert.Equal(2, tipoPolitomico.Valor);
            Assert.Equal("Politômico", tipoPolitomico.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Nao_Vazia()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.NotEmpty(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_Quantidade_Correta_De_Tipos()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task Deve_Retornar_SelectDto_Com_Valor_Do_Tipo_Long()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item =>
            {
                Assert.IsType<long>(item.Valor);
            });
        }

        [Fact]
        public async Task Deve_Retornar_SelectDto_Com_Descricao_Do_Tipo_String()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item =>
            {
                Assert.IsType<string>(item.Descricao);
                Assert.False(string.IsNullOrEmpty(item.Descricao));
            });
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Valores_Do_Enum_TipoItem()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var valoresEsperados = new[] { 1, 2 };
            var valoresRetornados = resultado.Select(t => (int)t.Valor).ToArray();

            Assert.Equal(valoresEsperados.Length, valoresRetornados.Length);
            Assert.All(valoresEsperados, valor => Assert.Contains(valor, valoresRetornados));
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Nomes_Do_Enum_TipoItem()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var nomesEsperados = new[] { "Dicotômico", "Politômico" };
            var nomesRetornados = resultado.Select(t => t.Descricao).ToArray();

            Assert.Equal(nomesEsperados.Length, nomesRetornados.Length);
            Assert.All(nomesEsperados, nome => Assert.Contains(nome, nomesRetornados));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Com_Objetos_SelectDto_Validos()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.All(resultado, item =>
            {
                Assert.NotNull(item);
                Assert.IsType<SelectDto>(item);
                Assert.True(item.Valor > 0);
                Assert.False(string.IsNullOrWhiteSpace(item.Descricao));
            });
        }

        [Fact]
        public async Task Nao_Deve_Chamar_Mediator_Durante_Execucao()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            mediatorMock.Verify(m => m.Send(It.IsAny<IRequest<object>>(), It.IsAny<System.Threading.CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Retornar_Nova_Lista_A_Cada_Execucao()
        {
            var resultado1 = await useCase.Executar();
            var resultado2 = await useCase.Executar();

            Assert.NotNull(resultado1);
            Assert.NotNull(resultado2);

            Assert.NotSame(resultado1, resultado2);
        }

        [Fact]
        public async Task Deve_Retornar_Mesma_Quantidade_De_Itens_Em_Multiplas_Execucoes()
        {
            var resultado1 = await useCase.Executar();
            var resultado2 = await useCase.Executar();
            var resultado3 = await useCase.Executar();

            Assert.Equal(resultado1.Count(), resultado2.Count());
            Assert.Equal(resultado2.Count(), resultado3.Count());
        }

        [Fact]
        public async Task Deve_Mapear_Corretamente_Valor_E_Nome_Para_Cada_Tipo()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var resultadoArray = resultado.ToArray();

            foreach (var item in resultadoArray)
            {
                var enumParsed = (TipoItem)item.Valor;
                var nomeEnum = Enum.GetName(typeof(TipoItem), enumParsed);

                Assert.Equal(nomeEnum, item.Descricao);
            }
        }

        [Fact]
        public async Task Deve_Retornar_Tipos_Com_Caracteres_Especiais_Corretamente()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var dicotomico = resultado.FirstOrDefault(t => t.Valor == 1);
            var politomico = resultado.FirstOrDefault(t => t.Valor == 2);

            Assert.NotNull(dicotomico);
            Assert.NotNull(politomico);

            Assert.Contains("ô", dicotomico.Descricao);
            Assert.Contains("ô", politomico.Descricao);
        }

        [Fact]
        public async Task Deve_Ter_Valores_Sequenciais_Iniciando_Em_Um()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var valores = resultado.Select(t => (int)t.Valor).OrderBy(v => v).ToList();

            Assert.Equal(1, valores[0]);
            Assert.Equal(2, valores[1]);
        }

        [Fact]
        public async Task Nao_Deve_Retornar_Valores_Duplicados()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var valores = resultado.Select(t => t.Valor).ToList();
            var valoresDistintos = valores.Distinct().ToList();

            Assert.Equal(valores.Count, valoresDistintos.Count);
        }

        [Fact]
        public async Task Nao_Deve_Retornar_Descricoes_Duplicadas()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var descricoes = resultado.Select(t => t.Descricao).ToList();
            var descricoesDistintas = descricoes.Distinct().ToList();

            Assert.Equal(descricoes.Count, descricoesDistintas.Count);
        }
    }
}
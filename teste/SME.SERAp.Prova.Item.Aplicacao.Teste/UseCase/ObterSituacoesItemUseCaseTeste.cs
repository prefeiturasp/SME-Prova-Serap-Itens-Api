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
    public class ObterSituacoesItemUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterSituacoesItemUseCase useCase;

        public ObterSituacoesItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterSituacoesItemUseCase(mediatorMock.Object);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterSituacoesItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Com_Todas_SituacoesItem_Do_Enum()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var enumValues = Enum.GetValues(typeof(SituacaoItem));
            Assert.Equal(enumValues.Length, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Contains(resultadoLista, s => s.Valor == (int)SituacaoItem.Inativo && s.Descricao == "Inativo");
            Assert.Contains(resultadoLista, s => s.Valor == (int)SituacaoItem.Ativo && s.Descricao == "Ativo");
            Assert.Contains(resultadoLista, s => s.Valor == (int)SituacaoItem.Pendente && s.Descricao == "Pendente");
            Assert.Contains(resultadoLista, s => s.Valor == (int)SituacaoItem.Rascunho && s.Descricao == "Rascunho");
        }

        [Fact]
        public async Task Deve_Retornar_Situacao_Inativo_Com_Valor_Zero()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var situacaoInativo = resultado.FirstOrDefault(s => s.Descricao == "Inativo");

            Assert.NotNull(situacaoInativo);
            Assert.Equal(0, situacaoInativo.Valor);
            Assert.Equal("Inativo", situacaoInativo.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Situacao_Ativo_Com_Valor_Um()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var situacaoAtivo = resultado.FirstOrDefault(s => s.Descricao == "Ativo");

            Assert.NotNull(situacaoAtivo);
            Assert.Equal(1, situacaoAtivo.Valor);
            Assert.Equal("Ativo", situacaoAtivo.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Situacao_Pendente_Com_Valor_Dois()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var situacaoPendente = resultado.FirstOrDefault(s => s.Descricao == "Pendente");

            Assert.NotNull(situacaoPendente);
            Assert.Equal(2, situacaoPendente.Valor);
            Assert.Equal("Pendente", situacaoPendente.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Situacao_Rascunho_Com_Valor_Tres()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var situacaoRascunho = resultado.FirstOrDefault(s => s.Descricao == "Rascunho");

            Assert.NotNull(situacaoRascunho);
            Assert.Equal(3, situacaoRascunho.Valor);
            Assert.Equal("Rascunho", situacaoRascunho.Descricao);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Nao_Vazia()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.NotEmpty(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_Quantidade_Correta_De_Situacoes()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.Count());
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
        public async Task Deve_Retornar_Todos_Valores_Do_Enum_SituacaoItem()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var valoresEsperados = new[] { 0, 1, 2, 3 };
            var valoresRetornados = resultado.Select(s => (int)s.Valor).ToArray();

            Assert.Equal(valoresEsperados.Length, valoresRetornados.Length);
            Assert.All(valoresEsperados, valor => Assert.Contains(valor, valoresRetornados));
        }

        [Fact]
        public async Task Deve_Retornar_Todos_Nomes_Do_Enum_SituacaoItem()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var nomesEsperados = new[] { "Inativo", "Ativo", "Pendente", "Rascunho" };
            var nomesRetornados = resultado.Select(s => s.Descricao).ToArray();

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
                Assert.True(item.Valor >= 0);
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
        public async Task Deve_Mapear_Corretamente_Valor_E_Nome_Para_Cada_Situacao()
        {
            var resultado = await useCase.Executar();

            Assert.NotNull(resultado);

            var resultadoArray = resultado.ToArray();

            foreach (var item in resultadoArray)
            {
                var enumParsed = (SituacaoItem)item.Valor;
                var nomeEnum = Enum.GetName(typeof(SituacaoItem), enumParsed);

                Assert.Equal(nomeEnum, item.Descricao);
            }
        }
    }
}
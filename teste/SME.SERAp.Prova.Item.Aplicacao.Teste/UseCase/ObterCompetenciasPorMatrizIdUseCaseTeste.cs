using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;
using SME.SERAp.Prova.Item.Aplicacao;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterCompetenciasPorMatrizIdUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterCompetenciasPorMatrizIdUseCase useCase;
        private const long MatrizIdValido = 50L;

        public ObterCompetenciasPorMatrizIdUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterCompetenciasPorMatrizIdUseCase(mediatorMock.Object);
        }

        private IEnumerable<RetornoCompetenciaDto> ObterCompetenciasMock()
        {
            return new List<RetornoCompetenciaDto>
            {
                new RetornoCompetenciaDto { Id = 1, Descricao = "Competência 1 - Leitura e Interpretação" },
                new RetornoCompetenciaDto { Id = 2, Descricao = "Competência 2 - Análise Crítica" },
                new RetornoCompetenciaDto { Id = 3, Descricao = "Competência 3 - Resolução de Problemas" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterCompetenciasPorMatrizIdUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Com_Dados_Completos_Em_Caso_De_Sucesso()
        {
            var competencias = ObterCompetenciasMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(competencias);

            var resultado = await useCase.Executar(MatrizIdValido);

            Assert.NotNull(resultado);
            Assert.Equal(competencias.Count(), resultado.Count());

            var resultadoLista = resultado.ToList();
            var competenciasLista = competencias.ToList();

            for (int i = 0; i < competenciasLista.Count; i++)
            {
                Assert.Equal(competenciasLista[i].Id, resultadoLista[i].Valor);
                Assert.Equal(competenciasLista[i].Descricao, resultadoLista[i].Descricao);
            }

            Assert.Contains(resultadoLista, s => s.Valor == 1 && s.Descricao == "Competência 1 - Leitura e Interpretação");
            Assert.Contains(resultadoLista, s => s.Valor == 2 && s.Descricao == "Competência 2 - Análise Crítica");
            Assert.Contains(resultadoLista, s => s.Valor == 3 && s.Descricao == "Competência 3 - Resolução de Problemas");

            mediatorMock.Verify(m => m.Send(It.Is<ObterCompetenciasPorMatrizIdQuery>(q => q.MatrizId == MatrizIdValido), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Existirem_Competencias_Para_MatrizId()
        {
            var competenciasVazias = Enumerable.Empty<RetornoCompetenciaDto>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(competenciasVazias);

            var resultado = await useCase.Executar(MatrizIdValido);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterCompetenciasPorMatrizIdQuery>(q => q.MatrizId == MatrizIdValido), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_SelectDto_Com_Valor_E_Descricao_Corretos_Para_Uma_Unica_Competencia()
        {
            var competenciaUnica = new List<RetornoCompetenciaDto>
            {
                new RetornoCompetenciaDto { Id = 10, Descricao = "Competência Única de Teste" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(competenciaUnica);

            var resultado = await useCase.Executar(MatrizIdValido);

            Assert.NotNull(resultado);
            Assert.Single(resultado);

            var primeiroItem = resultado.First();
            Assert.Equal(10, primeiroItem.Valor);
            Assert.Equal("Competência Única de Teste", primeiroItem.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Mediator_Com_MatrizId_Correto()
        {
            const long matrizIdEspecifico = 999L;
            var competencias = ObterCompetenciasMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(competencias);

            await useCase.Executar(matrizIdEspecifico);

            mediatorMock.Verify(m => m.Send(
                It.Is<ObterCompetenciasPorMatrizIdQuery>(q => q.MatrizId == matrizIdEspecifico),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Mapear_Corretamente_Multiplas_Competencias_Para_SelectDto()
        {
            var competencias = new List<RetornoCompetenciaDto>
            {
                new RetornoCompetenciaDto { Id = 100, Descricao = "Descrição 100" },
                new RetornoCompetenciaDto { Id = 200, Descricao = "Descrição 200" },
                new RetornoCompetenciaDto { Id = 300, Descricao = "Descrição 300" },
                new RetornoCompetenciaDto { Id = 400, Descricao = "Descrição 400" },
                new RetornoCompetenciaDto { Id = 500, Descricao = "Descrição 500" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterCompetenciasPorMatrizIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(competencias);

            var resultado = await useCase.Executar(MatrizIdValido);

            Assert.Equal(5, resultado.Count());

            var resultadoArray = resultado.ToArray();
            for (int i = 0; i < competencias.Count; i++)
            {
                Assert.Equal(competencias[i].Id, resultadoArray[i].Valor);
                Assert.Equal(competencias[i].Descricao, resultadoArray[i].Descricao);
            }
        }
    }
}
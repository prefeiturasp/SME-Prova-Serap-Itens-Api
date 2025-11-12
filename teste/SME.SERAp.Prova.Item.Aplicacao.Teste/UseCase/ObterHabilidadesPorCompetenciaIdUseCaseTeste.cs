using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;
using SME.SERAp.Prova.Item.Aplicacao;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterHabilidadesPorCompetenciaIdUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterHabilidadesPorCompetenciaIdUseCase useCase;
        private const long CompetenciaIdValido = 75L;

        public ObterHabilidadesPorCompetenciaIdUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterHabilidadesPorCompetenciaIdUseCase(mediatorMock.Object);
        }

        private IEnumerable<RetornoHabilidadeDto> ObterHabilidadesComCodigoMock()
        {
            return new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 1, Codigo = "H01", Descricao = "Identificar informações explícitas" },
                new RetornoHabilidadeDto { Id = 2, Codigo = "H02", Descricao = "Inferir informações implícitas" },
                new RetornoHabilidadeDto { Id = 3, Codigo = "H03", Descricao = "Analisar contexto histórico" }
            };
        }

        private IEnumerable<RetornoHabilidadeDto> ObterHabilidadesSemCodigoMock()
        {
            return new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 10, Codigo = null, Descricao = "Habilidade sem código 1" },
                new RetornoHabilidadeDto { Id = 11, Codigo = "", Descricao = "Habilidade sem código 2" },
                new RetornoHabilidadeDto { Id = 12, Codigo = string.Empty, Descricao = "Habilidade sem código 3" }
            };
        }

        private IEnumerable<RetornoHabilidadeDto> ObterHabilidadesMistasMock()
        {
            return new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 20, Codigo = "HAB01", Descricao = "Com código" },
                new RetornoHabilidadeDto { Id = 21, Codigo = null, Descricao = "Sem código" },
                new RetornoHabilidadeDto { Id = 22, Codigo = "", Descricao = "Código vazio" },
                new RetornoHabilidadeDto { Id = 23, Codigo = "HAB02", Descricao = "Outro com código" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterHabilidadesPorCompetenciaIdUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SelectDto_Com_Dados_Completos_Em_Caso_De_Sucesso()
        {
            var habilidades = ObterHabilidadesComCodigoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Equal(habilidades.Count(), resultado.Count());

            var resultadoLista = resultado.ToList();
            var habilidadesLista = habilidades.ToList();

            for (int i = 0; i < habilidadesLista.Count; i++)
            {
                Assert.Equal(habilidadesLista[i].Id, resultadoLista[i].Valor);
                Assert.Equal($"{habilidadesLista[i].Codigo} - {habilidadesLista[i].Descricao}", resultadoLista[i].Descricao);
            }

            mediatorMock.Verify(m => m.Send(It.Is<ObterHabilidadesPorCompetenciaIdQuery>(q => q.CompetenciaId == CompetenciaIdValido), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Formatar_Descricao_Com_Codigo_Quando_Codigo_Existir()
        {
            var habilidades = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 1, Codigo = "H01", Descricao = "Descrição teste" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Single(resultado);

            var primeiroItem = resultado.First();
            Assert.Equal(1, primeiroItem.Valor);
            Assert.Equal("H01 - Descrição teste", primeiroItem.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Apenas_Descricao_Quando_Codigo_For_Nulo_Ou_Vazio()
        {
            var habilidades = ObterHabilidadesSemCodigoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Equal("Habilidade sem código 1", resultadoLista[0].Descricao);
            Assert.Equal("Habilidade sem código 2", resultadoLista[1].Descricao);
            Assert.Equal("Habilidade sem código 3", resultadoLista[2].Descricao);

            Assert.DoesNotContain(resultadoLista, r => r.Descricao.Contains(" - "));

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Existirem_Habilidades_Para_CompetenciaId()
        {
            var habilidadesVazias = Enumerable.Empty<RetornoHabilidadeDto>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidadesVazias);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterHabilidadesPorCompetenciaIdQuery>(q => q.CompetenciaId == CompetenciaIdValido), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Tratar_Corretamente_Habilidades_Com_E_Sem_Codigo_Na_Mesma_Lista()
        {
            var habilidades = ObterHabilidadesMistasMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Equal(4, resultado.Count());

            var resultadoLista = resultado.ToList();

            Assert.Equal(20, resultadoLista[0].Valor);
            Assert.Equal("HAB01 - Com código", resultadoLista[0].Descricao);

            Assert.Equal(21, resultadoLista[1].Valor);
            Assert.Equal("Sem código", resultadoLista[1].Descricao);

            Assert.Equal(22, resultadoLista[2].Valor);
            Assert.Equal("Código vazio", resultadoLista[2].Descricao);

            Assert.Equal(23, resultadoLista[3].Valor);
            Assert.Equal("HAB02 - Outro com código", resultadoLista[3].Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Mediator_Com_CompetenciaId_Correto()
        {
            const long competenciaIdEspecifico = 888L;
            var habilidades = ObterHabilidadesComCodigoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            await useCase.Executar(competenciaIdEspecifico);

            mediatorMock.Verify(m => m.Send(
                It.Is<ObterHabilidadesPorCompetenciaIdQuery>(q => q.CompetenciaId == competenciaIdEspecifico),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Mapear_Corretamente_Uma_Unica_Habilidade()
        {
            var habilidadeUnica = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 99, Codigo = "UNICA", Descricao = "Habilidade única" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidadeUnica);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            Assert.NotNull(resultado);
            Assert.Single(resultado);

            var item = resultado.First();
            Assert.Equal(99, item.Valor);
            Assert.Equal("UNICA - Habilidade única", item.Descricao);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Preservar_Ordem_Das_Habilidades_Retornadas_Pela_Query()
        {
            var habilidades = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 5, Codigo = "H05", Descricao = "Quinta" },
                new RetornoHabilidadeDto { Id = 1, Codigo = "H01", Descricao = "Primeira" },
                new RetornoHabilidadeDto { Id = 3, Codigo = "H03", Descricao = "Terceira" },
                new RetornoHabilidadeDto { Id = 2, Codigo = "H02", Descricao = "Segunda" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(habilidades);

            var resultado = await useCase.Executar(CompetenciaIdValido);

            var resultadoArray = resultado.ToArray();

            Assert.Equal(5, resultadoArray[0].Valor);
            Assert.Equal(1, resultadoArray[1].Valor);
            Assert.Equal(3, resultadoArray[2].Valor);
            Assert.Equal(2, resultadoArray[3].Valor);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterHabilidadesPorCompetenciaIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
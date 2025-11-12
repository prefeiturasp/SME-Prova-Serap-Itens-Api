using MediatR;
using Moq;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class ObterAssuntosUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly ObterAssuntosUseCase useCase;
        private const long DisciplinaIdValida = 10L;

        public ObterAssuntosUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new ObterAssuntosUseCase(mediatorMock.Object);
        }

        private IEnumerable<Assunto> ObterAssuntosMock()
        {
            return new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = "Matemática Básica" },
                new Assunto { Id = 2, Descricao = "Álgebra" },
                new Assunto { Id = 3, Descricao = "Geometria" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterAssuntosUseCase(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_SelectDto_Com_Dados_Completos_Em_Caso_De_Sucesso()
        {
            var assuntos = ObterAssuntosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Equal(2, listaResultado[0].Valor);
            Assert.Equal("Álgebra", listaResultado[0].Descricao);
            Assert.Equal(3, listaResultado[1].Valor);
            Assert.Equal("Geometria", listaResultado[1].Descricao);
            Assert.Equal(1, listaResultado[2].Valor);
            Assert.Equal("Matemática Básica", listaResultado[2].Descricao);

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == DisciplinaIdValida),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Assuntos()
        {
            var assuntosVazios = Enumerable.Empty<Assunto>();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntosVazios);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == DisciplinaIdValida),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Query_Retornar_Null()
        {
            IEnumerable<Assunto> assuntosNull = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntosNull);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.Null(resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == DisciplinaIdValida),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Ordenar_Assuntos_Por_Descricao_Alfabeticamente()
        {
            var assuntosDesordenados = new List<Assunto>
            {
                new Assunto { Id = 5, Descricao = "Zoologia" },
                new Assunto { Id = 3, Descricao = "Biologia" },
                new Assunto { Id = 1, Descricao = "Anatomia" },
                new Assunto { Id = 4, Descricao = "Genética" },
                new Assunto { Id = 2, Descricao = "Ecologia" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntosDesordenados);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(5, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Equal("Anatomia", listaResultado[0].Descricao);
            Assert.Equal("Biologia", listaResultado[1].Descricao);
            Assert.Equal("Ecologia", listaResultado[2].Descricao);
            Assert.Equal("Genética", listaResultado[3].Descricao);
            Assert.Equal("Zoologia", listaResultado[4].Descricao);
        }

        [Fact]
        public async Task Deve_Mapear_Corretamente_Id_Para_Valor_E_Descricao()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 100, Descricao = "Assunto Teste 1" },
                new Assunto { Id = 200, Descricao = "Assunto Teste 2" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();

            Assert.Equal(100, listaResultado[0].Valor);
            Assert.Equal("Assunto Teste 1", listaResultado[0].Descricao);
            Assert.Equal(200, listaResultado[1].Valor);
            Assert.Equal("Assunto Teste 2", listaResultado[1].Descricao);
        }

        [Fact]
        public async Task Deve_Executar_Com_DisciplinaId_Zero()
        {
            var assuntos = ObterAssuntosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(0);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == 0),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Executar_Com_DisciplinaId_Negativo()
        {
            var assuntos = ObterAssuntosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(-1);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == -1),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Executar_Com_DisciplinaId_Muito_Grande()
        {
            var assuntos = ObterAssuntosMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(long.MaxValue);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            mediatorMock.Verify(m => m.Send(It.Is<ObterAssuntosQuery>(q => q.DisciplinaId == long.MaxValue),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Apenas_Um_Assunto()
        {
            var assuntoUnico = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = "Único Assunto" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntoUnico);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Single(resultado);

            var item = resultado.First();
            Assert.Equal(1, item.Valor);
            Assert.Equal("Único Assunto", item.Descricao);
        }

        [Fact]
        public async Task Deve_Lidar_Com_Assuntos_Com_Descricao_Vazia()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = string.Empty },
                new Assunto { Id = 2, Descricao = "Assunto Normal" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Equal(string.Empty, listaResultado[0].Descricao);
            Assert.Equal("Assunto Normal", listaResultado[1].Descricao);
        }

        [Fact]
        public async Task Deve_Lidar_Com_Assuntos_Com_Descricao_Null()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = null },
                new Assunto { Id = 2, Descricao = "Assunto Normal" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Null(listaResultado[0].Descricao);
            Assert.Equal("Assunto Normal", listaResultado[1].Descricao);
        }

        [Fact]
        public async Task Deve_Ordenar_Assuntos_Com_Descricoes_Semelhantes()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = "Assunto B" },
                new Assunto { Id = 2, Descricao = "Assunto A" },
                new Assunto { Id = 3, Descricao = "Assunto C" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();

            Assert.Equal("Assunto A", listaResultado[0].Descricao);
            Assert.Equal("Assunto B", listaResultado[1].Descricao);
            Assert.Equal("Assunto C", listaResultado[2].Descricao);
        }

        [Fact]
        public async Task Deve_Lidar_Com_Descricoes_Com_Caracteres_Especiais()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = "Matemática Avançada" },
                new Assunto { Id = 2, Descricao = "Álgebra Linear" },
                new Assunto { Id = 3, Descricao = "Cálculo I" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Contains("Álgebra Linear", listaResultado.Select(r => r.Descricao));
            Assert.Contains("Cálculo I", listaResultado.Select(r => r.Descricao));
            Assert.Contains("Matemática Avançada", listaResultado.Select(r => r.Descricao));
        }

        [Fact]
        public async Task Deve_Retornar_Muitos_Assuntos()
        {
            var muitosAssuntos = new List<Assunto>();
            for (int i = 1; i <= 100; i++)
            {
                muitosAssuntos.Add(new Assunto { Id = i, Descricao = $"Assunto {i:D3}" });
            }

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(muitosAssuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(100, resultado.Count());

            var listaResultado = resultado.ToList();
            Assert.Equal("Assunto 001", listaResultado[0].Descricao);
            Assert.Equal("Assunto 100", listaResultado[99].Descricao);
        }

        [Fact]
        public async Task Deve_Preservar_Ids_Originais_Apos_Ordenacao()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 999, Descricao = "Zoologia" },
                new Assunto { Id = 111, Descricao = "Anatomia" },
                new Assunto { Id = 555, Descricao = "Biologia" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();

            Assert.Equal(111, listaResultado[0].Valor);
            Assert.Equal(555, listaResultado[1].Valor);
            Assert.Equal(999, listaResultado[2].Valor);
        }

        [Fact]
        public async Task Deve_Lidar_Com_Descricoes_Iguais()
        {
            var assuntos = new List<Assunto>
            {
                new Assunto { Id = 1, Descricao = "Assunto Duplicado" },
                new Assunto { Id = 2, Descricao = "Assunto Duplicado" },
                new Assunto { Id = 3, Descricao = "Assunto Único" }
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAssuntosQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(assuntos);

            var resultado = await useCase.Executar(DisciplinaIdValida);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count());

            var listaResultado = resultado.ToList();

            Assert.Equal(2, listaResultado.Count(r => r.Descricao == "Assunto Duplicado"));
        }
    }
}
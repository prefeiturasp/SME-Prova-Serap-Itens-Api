using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Habilidade
{
    public class ObterHabilidadesPorCompetenciaIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioHabilidade> repositorio;
        private readonly ObterHabilidadesPorCompetenciaIdQueryHandler handler;

        public ObterHabilidadesPorCompetenciaIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioHabilidade>();
            handler = new ObterHabilidadesPorCompetenciaIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterHabilidadesPorCompetenciaIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Habilidades_Quando_Encontradas()
        {
            var habilidadesEsperadas = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 1, Descricao = "Resolver equações" },
                new RetornoHabilidadeDto { Id = 2, Descricao = "Interpretar gráficos" }
            };

            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(100))
                .ReturnsAsync(habilidadesEsperadas);

            var query = new ObterHabilidadesPorCompetenciaIdQuery(100);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, h => h.Descricao == "Resolver equações");
            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(100), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Encontrar_Habilidades()
        {
            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(999))
                .ReturnsAsync(Enumerable.Empty<RetornoHabilidadeDto>());

            var query = new ObterHabilidadesPorCompetenciaIdQuery(999);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(999), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_CompetenciaId_Correto()
        {
            var habilidadesEsperadas = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 5, Descricao = "Escrever relatórios" }
            };

            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(250))
                .ReturnsAsync(habilidadesEsperadas);

            var query = new ObterHabilidadesPorCompetenciaIdQuery(250);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(250), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task Deve_Passar_CompetenciaId_Correto_Para_Repositorio(long competenciaId)
        {
            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(competenciaId))
                .ReturnsAsync(new List<RetornoHabilidadeDto>
                {
                new RetornoHabilidadeDto { Id = competenciaId, Descricao = $"Habilidade {competenciaId}" }
                });

            var query = new ObterHabilidadesPorCompetenciaIdQuery(competenciaId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(competenciaId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Habilidades_Com_Todas_Propriedades()
        {
            var habilidadesEsperadas = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto
                {
                    Id = 3,
                    Descricao = "Analisar dados experimentais",
                    Codigo = "300"
                }
            };

            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(300))
                .ReturnsAsync(habilidadesEsperadas);

            var query = new ObterHabilidadesPorCompetenciaIdQuery(300);
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(3, resultado[0].Id);
            Assert.Equal("Analisar dados experimentais", resultado[0].Descricao);
            Assert.Equal("300", resultado[0].Codigo);
            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(300), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterHabilidadesPorCompetenciaIdQuery(100);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var habilidadesEsperadas = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto { Id = 2, Descricao = "Identificar padrões" }
            };

            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(200))
                .ReturnsAsync(habilidadesEsperadas);

            var query = new ObterHabilidadesPorCompetenciaIdQuery(200);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(200), Times.Once);
        }

        [Fact]
        public async Task Deve_Buscar_Habilidades_Por_CompetenciaId_Especifico()
        {
            var habilidadesEsperadas = new List<RetornoHabilidadeDto>
            {
                new RetornoHabilidadeDto
                {
                    Id = 10,
                    Descricao = "Realizar experimentos científicos",
                    Codigo = "450"
                }
            };

            repositorio
                .Setup(r => r.ObterHabilidadesPorCompetenciaId(450))
                .ReturnsAsync(habilidadesEsperadas);

            var query = new ObterHabilidadesPorCompetenciaIdQuery(450);
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(10, resultado[0].Id);
            Assert.Equal("Realizar experimentos científicos", resultado[0].Descricao);
            Assert.Equal("450", resultado[0].Codigo);
            repositorio.Verify(r => r.ObterHabilidadesPorCompetenciaId(450), Times.Once);
        }
    }
}

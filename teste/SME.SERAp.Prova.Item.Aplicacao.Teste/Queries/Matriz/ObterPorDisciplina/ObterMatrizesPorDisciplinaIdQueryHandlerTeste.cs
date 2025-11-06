using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Matriz
{
    public class ObterMatrizesPorDisciplinaIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioMatriz> repositorio;
        private readonly ObterMatrizesPorDisciplinaIdQueryHandler handler;

        public ObterMatrizesPorDisciplinaIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioMatriz>();
            handler = new ObterMatrizesPorDisciplinaIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterMatrizesPorDisciplinaIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Matrizes_Quando_Encontradas()
        {
            var matrizesEsperadas = new List<Dominio.Entities.Matriz>
            {
                new Dominio.Entities.Matriz { Id = 1, Descricao = "Matriz Matemática" },
                new Dominio.Entities.Matriz { Id = 2, Descricao = "Matriz Física" }
            };

            repositorio
                .Setup(r => r.ObterPorDisciplinaId(100))
                .ReturnsAsync(matrizesEsperadas);

            var query = new ObterMatrizesPorDisciplinaIdQuery(100);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
            Assert.Contains(resultado, m => m.Descricao == "Matriz Matemática");
            repositorio.Verify(r => r.ObterPorDisciplinaId(100), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Encontrar_Matrizes()
        {
            repositorio
                .Setup(r => r.ObterPorDisciplinaId(999))
                .ReturnsAsync(Enumerable.Empty<Dominio.Entities.Matriz>());

            var query = new ObterMatrizesPorDisciplinaIdQuery(999);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterPorDisciplinaId(999), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_DisciplinaId_Correto()
        {
            var matrizesEsperadas = new List<Dominio.Entities.Matriz>
            {
                new Dominio.Entities.Matriz { Id = 5, Descricao = "Matriz Química" }
            };

            repositorio
                .Setup(r => r.ObterPorDisciplinaId(250))
                .ReturnsAsync(matrizesEsperadas);

            var query = new ObterMatrizesPorDisciplinaIdQuery(250);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorDisciplinaId(250), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task Deve_Passar_DisciplinaId_Correto_Para_Repositorio(long disciplinaId)
        {
            repositorio
                .Setup(r => r.ObterPorDisciplinaId(disciplinaId))
                .ReturnsAsync(new List<Dominio.Entities.Matriz>
                {
                    new Dominio.Entities.Matriz { Id = disciplinaId, Descricao = $"Matriz {disciplinaId}" }
                });

            var query = new ObterMatrizesPorDisciplinaIdQuery(disciplinaId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorDisciplinaId(disciplinaId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Matrizes_Com_Todas_Propriedades()
        {
            var matrizesEsperadas = new List<Dominio.Entities.Matriz>
            {
                new Dominio.Entities.Matriz { Id = 3, Descricao = "Matriz Biologia", Modelo = "BIO-2025" }
            };

            repositorio
                .Setup(r => r.ObterPorDisciplinaId(300))
                .ReturnsAsync(matrizesEsperadas);

            var query = new ObterMatrizesPorDisciplinaIdQuery(300);
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(3, resultado[0].Id);
            Assert.Equal("Matriz Biologia", resultado[0].Descricao);
            Assert.Equal("BIO-2025", resultado[0].Modelo);
            repositorio.Verify(r => r.ObterPorDisciplinaId(300), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterPorDisciplinaId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterMatrizesPorDisciplinaIdQuery(100);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterPorDisciplinaId(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var matrizesEsperadas = new List<Dominio.Entities.Matriz>
            {
                new Dominio.Entities.Matriz { Id = 2, Descricao = "Matriz História" }
            };

            repositorio
                .Setup(r => r.ObterPorDisciplinaId(200))
                .ReturnsAsync(matrizesEsperadas);

            var query = new ObterMatrizesPorDisciplinaIdQuery(200);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorDisciplinaId(200), Times.Once);
        }

        [Fact]
        public async Task Deve_Buscar_Matrizes_Por_DisciplinaId_Especifico()
        {
            var matrizesEsperadas = new List<Dominio.Entities.Matriz>
            {
                new Dominio.Entities.Matriz { Id = 10, Descricao = "Matriz Filosofia", Modelo = "FIL-450" }
            };

            repositorio
                .Setup(r => r.ObterPorDisciplinaId(450))
                .ReturnsAsync(matrizesEsperadas);

            var query = new ObterMatrizesPorDisciplinaIdQuery(450);
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(10, resultado[0].Id);
            Assert.Equal("Matriz Filosofia", resultado[0].Descricao);
            Assert.Equal("FIL-450", resultado[0].Modelo);
            repositorio.Verify(r => r.ObterPorDisciplinaId(450), Times.Once);
        }
    }
}

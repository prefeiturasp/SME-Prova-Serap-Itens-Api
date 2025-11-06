using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Disciplina
{
    public class ObterDisciplinaPorLegadoIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioDisciplina> repositorio;
        private readonly ObterDisciplinaPorLegadoIdQueryHandler handler;

        public ObterDisciplinaPorLegadoIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioDisciplina>();
            handler = new ObterDisciplinaPorLegadoIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterDisciplinaPorLegadoIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Disciplina_Quando_Encontrada()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 1, Descricao = "Matemática" };
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(100))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorLegadoIdQuery(100);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(disciplinaEsperada.Id, resultado.Id);
            Assert.Equal(disciplinaEsperada.Descricao, resultado.Descricao);
            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(100), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_Disciplina()
        {
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(999))
                .ReturnsAsync((Dominio.Entities.Disciplina)null);

            var query = new ObterDisciplinaPorLegadoIdQuery(999);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(999), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_LegadoId_Correto()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 5, Descricao = "Física" };
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(250))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorLegadoIdQuery(250);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(250), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task Deve_Passar_LegadoId_Correto_Para_Repositorio(long legadoId)
        {
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(legadoId))
                .ReturnsAsync(new Dominio.Entities.Disciplina { Id = legadoId, Descricao = $"Disciplina {legadoId}" });

            var query = new ObterDisciplinaPorLegadoIdQuery(legadoId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(legadoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Disciplina_Com_Todas_Propriedades()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina
            {
                Id = 3,
                Descricao = "Química",
                Codigo = 300
            };

            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(300))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorLegadoIdQuery(300);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Id);
            Assert.Equal("Química", resultado.Descricao);
            Assert.Equal(300, resultado.Codigo);
            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(300), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterDisciplinaPorLegadoIdQuery(100);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 2, Descricao = "História" };
            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(200))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorLegadoIdQuery(200);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(200), Times.Once);
        }

        [Fact]
        public async Task Deve_Buscar_Disciplina_Por_LegadoId_Especifico()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina
            {
                Id = 10,
                Descricao = "Biologia",
                Codigo = 450
            };

            repositorio
                .Setup(r => r.ObterDisciplinaPorLegadoId(450))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorLegadoIdQuery(450);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(10, resultado.Id);
            Assert.Equal("Biologia", resultado.Descricao);
            Assert.Equal(450, resultado.Codigo);
            repositorio.Verify(r => r.ObterDisciplinaPorLegadoId(450), Times.Once);
        }
    }
}

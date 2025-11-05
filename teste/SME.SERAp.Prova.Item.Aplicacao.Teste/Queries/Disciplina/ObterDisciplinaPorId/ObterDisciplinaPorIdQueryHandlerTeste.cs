using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Disciplina
{
    public class ObterDisciplinaPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioDisciplina> repositorio;
        private readonly ObterDisciplinaPorIdQueryHandler handler;

        public ObterDisciplinaPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioDisciplina>();
            handler = new ObterDisciplinaPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterDisciplinaPorIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Disciplina_Quando_Encontrada()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 1, Descricao = "Matemática" };
            repositorio
                .Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(disciplinaEsperada.Id, resultado.Id);
            Assert.Equal(disciplinaEsperada.Descricao, resultado.Descricao);
            repositorio.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_Disciplina()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(99))
                .ReturnsAsync((Dominio.Entities.Disciplina)null);

            var query = new ObterDisciplinaPorIdQuery(99);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorIdAsync(99), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_Id_Correto()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 5, Descricao = "Física" };
            repositorio
                .Setup(r => r.ObterPorIdAsync(5))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorIdQuery(5);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(5), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(999)]
        public async Task Deve_Passar_Id_Correto_Para_Repositorio(long id)
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(id))
                .ReturnsAsync(new Dominio.Entities.Disciplina { Id = id, Descricao = $"Disciplina {id}" });

            var query = new ObterDisciplinaPorIdQuery(id);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Disciplina_Com_Todas_Propriedades()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina
            {
                Id = 3,
                Descricao = "Química",
                Codigo = 123
            };

            repositorio
                .Setup(r => r.ObterPorIdAsync(3))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorIdQuery(3);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Id);
            Assert.Equal("Química", resultado.Descricao);
            Assert.Equal(123, resultado.Codigo);
            repositorio.Verify(r => r.ObterPorIdAsync(3), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterDisciplinaPorIdQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterPorIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var disciplinaEsperada = new Dominio.Entities.Disciplina { Id = 2, Descricao = "História" };
            repositorio
                .Setup(r => r.ObterPorIdAsync(2))
                .ReturnsAsync(disciplinaEsperada);

            var query = new ObterDisciplinaPorIdQuery(2);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(2), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Disciplina_Especifica_Por_Id()
        {
            var disciplinas = new List<Dominio.Entities.Disciplina>
            {
                new Dominio.Entities.Disciplina { Id = 1, Descricao = "Português" },
                new Dominio.Entities.Disciplina { Id = 2, Descricao = "Inglês" },
                new Dominio.Entities.Disciplina { Id = 3, Descricao = "Espanhol" }
            };

            repositorio
                .Setup(r => r.ObterPorIdAsync(2))
                .ReturnsAsync(disciplinas[1]);

            var query = new ObterDisciplinaPorIdQuery(2);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Id);
            Assert.Equal("Inglês", resultado.Descricao);
            repositorio.Verify(r => r.ObterPorIdAsync(2), Times.Once);
        }
    }
}

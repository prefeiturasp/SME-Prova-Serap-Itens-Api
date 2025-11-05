using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Disciplina
{
    public class ObterDisciplinasPorAreaConhecimentoIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioDisciplina> repositorio;
        private readonly ObterDisciplinasPorAreaConhecimentoIdQueryHandler handler;

        public ObterDisciplinasPorAreaConhecimentoIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioDisciplina>();
            handler = new ObterDisciplinasPorAreaConhecimentoIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterDisciplinasPorAreaConhecimentoIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Disciplinas_Quando_Encontradas()
        {
            var disciplinasEsperadas = new List<Dominio.Entities.Disciplina>
            {
                new Dominio.Entities.Disciplina { Id = 1, Descricao = "Matemática" },
                new Dominio.Entities.Disciplina { Id = 2, Descricao = "Física" },
                new Dominio.Entities.Disciplina { Id = 3, Descricao = "Química" }
            };

            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(1))
                .ReturnsAsync(disciplinasEsperadas);

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal(3, listaResultado.Count);
            Assert.Equal(disciplinasEsperadas[0].Id, listaResultado[0].Id);
            Assert.Equal(disciplinasEsperadas[0].Descricao, listaResultado[0].Descricao);
            Assert.Equal(disciplinasEsperadas[1].Id, listaResultado[1].Id);
            Assert.Equal(disciplinasEsperadas[2].Id, listaResultado[2].Id);
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Disciplinas()
        {
            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(5))
                .ReturnsAsync(new List<Dominio.Entities.Disciplina>());

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(5);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(5), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Uma_Disciplina_Quando_Houver_Apenas_Uma()
        {
            var disciplinasEsperadas = new List<Dominio.Entities.Disciplina>
            {
                new Dominio.Entities.Disciplina { Id = 1, Descricao = "Português" }
            };

            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(2))
                .ReturnsAsync(disciplinasEsperadas);

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(2);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal(1, listaResultado[0].Id);
            Assert.Equal("Português", listaResultado[0].Descricao);
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(2), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_AreaConhecimentoId_Correto()
        {
            var disciplinasEsperadas = new List<Dominio.Entities.Disciplina>
            {
                new Dominio.Entities.Disciplina { Id = 1, Descricao = "História" }
            };

            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(10))
                .ReturnsAsync(disciplinasEsperadas);

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(10);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(10), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async Task Deve_Passar_AreaConhecimentoId_Correto_Para_Repositorio(long areaConhecimentoId)
        {
            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(areaConhecimentoId))
                .ReturnsAsync(new List<Dominio.Entities.Disciplina>());

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(areaConhecimentoId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(areaConhecimentoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Multiplas_Disciplinas_Da_Mesma_Area()
        {
            var disciplinasEsperadas = new List<Dominio.Entities.Disciplina>
            {
                new Dominio.Entities.Disciplina { Id = 10, Descricao = "Biologia" },
                new Dominio.Entities.Disciplina { Id = 11, Descricao = "Química Orgânica" },
                new Dominio.Entities.Disciplina { Id = 12, Descricao = "Física Moderna" },
                new Dominio.Entities.Disciplina { Id = 13, Descricao = "Astronomia" }
            };

            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(3))
                .ReturnsAsync(disciplinasEsperadas);

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(3);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var listaResultado = resultado.ToList();
            Assert.Equal(4, listaResultado.Count);
            Assert.All(listaResultado, disciplina =>
            {
                Assert.True(disciplina.Id > 0);
                Assert.NotNull(disciplina.Descricao);
                Assert.NotEmpty(disciplina.Descricao);
            });
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(3), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Retornar_Null()
        {
            repositorio
                .Setup(r => r.ObterPorAreaconhecimentoId(99))
                .ReturnsAsync((IEnumerable<Dominio.Entities.Disciplina>)null);

            var query = new ObterDisciplinasPorAreaConhecimentoIdQuery(99);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorAreaconhecimentoId(99), Times.Once);
        }
    }
}

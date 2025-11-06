using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Assunto
{
    public class ObterAssuntosQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAssunto> repositorio;
        private readonly ObterAssuntosQueryHandler handler;

        public ObterAssuntosQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAssunto>();
            handler = new ObterAssuntosQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterAssuntosQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Assuntos_Ordenados_Por_Descricao()
        {
            var assuntosEsperados = new List<Dominio.Entities.Assunto>
            {
                new Dominio.Entities.Assunto { Id = 3, Descricao = "Trigonometria" },
                new Dominio.Entities.Assunto { Id = 1, Descricao = "Álgebra" },
                new Dominio.Entities.Assunto { Id = 2, Descricao = "Geometria" }
            };

            repositorio
                .Setup(r => r.ObterAssuntos(1))
                .ReturnsAsync(assuntosEsperados);

            var query = new ObterAssuntosQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal(3, listaResultado.Count);
            Assert.Equal("Álgebra", listaResultado[0].Descricao);
            Assert.Equal("Geometria", listaResultado[1].Descricao);
            Assert.Equal("Trigonometria", listaResultado[2].Descricao);
            repositorio.Verify(r => r.ObterAssuntos(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Assuntos()
        {
            repositorio
                .Setup(r => r.ObterAssuntos(5))
                .ReturnsAsync(new List<Dominio.Entities.Assunto>());

            var query = new ObterAssuntosQuery(5);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterAssuntos(5), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Um_Assunto_Quando_Houver_Apenas_Um()
        {
            var assuntosEsperados = new List<Dominio.Entities.Assunto>
            {
                new Dominio.Entities.Assunto { Id = 1, Descricao = "Funções" }
            };

            repositorio
                .Setup(r => r.ObterAssuntos(2))
                .ReturnsAsync(assuntosEsperados);

            var query = new ObterAssuntosQuery(2);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal("Funções", listaResultado[0].Descricao);
            repositorio.Verify(r => r.ObterAssuntos(2), Times.Once);
        }

        [Fact]
        public async Task Deve_Manter_Ordenacao_Alfabetica_Com_Multiplos_Assuntos()
        {
            var assuntosEsperados = new List<Dominio.Entities.Assunto>
            {
                new Dominio.Entities.Assunto { Id = 5, Descricao = "Probabilidade" },
                new Dominio.Entities.Assunto { Id = 4, Descricao = "Conjuntos" },
                new Dominio.Entities.Assunto { Id = 3, Descricao = "Estatística" },
                new Dominio.Entities.Assunto { Id = 2, Descricao = "Matrizes" },
                new Dominio.Entities.Assunto { Id = 1, Descricao = "Aritmética" }
            };

            repositorio
                .Setup(r => r.ObterAssuntos(3))
                .ReturnsAsync(assuntosEsperados);

            var query = new ObterAssuntosQuery(3);
            var resultado = await handler.Handle(query, CancellationToken.None);

            var listaResultado = resultado.ToList();
            Assert.Equal(5, listaResultado.Count);
            Assert.Equal("Aritmética", listaResultado[0].Descricao);
            Assert.Equal("Conjuntos", listaResultado[1].Descricao);
            Assert.Equal("Estatística", listaResultado[2].Descricao);
            Assert.Equal("Matrizes", listaResultado[3].Descricao);
            Assert.Equal("Probabilidade", listaResultado[4].Descricao);
            repositorio.Verify(r => r.ObterAssuntos(3), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_DisciplinaId_Correto()
        {
            var assuntosEsperados = new List<Dominio.Entities.Assunto>
            {
                new Dominio.Entities.Assunto { Id = 1, Descricao = "Assunto Teste" }
            };

            repositorio
                .Setup(r => r.ObterAssuntos(10))
                .ReturnsAsync(assuntosEsperados);

            var query = new ObterAssuntosQuery(10);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterAssuntos(10), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(100)]
        public async Task Deve_Passar_DisciplinaId_Correto_Para_Repositorio(long disciplinaId)
        {
            repositorio
                .Setup(r => r.ObterAssuntos(disciplinaId))
                .ReturnsAsync(new List<Dominio.Entities.Assunto>());

            var query = new ObterAssuntosQuery(disciplinaId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterAssuntos(disciplinaId), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterAssuntos(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterAssuntosQuery(1);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterAssuntos(It.IsAny<long>()), Times.Once);
        }
    }
}

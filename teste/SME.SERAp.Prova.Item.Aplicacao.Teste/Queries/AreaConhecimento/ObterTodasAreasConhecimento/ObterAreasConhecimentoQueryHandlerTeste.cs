using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.AreaConhecimento
{
    public class ObterAreasConhecimentoQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAreaConhecimento> repositorio;
        private readonly ObterAreasConhecimentoQueryHandler handler;

        public ObterAreasConhecimentoQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAreaConhecimento>();
            handler = new ObterAreasConhecimentoQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterAreasConhecimentoQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Areas_Ordenadas_Por_Descricao()
        {
            var areasEsperadas = new List<Dominio.Entities.AreaConhecimento>
            {
                new Dominio.Entities.AreaConhecimento { Id = 3, Descricao = "Matemática" },
                new Dominio.Entities.AreaConhecimento { Id = 1, Descricao = "Ciências Humanas" },
                new Dominio.Entities.AreaConhecimento { Id = 2, Descricao = "Linguagens" }
            };

            repositorio
                .Setup(r => r.ObterTudoAsync())
                .ReturnsAsync(areasEsperadas);

            var query = new ObterAreasConhecimentoQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal(3, listaResultado.Count);
            Assert.Equal("Ciências Humanas", listaResultado[0].Descricao);
            Assert.Equal("Linguagens", listaResultado[1].Descricao);
            Assert.Equal("Matemática", listaResultado[2].Descricao);
            repositorio.Verify(r => r.ObterTudoAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Areas()
        {
            repositorio
                .Setup(r => r.ObterTudoAsync())
                .ReturnsAsync(new List<Dominio.Entities.AreaConhecimento>());

            var query = new ObterAreasConhecimentoQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterTudoAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Uma_Area_Quando_Houver_Apenas_Uma()
        {
            var areasEsperadas = new List<Dominio.Entities.AreaConhecimento>
            {
                new Dominio.Entities.AreaConhecimento { Id = 1, Descricao = "Linguagens" }
            };

            repositorio
                .Setup(r => r.ObterTudoAsync())
                .ReturnsAsync(areasEsperadas);

            var query = new ObterAreasConhecimentoQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal("Linguagens", listaResultado[0].Descricao);
            repositorio.Verify(r => r.ObterTudoAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Manter_Ordenacao_Alfabetica_Com_Multiplas_Areas()
        {
            var areasEsperadas = new List<Dominio.Entities.AreaConhecimento>
            {
                new Dominio.Entities.AreaConhecimento { Id = 5, Descricao = "Química" },
                new Dominio.Entities.AreaConhecimento { Id = 4, Descricao = "Biologia" },
                new Dominio.Entities.AreaConhecimento { Id = 3, Descricao = "Física" },
                new Dominio.Entities.AreaConhecimento { Id = 2, Descricao = "História" },
                new Dominio.Entities.AreaConhecimento { Id = 1, Descricao = "Artes" }
            };

            repositorio
                .Setup(r => r.ObterTudoAsync())
                .ReturnsAsync(areasEsperadas);

            var query = new ObterAreasConhecimentoQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            var listaResultado = resultado.ToList();
            Assert.Equal(5, listaResultado.Count);
            Assert.Equal("Artes", listaResultado[0].Descricao);
            Assert.Equal("Biologia", listaResultado[1].Descricao);
            Assert.Equal("Física", listaResultado[2].Descricao);
            Assert.Equal("História", listaResultado[3].Descricao);
            Assert.Equal("Química", listaResultado[4].Descricao);
            repositorio.Verify(r => r.ObterTudoAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterTudoAsync())
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterAreasConhecimentoQuery();
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterTudoAsync(), Times.Once);
        }
    }
}

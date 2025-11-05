using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Dificuldade
{
    public class ObterIdDescricaoOrdemQueryHandlerTeste
    {
        private readonly Mock<IRepositorioDificuldade> repositorio;
        private readonly ObterIdDescricaoOrdemQueryHandler handler;

        public ObterIdDescricaoOrdemQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioDificuldade>();
            handler = new ObterIdDescricaoOrdemQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterIdDescricaoOrdemQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Dificuldades_Quando_Encontradas()
        {
            var dificuldadesEsperadas = new List<Dominio.Entities.Dificuldade>
            {
                new Dominio.Entities.Dificuldade { Id = 1, Descricao = "Fácil", Ordem = 1 },
                new Dominio.Entities.Dificuldade { Id = 2, Descricao = "Médio", Ordem = 2 },
                new Dominio.Entities.Dificuldade { Id = 3, Descricao = "Difícil", Ordem = 3 }
            };

            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(dificuldadesEsperadas);

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Equal(3, listaResultado.Count);
            Assert.Equal(dificuldadesEsperadas[0].Id, listaResultado[0].Id);
            Assert.Equal(dificuldadesEsperadas[0].Descricao, listaResultado[0].Descricao);
            Assert.Equal(dificuldadesEsperadas[0].Ordem, listaResultado[0].Ordem);
            Assert.Equal(dificuldadesEsperadas[1].Id, listaResultado[1].Id);
            Assert.Equal(dificuldadesEsperadas[2].Id, listaResultado[2].Id);
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Dificuldades()
        {
            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(new List<Dominio.Entities.Dificuldade>());

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Uma_Dificuldade_Quando_Houver_Apenas_Uma()
        {
            var dificuldadesEsperadas = new List<Dominio.Entities.Dificuldade>
            {
                new Dominio.Entities.Dificuldade { Id = 1, Descricao = "Fácil", Ordem = 1 }
            };

            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(dificuldadesEsperadas);

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal(1, listaResultado[0].Id);
            Assert.Equal("Fácil", listaResultado[0].Descricao);
            Assert.Equal(1, listaResultado[0].Ordem);
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Dificuldades_Com_Propriedades_Id_Descricao_Ordem()
        {
            var dificuldadesEsperadas = new List<Dominio.Entities.Dificuldade>
            {
                new Dominio.Entities.Dificuldade { Id = 5, Descricao = "Muito Difícil", Ordem = 5 },
                new Dominio.Entities.Dificuldade { Id = 4, Descricao = "Difícil", Ordem = 4 }
            };

            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(dificuldadesEsperadas);

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            var listaResultado = resultado.ToList();
            Assert.Equal(2, listaResultado.Count);
            Assert.All(listaResultado, dificuldade =>
            {
                Assert.True(dificuldade.Id > 0);
                Assert.NotNull(dificuldade.Descricao);
                Assert.True(dificuldade.Ordem > 0);
            });
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Uma_Vez()
        {
            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(new List<Dominio.Entities.Dificuldade>());

            var query = new ObterIdDescricaoOrdemQuery();
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterIdDescricaoOrdemQuery();
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Retornar_Null()
        {
            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync((IEnumerable<Dominio.Entities.Dificuldade>)null);

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterIdDescricaoOrdemAsync(), Times.Once);
        }

        [Fact]
        public async Task Deve_Preservar_Ordem_Retornada_Pelo_Repositorio()
        {
            var dificuldadesEsperadas = new List<Dominio.Entities.Dificuldade>
            {
                new Dominio.Entities.Dificuldade { Id = 1, Descricao = "Fácil", Ordem = 1 },
                new Dominio.Entities.Dificuldade { Id = 2, Descricao = "Médio", Ordem = 2 },
                new Dominio.Entities.Dificuldade { Id = 3, Descricao = "Difícil", Ordem = 3 }
            };

            repositorio
                .Setup(r => r.ObterIdDescricaoOrdemAsync())
                .ReturnsAsync(dificuldadesEsperadas);

            var query = new ObterIdDescricaoOrdemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            var listaResultado = resultado.ToList();
            for (int i = 0; i < dificuldadesEsperadas.Count; i++)
            {
                Assert.Equal(dificuldadesEsperadas[i].Id, listaResultado[i].Id);
                Assert.Equal(dificuldadesEsperadas[i].Ordem, listaResultado[i].Ordem);
            }
        }
    }
}

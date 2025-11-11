using Moq;
using SME.SERAp.Prova.Item.Aplicacao.Queries.NivelItem.ObterIdNivelItemOrdem;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.NivelItem
{
    public class ObterNivelItemQueryHandlerTeste
    {
        private readonly Mock<IRepositorioNivelItem> repositorioNivelItem;
        private readonly ObterNivelItemQueryHandler handler;

        public ObterNivelItemQueryHandlerTeste()
        {
            repositorioNivelItem = new Mock<IRepositorioNivelItem>();
            handler = new ObterNivelItemQueryHandler(repositorioNivelItem.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterNivelItemQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Niveis_Quando_Encontrados()
        {
            var niveisEsperados = new List<Dominio.Entities.NivelItem>
            {
                new Dominio.Entities.NivelItem { Id = 1, Descricao = "Básico" },
                new Dominio.Entities.NivelItem { Id = 2, Descricao = "Avançado" }
            };

            repositorioNivelItem
                .Setup(r => r.Obter())
                .ReturnsAsync(niveisEsperados);

            var query = new ObterNivelItemQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, n => n.Descricao == "Básico");
            repositorioNivelItem.Verify(r => r.Obter(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Niveis()
        {
            repositorioNivelItem
                .Setup(r => r.Obter())
                .ReturnsAsync(Enumerable.Empty<Dominio.Entities.NivelItem>());

            var query = new ObterNivelItemQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioNivelItem.Verify(r => r.Obter(), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var niveisEsperados = new List<Dominio.Entities.NivelItem>
            {
                new Dominio.Entities.NivelItem { Id = 10, Descricao = "Intermediário" }
            };

            repositorioNivelItem
                .Setup(r => r.Obter())
                .ReturnsAsync(niveisEsperados);

            var query = new ObterNivelItemQuery();
            await handler.Handle(query, CancellationToken.None);

            repositorioNivelItem.Verify(r => r.Obter(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Niveis_Com_Todas_Propriedades()
        {
            var niveisEsperados = new List<Dominio.Entities.NivelItem>
            {
                new Dominio.Entities.NivelItem { Id = 5, Descricao = "Avançado" }
            };

            repositorioNivelItem
                .Setup(r => r.Obter())
                .ReturnsAsync(niveisEsperados);

            var query = new ObterNivelItemQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(5, resultado[0].Id);
            Assert.Equal("Avançado", resultado[0].Descricao);
            repositorioNivelItem.Verify(r => r.Obter(), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorioNivelItem
                .Setup(r => r.Obter())
                .ThrowsAsync(new InvalidOperationException("Erro no repositório"));

            var query = new ObterNivelItemQuery();
            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no repositório", excecao.Message);
            repositorioNivelItem.Verify(r => r.Obter(), Times.Once);
        }
    }
}

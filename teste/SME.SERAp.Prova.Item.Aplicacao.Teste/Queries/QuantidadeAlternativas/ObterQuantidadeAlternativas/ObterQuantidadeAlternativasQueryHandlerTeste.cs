using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.QuantidadeAlternativas
{
    public class ObterQuantidadeAlternativasQueryHandlerTeste
    {
        private readonly Mock<IRepositorioQuantidadeAlternativas> repositorioQuantidadeAlternativas;
        private readonly ObterQuantidadeAlternativasQueryHandler handler;

        public ObterQuantidadeAlternativasQueryHandlerTeste()
        {
            repositorioQuantidadeAlternativas = new Mock<IRepositorioQuantidadeAlternativas>();
            handler = new ObterQuantidadeAlternativasQueryHandler(repositorioQuantidadeAlternativas.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterQuantidadeAlternativasQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_QuantidadeAlternativas_Ordenada_Por_Descricao()
        {
            var alternativasEsperadas = new List<Dominio.Entities.QuantidadeAlternativas>
            {
                new Dominio.Entities.QuantidadeAlternativas { Id = 2, Descricao = "Cinco alternativas" },
                new Dominio.Entities.QuantidadeAlternativas { Id = 1, Descricao = "Três alternativas" },
                new Dominio.Entities.QuantidadeAlternativas { Id = 3, Descricao = "Duas alternativas" }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Count);

            // Deve estar ordenado alfabeticamente pela Descricao
            Assert.Equal("Cinco alternativas", resultado[0].Descricao);
            Assert.Equal("Duas alternativas", resultado[1].Descricao);
            Assert.Equal("Três alternativas", resultado[2].Descricao);

            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Alternativas()
        {
            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ReturnsAsync(Enumerable.Empty<Dominio.Entities.QuantidadeAlternativas>());

            var query = new ObterQuantidadeAlternativasQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var alternativasEsperadas = new List<Dominio.Entities.QuantidadeAlternativas>
            {
                new Dominio.Entities.QuantidadeAlternativas { Id = 10, Descricao = "Quatro alternativas" }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterQuantidadeAlternativasQuery();
            await handler.Handle(query, CancellationToken.None);

            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Alternativas_Com_Todas_Propriedades()
        {
            var alternativasEsperadas = new List<Dominio.Entities.QuantidadeAlternativas>
            {
                new Dominio.Entities.QuantidadeAlternativas { Id = 5, Descricao = "Seis alternativas", Codigo = 123 }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(5, resultado[0].Id);
            Assert.Equal("Seis alternativas", resultado[0].Descricao);
            Assert.Equal(123, resultado[0].Codigo);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ThrowsAsync(new InvalidOperationException("Erro ao obter alternativas"));

            var query = new ObterQuantidadeAlternativasQuery();

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro ao obter alternativas", excecao.Message);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }

        [Theory]
        [InlineData(1, "3 alternativas")]
        [InlineData(2, "4 alternativas")]
        [InlineData(3, "5 alternativas")]
        public async Task Deve_Retornar_Alternativas_Corretas_Para_Diferentes_Casos(int id, string descricao)
        {
            var alternativasEsperadas = new List<Dominio.Entities.QuantidadeAlternativas>
            {
                new Dominio.Entities.QuantidadeAlternativas { Id = id, Descricao = descricao }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(id, resultado[0].Id);
            Assert.Equal(descricao, resultado[0].Descricao);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterQuantidadeAlternativas(), Times.Once);
        }
    }
}

using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.QuantidadeAlternativas
{
    public class ObterListaQuantidadeAlternativasQueryHandlerTeste
    {
        private readonly Mock<IRepositorioQuantidadeAlternativas> repositorioQuantidadeAlternativas;
        private readonly ObterListaQuantidadeAlternativasQueryHandler handler;

        public ObterListaQuantidadeAlternativasQueryHandlerTeste()
        {
            repositorioQuantidadeAlternativas = new Mock<IRepositorioQuantidadeAlternativas>();
            handler = new ObterListaQuantidadeAlternativasQueryHandler(repositorioQuantidadeAlternativas.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterListaQuantidadeAlternativasQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_QuantidadeAlternativas_Quando_Encontradas()
        {
            var alternativasEsperadas = new List<QuantidadeAlternativasDto>
            {
                new QuantidadeAlternativasDto { Valor = 1, Descricao = "3 Alternativas" },
                new QuantidadeAlternativasDto { Valor = 2, Descricao = "5 Alternativas" }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterListaQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, a => a.Descricao == "3 Alternativas");
            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Alternativas()
        {
            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ReturnsAsync(Enumerable.Empty<QuantidadeAlternativasDto>());

            var query = new ObterListaQuantidadeAlternativasQuery();
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var alternativasEsperadas = new List<QuantidadeAlternativasDto>
            {
                new QuantidadeAlternativasDto { Valor = 10, Descricao = "4 Alternativas" }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterListaQuantidadeAlternativasQuery();
            await handler.Handle(query, CancellationToken.None);

            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Alternativas_Com_Todas_Propriedades()
        {
            var alternativasEsperadas = new List<QuantidadeAlternativasDto>
            {
                new QuantidadeAlternativasDto { Valor = 5, Descricao = "6 Alternativas", Quantidade = 4 }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterListaQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(5, resultado[0].Valor);
            Assert.Equal("6 Alternativas", resultado[0].Descricao);
            Assert.Equal(4, resultado[0].Quantidade);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ThrowsAsync(new InvalidOperationException("Erro no repositório de alternativas"));

            var query = new ObterListaQuantidadeAlternativasQuery();

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no repositório de alternativas", excecao.Message);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }

        [Theory]
        [InlineData(1, "3 Alternativas")]
        [InlineData(2, "4 Alternativas")]
        [InlineData(3, "5 Alternativas")]
        public async Task Deve_Retornar_Alternativas_Corretas_Para_Diferentes_Casos(int id, string descricao)
        {
            var alternativasEsperadas = new List<QuantidadeAlternativasDto>
            {
                new QuantidadeAlternativasDto { Valor = id, Descricao = descricao }
            };

            repositorioQuantidadeAlternativas
                .Setup(r => r.ObterListaQuantidadeAlternativas())
                .ReturnsAsync(alternativasEsperadas);

            var query = new ObterListaQuantidadeAlternativasQuery();
            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(id, resultado[0].Valor);
            Assert.Equal(descricao, resultado[0].Descricao);
            repositorioQuantidadeAlternativas.Verify(r => r.ObterListaQuantidadeAlternativas(), Times.Once);
        }
    }
}

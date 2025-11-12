using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Item
{
    public class ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandlerTeste
    {
        private readonly Mock<IRepositorioItem> repositorioItemMock;
        private readonly ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandler handler;
        private const long AreaConhecimentoIdValido = 10;
        private const long DisciplinaIdValido = 20;

        public ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandlerTeste()
        {
            repositorioItemMock = new Mock<IRepositorioItem>();
            handler = new ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandler(repositorioItemMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Quantidade_Quando_Repositorio_Retornar_Dado()
        {
            long? quantidadeEsperada = 15;
            var query = new ObterQtdPorAreaConhecimentoEhDisciplinaQuery(AreaConhecimentoIdValido, DisciplinaIdValido);

            repositorioItemMock.Setup(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido))
                               .ReturnsAsync(quantidadeEsperada);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(quantidadeEsperada, resultado);

            repositorioItemMock.Verify(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Nao_Tiver_Dados()
        {
            long? valorNulo = null;
            var query = new ObterQtdPorAreaConhecimentoEhDisciplinaQuery(AreaConhecimentoIdValido, DisciplinaIdValido);

            repositorioItemMock.Setup(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido))
                               .ReturnsAsync(valorNulo);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioItemMock.Verify(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido), Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Repositorio_Falhar()
        {
            var query = new ObterQtdPorAreaConhecimentoEhDisciplinaQuery(AreaConhecimentoIdValido, DisciplinaIdValido);
            var excecaoEsperada = new InvalidOperationException("Falha no banco de dados.");

            repositorioItemMock.Setup(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido))
                               .ThrowsAsync(excecaoEsperada);

            var excecaoCapturada = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal(excecaoEsperada.Message, excecaoCapturada.Message);
            repositorioItemMock.Verify(r => r.ObterQtdItensAreaConhecimentoEhDisciplina(AreaConhecimentoIdValido, DisciplinaIdValido), Times.Once);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandler(null));
        }
    }
}

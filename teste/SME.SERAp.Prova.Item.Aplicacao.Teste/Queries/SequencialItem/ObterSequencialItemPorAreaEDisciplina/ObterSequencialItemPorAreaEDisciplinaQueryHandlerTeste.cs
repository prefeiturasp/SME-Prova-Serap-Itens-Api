using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.SequencialItem
{
    public class ObterSequencialItemPorAreaEDisciplinaQueryHandlerTeste
    {
        private readonly Mock<IRepositoSequencialItem> repositorioSequencialItem;
        private readonly ObterSequencialItemPorAreaEDisciplinaQueryHandler handler;

        public ObterSequencialItemPorAreaEDisciplinaQueryHandlerTeste()
        {
            repositorioSequencialItem = new Mock<IRepositoSequencialItem>();
            handler = new ObterSequencialItemPorAreaEDisciplinaQueryHandler(repositorioSequencialItem.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterSequencialItemPorAreaEDisciplinaQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_SequencialItem_Quando_Encontrado()
        {
            var esperado = new Dominio.Entities.SequencialItem
            {
                Id = It.IsAny<long>(),
                CodigoAreaConhecimento = It.IsAny<long>(),
                CodigoDisciplina = It.IsAny<long>(),
                Sequencial = It.IsAny<long>()
            };

            repositorioSequencialItem
                .Setup(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina))
                .ReturnsAsync(esperado);

            var query = new ObterSequencialItemPorAreaEDisciplinaQuery(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(esperado.Id, resultado.Id);
            Assert.Equal(esperado.CodigoAreaConhecimento, resultado.CodigoAreaConhecimento);
            Assert.Equal(esperado.CodigoDisciplina, resultado.CodigoDisciplina);
            Assert.Equal(esperado.Sequencial, resultado.Sequencial);

            repositorioSequencialItem.Verify(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Nao_Encontrar_SequencialItem()
        {
            var codigoAreaConhecimento = It.IsAny<long>();
            var codigoDisciplina = It.IsAny<long>();
            repositorioSequencialItem
                .Setup(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(codigoAreaConhecimento, codigoDisciplina))
                .ReturnsAsync((Dominio.Entities.SequencialItem)null);

            var query = new ObterSequencialItemPorAreaEDisciplinaQuery(codigoAreaConhecimento, codigoDisciplina);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorioSequencialItem.Verify(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(codigoAreaConhecimento, codigoDisciplina), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            var codigoAreaConhecimento = It.IsAny<long>();
            var codigoDisciplina = It.IsAny<long>();
            repositorioSequencialItem
                .Setup(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(codigoAreaConhecimento, codigoDisciplina))
                .ThrowsAsync(new InvalidOperationException("Erro no repositório"));

            var query = new ObterSequencialItemPorAreaEDisciplinaQuery(codigoAreaConhecimento, codigoDisciplina);

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no repositório", excecao.Message);
            repositorioSequencialItem.Verify(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(codigoAreaConhecimento, codigoDisciplina), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_Parametros_Corretos()
        {
            var esperado = new Dominio.Entities.SequencialItem
            {
                Id = It.IsAny<long>(),
                CodigoAreaConhecimento = It.IsAny<long>(),
                CodigoDisciplina = It.IsAny<long>(),
                Sequencial = It.IsAny<long>()
            };

            repositorioSequencialItem
                .Setup(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina))
                .ReturnsAsync(esperado);

            var query = new ObterSequencialItemPorAreaEDisciplinaQuery(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina);
            await handler.Handle(query, CancellationToken.None);

            repositorioSequencialItem.Verify(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(esperado.CodigoAreaConhecimento, esperado.CodigoDisciplina), Times.Once);
        }

        [Theory]
        [InlineData(123, 987)]
        [InlineData(456, 654)]
        [InlineData(789, 321)]
        public async Task Deve_Passar_Parametros_Corretos_Para_Repositorio(long area, long disciplina)
        {
            var esperado = new Dominio.Entities.SequencialItem { Id = 10, CodigoAreaConhecimento = area, CodigoDisciplina = disciplina, Sequencial = 50 };

            repositorioSequencialItem
                .Setup(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(area, disciplina))
                .ReturnsAsync(esperado);

            var query = new ObterSequencialItemPorAreaEDisciplinaQuery(area, disciplina);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(area, resultado.CodigoAreaConhecimento);
            Assert.Equal(disciplina, resultado.CodigoDisciplina);

            repositorioSequencialItem.Verify(r => r.ObterSequencialItemPorCodigoAreaEDisciplina(area, disciplina), Times.Once);
        }
    }
}

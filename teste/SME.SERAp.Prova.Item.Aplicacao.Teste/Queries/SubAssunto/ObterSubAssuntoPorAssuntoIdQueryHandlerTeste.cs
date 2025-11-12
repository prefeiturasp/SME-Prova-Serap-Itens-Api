using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.SubAssunto
{
    public class ObterSubAssuntoPorAssuntoIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioSubAssunto> repositorioSubAssunto;
        private readonly ObterSubAssuntoPorAssuntoIdQueryHandler handler;

        public ObterSubAssuntoPorAssuntoIdQueryHandlerTeste()
        {
            repositorioSubAssunto = new Mock<IRepositorioSubAssunto>();
            handler = new ObterSubAssuntoPorAssuntoIdQueryHandler(repositorioSubAssunto.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterSubAssuntoPorAssuntoIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Lista_De_SubAssuntos_Quando_Encontrar()
        {
            var assuntoId = 5;
            var subAssuntosEsperados = new List<Dominio.Entities.SubAssunto>
            {
                new Dominio.Entities.SubAssunto { Id = 1, Descricao = "SubAssunto A", AssuntoId = assuntoId },
                new Dominio.Entities.SubAssunto { Id = 2, Descricao = "SubAssunto B", AssuntoId = assuntoId }
            };

            repositorioSubAssunto
                .Setup(r => r.ObterSubAssuntosPorAssuntoId(assuntoId))
                .ReturnsAsync(subAssuntosEsperados);

            var query = new ObterSubAssuntoPorAssuntoIdQuery(assuntoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Collection(resultado,
                item => Assert.Equal("SubAssunto A", item.Descricao),
                item => Assert.Equal("SubAssunto B", item.Descricao));

            repositorioSubAssunto.Verify(r => r.ObterSubAssuntosPorAssuntoId(assuntoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_SubAssuntos()
        {
            var assuntoId = 10;
            repositorioSubAssunto
                .Setup(r => r.ObterSubAssuntosPorAssuntoId(assuntoId))
                .ReturnsAsync(new List<Dominio.Entities.SubAssunto>());

            var query = new ObterSubAssuntoPorAssuntoIdQuery(assuntoId);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);

            repositorioSubAssunto.Verify(r => r.ObterSubAssuntosPorAssuntoId(assuntoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            var assuntoId = 99;
            repositorioSubAssunto
                .Setup(r => r.ObterSubAssuntosPorAssuntoId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Erro no repositório"));

            var query = new ObterSubAssuntoPorAssuntoIdQuery(assuntoId);

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no repositório", excecao.Message);
            repositorioSubAssunto.Verify(r => r.ObterSubAssuntosPorAssuntoId(assuntoId), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public async Task Deve_Chamar_Repositorio_Com_AssuntoId_Correto(long assuntoId)
        {
            var subAssuntos = new List<Dominio.Entities.SubAssunto> { new Dominio.Entities.SubAssunto { Id = 1, Descricao = "Teste", AssuntoId = assuntoId } };

            repositorioSubAssunto
                .Setup(r => r.ObterSubAssuntosPorAssuntoId(assuntoId))
                .ReturnsAsync(subAssuntos);

            var query = new ObterSubAssuntoPorAssuntoIdQuery(assuntoId);
            await handler.Handle(query, CancellationToken.None);

            repositorioSubAssunto.Verify(r => r.ObterSubAssuntosPorAssuntoId(assuntoId), Times.Once);
        }

        [Fact]
        public async Task Deve_Permitir_AssuntoId_Igual_A_Zero_Sem_Erro()
        {
            repositorioSubAssunto
                .Setup(r => r.ObterSubAssuntosPorAssuntoId(0))
                .ReturnsAsync(new List<Dominio.Entities.SubAssunto>());

            var query = new ObterSubAssuntoPorAssuntoIdQuery(0);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
            repositorioSubAssunto.Verify(r => r.ObterSubAssuntosPorAssuntoId(0), Times.Once);
        }
    }
}

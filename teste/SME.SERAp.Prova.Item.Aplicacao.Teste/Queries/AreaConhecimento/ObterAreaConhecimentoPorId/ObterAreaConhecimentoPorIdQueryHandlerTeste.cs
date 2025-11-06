using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.AreaConhecimento
{
    public class ObterAreaConhecimentoPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAreaConhecimento> repositorio;
        private readonly ObterAreaConhecimentoPorIdQueryHandler handler;

        public ObterAreaConhecimentoPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAreaConhecimento>();
            handler = new ObterAreaConhecimentoPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterAreaConhecimentoPorIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_AreaConhecimento_Quando_Encontrado()
        {
            var areaEsperada = new Dominio.Entities.AreaConhecimento { Id = 1, Descricao = "Matemática" };

            repositorio
                .Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(areaEsperada);

            var query = new ObterAreaConhecimentoPorIdQuery(1);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(areaEsperada.Id, resultado.Id);
            Assert.Equal(areaEsperada.Descricao, resultado.Descricao);
            repositorio.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_AreaConhecimento()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(999))
                .ReturnsAsync((Dominio.Entities.AreaConhecimento)null);

            var query = new ObterAreaConhecimentoPorIdQuery (999);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterAreaConhecimentoPorIdQuery (5);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterPorIdAsync(5), Times.Once);
        }
    }
}

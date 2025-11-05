using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.AreaConhecimento
{
    public class ObterAreaConhecimentoPorLegadoIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAreaConhecimento> repositorio;
        private readonly ObterAreaConhecimentoPorLegadoIdQueryHandler handler;

        public ObterAreaConhecimentoPorLegadoIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAreaConhecimento>();
            handler = new ObterAreaConhecimentoPorLegadoIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterAreaConhecimentoPorLegadoIdQueryHandler(null));
        }


        [Fact]
        public async Task Deve_Retornar_AreaConhecimento_Quando_Encontrado()
        {
            var areaEsperada = new Dominio.Entities.AreaConhecimento { Id = 1, Descricao = "Linguagens" };

            repositorio
                .Setup(r => r.ObterAreaConhecimentoPorLegadoId(1))
                .ReturnsAsync(areaEsperada);

            var query = new ObterAreaConhecimentoPorLegadoIdQuery(1);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(areaEsperada.Id, resultado.Id);
            Assert.Equal(areaEsperada.Descricao, resultado.Descricao);
            repositorio.Verify(r => r.ObterAreaConhecimentoPorLegadoId(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_AreaConhecimento()
        {
            repositorio
                .Setup(r => r.ObterAreaConhecimentoPorLegadoId(12))
                .ReturnsAsync((Dominio.Entities.AreaConhecimento)null);

            var query = new ObterAreaConhecimentoPorLegadoIdQuery(12);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterAreaConhecimentoPorLegadoId(12), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterAreaConhecimentoPorLegadoId(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterAreaConhecimentoPorLegadoIdQuery(It.IsAny<long>());

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterAreaConhecimentoPorLegadoId(It.IsAny<long>()), Times.Once);
        }
    }
}

using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.Matriz
{
    public class ObterMatrizPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioMatriz> repositorio;
        private readonly ObterMatrizPorIdQueryHandler handler;

        public ObterMatrizPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioMatriz>();
            handler = new ObterMatrizPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new ObterMatrizPorIdQueryHandler(null));
        }

        [Fact]
        public async Task Deve_Retornar_Matriz_Quando_Encontrada()
        {
            var matrizEsperada = new Dominio.Entities.Matriz { Id = 1, Descricao = "Matriz Base" };

            repositorio
                .Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(1);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(matrizEsperada.Id, resultado.Id);
            Assert.Equal(matrizEsperada.Descricao, resultado.Descricao);
            repositorio.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_Nao_Encontrar_Matriz()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(999))
                .ReturnsAsync((Dominio.Entities.Matriz)null);

            var query = new ObterMatrizPorIdQuery(999);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorIdAsync(999), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Com_MatrizId_Correto()
        {
            var matrizEsperada = new Dominio.Entities.Matriz { Id = 5, Descricao = "Matriz Curricular" };

            repositorio
                .Setup(r => r.ObterPorIdAsync(5))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(5);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(5), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(500)]
        public async Task Deve_Passar_MatrizId_Correto_Para_Repositorio(long matrizId)
        {
            var matrizEsperada = new Dominio.Entities.Matriz { Id = matrizId, Descricao = $"Matriz {matrizId}" };

            repositorio
                .Setup(r => r.ObterPorIdAsync(matrizId))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(matrizId);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(matrizId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Matriz_Com_Todas_Propriedades()
        {
            var matrizEsperada = new Dominio.Entities.Matriz
            {
                Id = 3,
                Descricao = "Matriz Técnica",
                Modelo = "MTX-2025"
            };

            repositorio
                .Setup(r => r.ObterPorIdAsync(3))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(3);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.Id);
            Assert.Equal("Matriz Técnica", resultado.Descricao);
            Assert.Equal("MTX-2025", resultado.Modelo);
            repositorio.Verify(r => r.ObterPorIdAsync(3), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Repositorio_Falhar()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(It.IsAny<long>()))
                .ThrowsAsync(new InvalidOperationException("Falha no repositório"));

            var query = new ObterMatrizPorIdQuery(10);
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Falha no repositório", exception.Message);
            repositorio.Verify(r => r.ObterPorIdAsync(It.IsAny<long>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Repositorio_Apenas_Uma_Vez()
        {
            var matrizEsperada = new Dominio.Entities.Matriz { Id = 2, Descricao = "Matriz Humanas" };

            repositorio
                .Setup(r => r.ObterPorIdAsync(2))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(2);
            await handler.Handle(query, CancellationToken.None);

            repositorio.Verify(r => r.ObterPorIdAsync(2), Times.Once);
        }

        [Fact]
        public async Task Deve_Buscar_Matriz_Por_Id_Especifico()
        {
            var matrizEsperada = new Dominio.Entities.Matriz
            {
                Id = 10,
                Descricao = "Matriz Biologia",
                Modelo = "BIO-450"
            };

            repositorio
                .Setup(r => r.ObterPorIdAsync(10))
                .ReturnsAsync(matrizEsperada);

            var query = new ObterMatrizPorIdQuery(10);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal(10, resultado.Id);
            Assert.Equal("Matriz Biologia", resultado.Descricao);
            Assert.Equal("BIO-450", resultado.Modelo);
            repositorio.Verify(r => r.ObterPorIdAsync(10), Times.Once);
        }
    }
}

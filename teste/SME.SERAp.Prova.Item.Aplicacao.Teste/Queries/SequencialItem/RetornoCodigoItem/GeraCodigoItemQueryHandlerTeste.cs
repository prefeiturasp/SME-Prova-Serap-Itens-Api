using MediatR;
using Moq;
using SME.SERAp.Prova.Item.Dados.Interfaces;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Queries.SequencialItem
{
    public class GeraCodigoItemQueryHandlerTeste
    {
        private readonly Mock<IRepositoSequencialItem> repositorioSequencialItem;
        private readonly Mock<IMediator> mediator;
        private readonly GeraCodigoItemQueryHandler handler;

        public GeraCodigoItemQueryHandlerTeste()
        {
            repositorioSequencialItem = new Mock<IRepositoSequencialItem>();
            mediator = new Mock<IMediator>();
            handler = new GeraCodigoItemQueryHandler(repositorioSequencialItem.Object, mediator.Object);
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Repositorio_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new GeraCodigoItemQueryHandler(null, Mock.Of<IMediator>()));
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new GeraCodigoItemQueryHandler(Mock.Of<IRepositoSequencialItem>(), null));
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Com_Sequencial_Inicial_Quando_Nao_Houver_SequencialItem()
        {
            var area = new Dominio.Entities.AreaConhecimento { Codigo = 10 };
            var disciplina = new Dominio.Entities.Disciplina { Codigo = 20 };

            mediator
                .Setup(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Dominio.Entities.SequencialItem)null);

            mediator
                .Setup(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<long>());

            var query = new GeraCodigoItemQuery(area, disciplina);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal("10201", resultado);
            mediator.Verify(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Com_Sequencial_Incrementado_Quando_SequencialItem_Existir()
        {
            var area = new Dominio.Entities.AreaConhecimento { Codigo = 5 };
            var disciplina = new Dominio.Entities.Disciplina { Codigo = 8 };
            var sequencialExistente = new Dominio.Entities.SequencialItem(1, 5, 8, 9, DateTime.Now);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sequencialExistente);

            mediator
                .Setup(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<long>());

            var query = new GeraCodigoItemQuery(area, disciplina);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Equal("5810", resultado);
            mediator.Verify(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_SalvarSequencialItem_Com_Valores_Corretos()
        {
            var area = new Dominio.Entities.AreaConhecimento { Codigo = 100 };
            var disciplina = new Dominio.Entities.Disciplina { Codigo = 200 };
            var sequencialItemExistente = new Dominio.Entities.SequencialItem(10, 100, 200, 5, DateTime.UtcNow);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sequencialItemExistente);

            mediator
                .Setup(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<long>());

            var query = new GeraCodigoItemQuery(area, disciplina);
            await handler.Handle(query, CancellationToken.None);

            mediator.Verify(m => m.Send(It.Is<SalvarSequencialItemCommand>(c =>
                c.SequencialItem.CodigoAreaConhecimento == 100 &&
                c.SequencialItem.CodigoDisciplina == 200 &&
                c.SequencialItem.Sequencial == 6
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Mediator_Falhar()
        {
            var area = new Dominio.Entities.AreaConhecimento { Codigo = 1 };
            var disciplina = new Dominio.Entities.Disciplina { Codigo = 2 };

            mediator
                .Setup(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("Erro no Mediator"));

            var query = new GeraCodigoItemQuery(area, disciplina);

            var excecao = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.Handle(query, CancellationToken.None));

            Assert.Equal("Erro no Mediator", excecao.Message);
            mediator.Verify(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Theory]
        [InlineData(10, 20, 0, "10201")]
        [InlineData(11, 22, 5, "11226")]
        [InlineData(1, 2, 9, "1210")]
        public async Task Deve_Gerar_CodigoItem_Corretamente_Para_Diferentes_Entradas(long areaCodigo, long disciplinaCodigo, long sequencialAtual, string codigoEsperado)
        {
            var area = new Dominio.Entities.AreaConhecimento { Codigo = areaCodigo };
            var disciplina = new Dominio.Entities.Disciplina { Codigo = disciplinaCodigo };

            Dominio.Entities.SequencialItem sequencial = sequencialAtual == 0 ? null :
                new Dominio.Entities.SequencialItem(1, areaCodigo, disciplinaCodigo, sequencialAtual, DateTime.Now);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sequencial);

            mediator
                .Setup(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(It.IsAny<long>());

            var query = new GeraCodigoItemQuery(area, disciplina);
            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(codigoEsperado, resultado);
            mediator.Verify(m => m.Send(It.IsAny<ObterSequencialItemPorAreaEDisciplinaQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediator.Verify(m => m.Send(It.IsAny<SalvarSequencialItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

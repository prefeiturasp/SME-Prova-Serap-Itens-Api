using Moq;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alterantiva;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Commands.PublicarFilaRabbit;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterUltimaVersaoItemPorCodigo;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Enums;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    using DominioItem = SME.SERAp.Prova.Item.Dominio.Entities.Item;

    public class SalvarItemUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly SalvarItemUseCase useCase;
        private const long AreaConhecimentoIdValido = 10L;
        private const long DisciplinaIdValido = 20L;
        private const long ItemIdValido = 100L;
        private const string CodigoItemGerado = "AC10-DISC20-001";
        private const string CodigoItemExistente = "AC10-DISC20-001";

        public SalvarItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new SalvarItemUseCase(mediatorMock.Object);
        }

        private AreaConhecimento ObterAreaConhecimentoMock()
        {
            return new AreaConhecimento(AreaConhecimentoIdValido, 1, "Matemática", StatusGeral.Ativo)
            {
                Codigo = 10
            };
        }

        private Disciplina ObterDisciplinaMock()
        {
            return new Disciplina(DisciplinaIdValido, 2, AreaConhecimentoIdValido, "Álgebra", "Fundamental", StatusGeral.Ativo)
            {
                Codigo = 20
            };
        }

        private ItemDto ObterItemDtoNovoMock()
        {
            return new ItemDto
            {
                Id = null,
                CodigoItem = null,
                AreaConhecimentoId = AreaConhecimentoIdValido,
                DisciplinaId = DisciplinaIdValido,
                MatrizId = 30,
                CompetenciaId = 40,
                HabilidadeId = 50,
                AnoMatrizId = 60,
                DificuldadeSugeridaId = 70,
                Situacao = SituacaoItem.Ativo,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 4,
                PalavrasChave = new[] { "algebra", "equação" },
                Enunciado = "Resolva a equação",
                TextoBase = "Base do item",
                Fonte = "Fonte teste"
            };
        }

        private ItemDto ObterItemDtoNovaVersaoMock(string codigoItem = CodigoItemExistente)
        {
            return new ItemDto
            {
                Id = null,
                CodigoItem = codigoItem,
                AreaConhecimentoId = AreaConhecimentoIdValido,
                DisciplinaId = DisciplinaIdValido,
                MatrizId = 30,
                CompetenciaId = 40,
                HabilidadeId = 50,
                AnoMatrizId = 60,
                DificuldadeSugeridaId = 70,
                Situacao = SituacaoItem.Ativo,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 4,
                PalavrasChave = new[] { "algebra", "equação" },
                Enunciado = "Resolva a equação atualizada",
                TextoBase = "Base do item atualizada",
                Fonte = "Fonte teste"
            };
        }

        private DominioItem ObterUltimaVersaoItemMock(long versaoItem = 1)
        {
            var item = new DominioItem(
                CodigoItemExistente,
                AreaConhecimentoIdValido, DisciplinaIdValido,
                30, 40, 50, 60, 70,
                null, null, null, null, null,
                SituacaoItem.Ativo, TipoItem.Dicotômico,
                4, "algebra;equação",
                null, null, null, null, null,
                versaoItem, null, null, "Resolva a equação");

            item.Id = ItemIdValido;
            item.DataCriacao = DateTime.Now;
            item.DataAlteracao = DateTime.Now;

            return item;
        }

        private List<AltenativaDto> ObterAlternativasDtoMock()
        {
            return new List<AltenativaDto>
            {
                new AltenativaDto { Id = 0, Descricao = "Alternativa A", Numeracao = "A", Ordem = 1, Correta = true, Justificativa = "Correta" },
                new AltenativaDto { Id = 0, Descricao = "Alternativa B", Numeracao = "B", Ordem = 2, Correta = false, Justificativa = "Incorreta" }
            };
        }

        private void ConfigurarMocksPadrao(AreaConhecimento areaConhecimento, Disciplina disciplina)
        {
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);
            mediatorMock.Setup(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new SalvarItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Salvar_Item_Novo_Com_Sucesso_E_Retornar_ItemId()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.VersaoItem == 1 && cmd.Item.DataCriacao != default(DateTime)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Definir_VersaoItem_Como_1_Para_Item_Novo()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            Assert.Equal(1, itemDto.VersaoItem);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_CodigoItem_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.CodigoItem = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            Assert.Equal(CodigoItemGerado, itemDto.CodigoItem);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_CodigoItem_For_Vazio()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.CodigoItem = string.Empty;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            Assert.Equal(CodigoItemGerado, itemDto.CodigoItem);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Criar_Nova_Versao_Quando_CodigoItem_Existir_No_Banco()
        {
            var itemDto = ObterItemDtoNovaVersaoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersao = ObterUltimaVersaoItemMock(versaoItem: 1);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersao);

            await useCase.Executar(itemDto);

            Assert.Equal(2, itemDto.VersaoItem);
            Assert.Null(itemDto.Id);
            Assert.Equal(CodigoItemExistente, itemDto.CodigoItem);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.VersaoItem == 2 && cmd.Item.DataCriacao != default(DateTime)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Incrementar_VersaoItem_Baseado_Na_Ultima_Versao_Independente_Do_Valor_Enviado()
        {
            var itemDto = ObterItemDtoNovaVersaoMock();
            itemDto.VersaoItem = 99;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersao = ObterUltimaVersaoItemMock(versaoItem: 3);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersao);

            await useCase.Executar(itemDto);

            Assert.Equal(4, itemDto.VersaoItem);
        }

        [Fact]
        public async Task Deve_Preservar_CodigoItem_Original_Ignorando_Alteracoes_No_Dto()
        {
            var itemDto = ObterItemDtoNovaVersaoMock();
            itemDto.CodigoItem = "CODIGO-ALTERADO";
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersao = ObterUltimaVersaoItemMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersao);

            await useCase.Executar(itemDto);

            Assert.Equal(CodigoItemExistente, itemDto.CodigoItem);
        }

        [Fact]
        public async Task Deve_Criar_Item_Novo_Com_Codigo_Informado_Quando_Nao_Existir_No_Banco()
        {
            var codigoInexistente = "CODIGO-INEXISTENTE";
            var itemDto = ObterItemDtoNovaVersaoMock(codigoInexistente);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);

            await useCase.Executar(itemDto);

            Assert.Equal(1, itemDto.VersaoItem);
            Assert.Null(itemDto.Id);
            Assert.Equal(codigoInexistente, itemDto.CodigoItem);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.VersaoItem == 1 && cmd.Item.DataCriacao != default(DateTime)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Chamar_ObterUltimaVersao_Quando_CodigoItem_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.CodigoItem = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_AreaConhecimento_Nao_For_Encontrada()
        {
            var itemDto = ObterItemDtoNovoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((AreaConhecimento)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"A area de conhecimento com o id: {itemDto.AreaConhecimentoId} não foi encontrada", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Disciplina_Nao_For_Encontrada()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((Disciplina)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"A disciplina com o id: {itemDto.DisciplinaId} não foi encontrada", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_Alternativas_Quando_AlternativasDto_Nao_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Nao_Deve_Salvar_Alternativas_Quando_AlternativasDto_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.AlternativasDto = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Maior_Que_Zero()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.ArquivoAudioId = 500;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Zero()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.ArquivoAudioId = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_ItemVideo_Quando_ArquivoVideoId_For_Maior_Que_Zero()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemVideo_Quando_ArquivoVideoId_For_Zero()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.ArquivoVideoId = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Publicar_Na_Fila_Apos_Salvar_Item_Novo()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Publicar_Na_Fila_Apos_Criar_Nova_Versao()
        {
            var itemDto = ObterItemDtoNovaVersaoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersao = ObterUltimaVersaoItemMock(versaoItem: 1);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersao);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Salvar_Item_Alternativas_Audio_E_Video_Em_Fluxo_Completo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            itemDto.ArquivoAudioId = 500;
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.VersaoItem == 1 && cmd.Item.DataCriacao != default(DateTime)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Criar_Nova_Versao_Com_Alternativas_Audio_E_Video_Em_Fluxo_Completo()
        {
            var itemDto = ObterItemDtoNovaVersaoMock();
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            itemDto.ArquivoAudioId = 500;
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersao = ObterUltimaVersaoItemMock(versaoItem: 2);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersao);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            Assert.Equal(3, itemDto.VersaoItem);
            Assert.Equal(CodigoItemExistente, itemDto.CodigoItem);
            Assert.Null(itemDto.Id);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.VersaoItem == 3 && cmd.Item.DataCriacao != default(DateTime)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
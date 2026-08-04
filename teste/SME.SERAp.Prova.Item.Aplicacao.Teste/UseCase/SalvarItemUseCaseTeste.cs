using Moq;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alterantiva;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.RemoverAlternativasAusentesDto;
using SME.SERAp.Prova.Item.Aplicacao.Commands.ItemVersao;
using SME.SERAp.Prova.Item.Aplicacao.Commands.PublicarFilaRabbit;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Rascunho;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoNovaVersaoPorCodigo;
using SME.SERAp.Prova.Item.Aplicacao.Queries.Item.ObterRascunhoPorCodigo;
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

        private const long AreaConhecimentoIdValido = 21L;
        private const long DisciplinaIdValido = 63L;
        private const long MatrizIdValido = 23L;
        private const long CompetenciaIdValido = 1913L;
        private const long HabilidadeIdValido = 5821L;
        private const long AnoMatrizIdValido = 85L;
        private const long DificuldadeSugeridaIdValido = 1L;
        private const long SubAssuntoIdValido = 38L;
        private const long ItemIdValido = 546L;
        private const long ItemIdRascunho = 614L;
        private const string CodigoItemGerado = "AC21-DISC63-001";
        private const string CodigoItemExistente = "2199";

        public SalvarItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new SalvarItemUseCase(mediatorMock.Object);
        }

        private AreaConhecimento ObterAreaConhecimentoMock()
        {
            return new AreaConhecimento(AreaConhecimentoIdValido, 1, "Matemática", StatusGeral.Ativo)
            {
                Codigo = 21
            };
        }

        private Disciplina ObterDisciplinaMock()
        {
            return new Disciplina(DisciplinaIdValido, 2, AreaConhecimentoIdValido, "Álgebra", "Fundamental", StatusGeral.Ativo)
            {
                Codigo = 63
            };
        }

        private ItemDto ObterItemDtoBase(SituacaoItem situacao, string codigoItem = null, long? id = null, long versaoItem = 0)
        {
            return new ItemDto
            {
                Id = id,
                CodigoItem = codigoItem,
                AreaConhecimentoId = AreaConhecimentoIdValido,
                DisciplinaId = DisciplinaIdValido,
                MatrizId = MatrizIdValido,
                CompetenciaId = CompetenciaIdValido,
                HabilidadeId = HabilidadeIdValido,
                AnoMatrizId = AnoMatrizIdValido,
                DificuldadeSugeridaId = DificuldadeSugeridaIdValido,
                Discriminacao = 1.000m,
                AcertoCasual = null,
                Dificuldade = 2.000m,
                AssuntoId = null,
                SubAssuntoId = SubAssuntoIdValido,
                Situacao = situacao,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 23,
                PalavrasChave = new[] { "teste" },
                ParametroBTransformado = null,
                MediaEhDesvio = null,
                Observacao = null,
                SentencaDescritora = null,
                NivelItem = null,
                VersaoItem = versaoItem,
                TextoBase = null,
                Fonte = null,
                Enunciado = "<p>teste</p>",
                AlternativasDto = ObterAlternativasDtoMock(),
                ArquivoVideoId = 0,
                ArquivoAudioId = 0
            };
        }

        private DominioItem ObterDominioItemMock(long id, string codigoItem, SituacaoItem situacao, long versaoItem)
        {
            var item = new DominioItem(
                codigoItem,
                AreaConhecimentoIdValido, DisciplinaIdValido,
                MatrizIdValido, CompetenciaIdValido, HabilidadeIdValido, AnoMatrizIdValido, DificuldadeSugeridaIdValido,
                1.000m, null, 2.000m, null, SubAssuntoIdValido,
                situacao, TipoItem.Dicotômico,
                23, "teste",
                null, null, null, null, null,
                versaoItem, null, null, "<p>teste</p>");

            item.Id = id;
            item.DataCriacao = DateTime.Now;
            item.DataAlteracao = DateTime.Now;
            return item;
        }

        private List<AltenativaDto> ObterAlternativasDtoMock()
        {
            return new List<AltenativaDto>
            {
                new AltenativaDto { Id = 0, Descricao = "Alternativa A", Justificativa = "Justificativa A", Numeracao = "A", Correta = true, Ordem = 1 },
                new AltenativaDto { Id = 0, Descricao = "Alternativa B", Justificativa = "Justificativa B", Numeracao = "B", Correta = false, Ordem = 2 }
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
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<InativarVersoesAnterioresItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<InativarRascunhoPorCodigoItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
            mediatorMock.Setup(m => m.Send(It.IsAny<RemoverAlternativasAusentesDtoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new SalvarItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_AreaConhecimento_Nao_For_Encontrada()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho);
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
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho);
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
        public async Task TrataRascunho_Deve_Criar_Novo_Rascunho_Quando_Id_E_CodigoItem_Sao_Nulos()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: null, id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoNovaVersaoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            Assert.Equal(CodigoItemGerado, itemDto.CodigoItem);
            Assert.Equal(0, itemDto.VersaoItem);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.Situacao == SituacaoItem.Rascunho), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataRascunho_Deve_Lancar_Exception_Se_Ja_Existe_Rascunho_Nova_Versao_Para_CodigoItem()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: CodigoItemExistente, id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var rascunhoExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 2);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoNovaVersaoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(rascunhoExistente);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"Já existe um rascunho de nova versão para o item {itemDto.CodigoItem}. Envie o id do rascunho para editá-lo.", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataRascunho_Deve_Atualizar_Rascunho_Existente()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: CodigoItemExistente, id: ItemIdRascunho);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var itemExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 0);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.Is<ObterItemPorIdQuery>(q => q.Id == ItemIdRascunho), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemExistente);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == ItemIdRascunho && cmd.Item.Situacao == SituacaoItem.Rascunho), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataRascunho_Deve_Lancar_Exception_Ao_Atualizar_Item_Nao_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: CodigoItemExistente, id: ItemIdRascunho);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var itemExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Ativo, 1);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.Is<ObterItemPorIdQuery>(q => q.Id == ItemIdRascunho), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemExistente);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains("Não é permitido atualizar um item que não está em situação de rascunho.", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataRascunho_Deve_Lancar_Exception_Se_CodigoItem_Nao_Corresponde_Ao_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: "CODIGO-DIFERENTE", id: ItemIdRascunho);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var itemExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 0);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.Is<ObterItemPorIdQuery>(q => q.Id == ItemIdRascunho), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemExistente);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains("O código do item informado não corresponde ao rascunho encontrado.", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataNovaVersao_Deve_Lancar_Exception_Quando_CodigoItem_For_Nulo_Ou_Vazio()
        {
            var itemDtoNulo = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: null);
            var itemDtoVazio = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: string.Empty);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);

            var exceptionNulo = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDtoNulo));
            Assert.Contains("Não é possível ativar um item sem informar o código.", exceptionNulo.Message);

            var exceptionVazio = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDtoVazio));
            Assert.Contains("Não é possível ativar um item sem informar o código.", exceptionVazio.Message);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataNovaVersao_Deve_Ativar_Rascunho_Inicial_Quando_Nao_Ha_Versao_Ativa_E_Existe_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: CodigoItemExistente, id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var rascunhoExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 0);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(rascunhoExistente);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == ItemIdRascunho && cmd.Item.Situacao == SituacaoItem.Ativo && cmd.Item.VersaoItem == 1), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<InativarVersoesAnterioresItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<InativarRascunhoPorCodigoItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TrataNovaVersao_Deve_Ativar_Rascunho_Nova_Versao_Quando_Ha_Versao_Ativa_E_Existe_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: CodigoItemExistente, id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersaoAtiva = ObterDominioItemMock(ItemIdValido, CodigoItemExistente, SituacaoItem.Ativo, 1);
            var rascunhoNovaVersao = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 2);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersaoAtiva);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(rascunhoNovaVersao);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == ItemIdRascunho && cmd.Item.Situacao == SituacaoItem.Ativo && cmd.Item.VersaoItem == 2), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<InativarVersoesAnterioresItemCommand>(cmd => cmd.CodigoItem == CodigoItemExistente && cmd.VersaoAtual == 2), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<InativarRascunhoPorCodigoItemCommand>(cmd => cmd.CodigoItem == CodigoItemExistente), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TrataNovaVersao_Deve_Criar_Novo_Rascunho_Para_Edicao_Quando_Ha_Versao_Ativa_E_Nao_Ha_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: CodigoItemExistente, id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersaoAtiva = ObterDominioItemMock(ItemIdValido, CodigoItemExistente, SituacaoItem.Ativo, 1);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersaoAtiva);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.Situacao == SituacaoItem.Rascunho && cmd.Item.VersaoItem == 2), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<InativarVersoesAnterioresItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<InativarRascunhoPorCodigoItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TrataNovaVersao_Deve_Lancar_Exception_Quando_Nenhum_Item_Ou_Rascunho_Encontrado()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: "CODIGO-INEXISTENTE", id: null);
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync((DominioItem)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"Nenhum item ou rascunho encontrado com o código {itemDto.CodigoItem}.", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_Alternativas_Quando_AlternativasDto_Nao_For_Nulo_No_Fluxo_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(itemDto.AlternativasDto.Count));
        }

        [Fact]
        public async Task Nao_Deve_Salvar_Alternativas_Quando_AlternativasDto_For_Nulo_No_Fluxo_Rascunho()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
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
        public async Task Deve_Remover_E_Salvar_Alternativas_Ao_Atualizar_Rascunho_Existente()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, codigoItem: CodigoItemExistente, id: ItemIdRascunho);
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            itemDto.AlternativasDto.First().Id = 10L;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var itemExistente = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 0);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.Is<ObterItemPorIdQuery>(q => q.Id == ItemIdRascunho), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(itemExistente);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.Is<RemoverAlternativasAusentesDtoCommand>(cmd => cmd.ItemId == ItemIdValido && cmd.IdsAlternativasManter.Contains(10L)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(itemDto.AlternativasDto.Count));
        }

        [Fact]
        public async Task Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Maior_Que_Zero()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
            itemDto.ArquivoAudioId = 500;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Zero()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
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
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemVideo_Quando_ArquivoVideoId_For_Zero()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
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
        public async Task Deve_Salvar_Item_Rascunho_Com_Alternativas_Audio_E_Video_Em_Fluxo_Completo()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Rascunho, id: null);
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            itemDto.ArquivoAudioId = 500;
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == 0 && cmd.Item.Situacao == SituacaoItem.Rascunho && cmd.Item.VersaoItem == 0), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(itemDto.AlternativasDto.Count));
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Ativar_Rascunho_Com_Alternativas_Audio_E_Video_Em_Fluxo_Completo()
        {
            var itemDto = ObterItemDtoBase(SituacaoItem.Ativo, codigoItem: CodigoItemExistente, id: null);
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            itemDto.AlternativasDto.First().Id = 10L;
            itemDto.ArquivoAudioId = 500;
            itemDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();
            var ultimaVersaoAtiva = ObterDominioItemMock(ItemIdValido, CodigoItemExistente, SituacaoItem.Ativo, 1);
            var rascunhoNovaVersao = ObterDominioItemMock(ItemIdRascunho, CodigoItemExistente, SituacaoItem.Rascunho, 2);

            ConfigurarMocksPadrao(areaConhecimento, disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterUltimaVersaoItemPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ultimaVersaoAtiva);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterRascunhoPorCodigoQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(rascunhoNovaVersao);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.Is<SalvarItemCommand>(cmd => cmd.Item.Id == ItemIdRascunho && cmd.Item.Situacao == SituacaoItem.Ativo && cmd.Item.VersaoItem == 2), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<RemoverAlternativasAusentesDtoCommand>(cmd => cmd.ItemId == ItemIdValido && cmd.IdsAlternativasManter.Contains(10L)), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(itemDto.AlternativasDto.Count));
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<InativarVersoesAnterioresItemCommand>(cmd => cmd.CodigoItem == CodigoItemExistente && cmd.VersaoAtual == 2), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<InativarRascunhoPorCodigoItemCommand>(cmd => cmd.CodigoItem == CodigoItemExistente), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<PublicaFilaRabbitCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
using Moq;
using Xunit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alterantiva;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
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

        private ItemDto ObterItemDtoExistenteMock()
        {
            return new ItemDto
            {
                Id = ItemIdValido,
                CodigoItem = "AC10-DISC20-001",
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

        private List<AltenativaDto> ObterAlternativasDtoMock()
        {
            return new List<AltenativaDto>
            {
                new AltenativaDto { Id = 0, Descricao = "Alternativa A", Numeracao = "A", Ordem = 1, Correta = true, Justificativa = "Correta" },
                new AltenativaDto { Id = 0, Descricao = "Alternativa B", Numeracao = "B", Ordem = 2, Correta = false, Justificativa = "Incorreta" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new SalvarItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Salvar_Item_Novo_Com_Sucesso_E_Retornar_ItemId()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

            var resultado = await useCase.Executar(itemDto);

            Assert.Equal(ItemIdValido, resultado);

            mediatorMock.Verify(m => m.Send(It.Is<ObterAreaConhecimentoPorIdQuery>(q => q.Id == AreaConhecimentoIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterDisciplinaPorIdQuery>(q => q.Id == DisciplinaIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_AreaConhecimento_Nao_For_Encontrada()
        {
            var itemDto = ObterItemDtoNovoMock();
            AreaConhecimento areaNula = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaNula);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"A area de conhecimento com o id: {itemDto.AreaConhecimentoId} não foi encontrada", exception.Message);

            mediatorMock.Verify(m => m.Send(It.Is<ObterAreaConhecimentoPorIdQuery>(q => q.Id == AreaConhecimentoIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Disciplina_Nao_For_Encontrada()
        {
            var itemDto = ObterItemDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            Disciplina disciplinaNula = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplinaNula);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemDto));

            Assert.Contains($"A disciplina com o id: {itemDto.DisciplinaId} não foi encontrada", exception.Message);

            mediatorMock.Verify(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_Id_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.Id = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(CodigoItemGerado, itemDto.CodigoItem);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_Id_For_Zero()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.Id = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Gerar_CodigoItem_Quando_Id_For_Maior_Que_Zero()
        {
            var itemDto = ObterItemDtoExistenteMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_Alternativas_Quando_AlternativasDto_Nao_For_Nulo()
        {
            var itemDto = ObterItemDtoNovoMock();
            itemDto.AlternativasDto = ObterAlternativasDtoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);
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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);
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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);
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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Never);
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

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(ItemIdValido);
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
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
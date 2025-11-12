using MediatR;
using Moq;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class SalvarRascunhoItemUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly SalvarRascunhoItemUseCase useCase;
        private const long AreaConhecimentoIdValido = 10L;
        private const long DisciplinaIdValido = 20L;
        private const long ItemIdValido = 100L;
        private const string CodigoItemGerado = "AC10-DISC20-001";

        public SalvarRascunhoItemUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new SalvarRascunhoItemUseCase(mediatorMock.Object);
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

        private ItemRascunhoDto ObterItemRascunhoDtoNovoMock()
        {
            return new ItemRascunhoDto
            {
                Id = null,
                AreaConhecimentoId = AreaConhecimentoIdValido,
                DisciplinaId = DisciplinaIdValido,
                MatrizId = 30,
                CompetenciaId = 40,
                HabilidadeId = 50,
                AnoMatrizId = 60,
                DificuldadeSugeridaId = 70,
                Situacao = SituacaoItem.Rascunho,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 4,
                PalavrasChave = new[] { "algebra", "equação" },
                Enunciado = "Resolva a equação",
                TextoBase = "Base do item",
                Fonte = "Fonte teste",
                ArquivoAudioId = 0,
                ArquivoVideoId = 0
            };
        }

        private ItemRascunhoDto ObterItemRascunhoDtoExistenteMock()
        {
            return new ItemRascunhoDto
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
                Situacao = SituacaoItem.Rascunho,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 4,
                PalavrasChave = new[] { "algebra", "equação" },
                Enunciado = "Resolva a equação",
                TextoBase = "Base do item",
                Fonte = "Fonte teste",
                ArquivoAudioId = 0,
                ArquivoVideoId = 0
            };
        }

        private List<AlternativaRascunhoDto> ObterAlternativasRascunhoDtoMock()
        {
            return new List<AlternativaRascunhoDto>
            {
                new AlternativaRascunhoDto { Id = null, Descricao = "Alternativa A", Numeracao = "A", Ordem = 1, Correta = true, Justificativa = "Correta" },
                new AlternativaRascunhoDto { Id = null, Descricao = "Alternativa B", Numeracao = "B", Ordem = 2, Correta = false, Justificativa = "Incorreta" }
            };
        }

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() => new SalvarRascunhoItemUseCase(null));
        }

        [Fact]
        public async Task Deve_Salvar_Rascunho_Item_Novo_Com_Sucesso_E_Retornar_ItemId()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            var resultado = await useCase.Executar(itemRascunhoDto);

            Assert.Equal(ItemIdValido, resultado);
            mediatorMock.Verify(m => m.Send(It.Is<ObterAreaConhecimentoPorIdQuery>(q => q.Id == AreaConhecimentoIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.Is<ObterDisciplinaPorIdQuery>(q => q.Id == DisciplinaIdValido), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_AreaConhecimento_Nao_For_Encontrada()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((AreaConhecimento)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemRascunhoDto));

            Assert.Contains($"A area de conhecimento com o id: {itemRascunhoDto.AreaConhecimentoId} não foi encontrada", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Disciplina_Nao_For_Encontrada()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((Disciplina)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemRascunhoDto));

            Assert.Contains($"A disciplina com o id: {itemRascunhoDto.DisciplinaId} não foi encontrada", exception.Message);
            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_Id_For_Nulo()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.Id = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(CodigoItemGerado, itemRascunhoDto.CodigoItem);
        }

        [Fact]
        public async Task Deve_Gerar_CodigoItem_Quando_Id_For_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.Id = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Gerar_CodigoItem_Quando_Id_For_Maior_Que_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoExistenteMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Lancar_Exception_Quando_Id_Maior_Que_Zero_E_CodigoItem_For_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoExistenteMock();
            itemRascunhoDto.CodigoItem = "0";
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);

            var exception = await Assert.ThrowsAsync<Exception>(() => useCase.Executar(itemRascunhoDto));

            Assert.Contains("O codigo do item não pode ser zero, pois o item já existe na base de dados", exception.Message);
        }

        [Fact]
        public async Task Deve_Salvar_Alternativas_Quando_AlternativasDto_Nao_For_Nulo()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.AlternativasDto = ObterAlternativasRascunhoDtoMock();
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Nao_Deve_Salvar_Alternativas_Quando_AlternativasDto_For_Nulo()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.AlternativasDto = null;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Maior_Que_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.ArquivoAudioId = 500;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemAudio_Quando_ArquivoAudioId_For_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.ArquivoAudioId = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_ItemVideo_Quando_ArquivoVideoId_For_Maior_Que_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Salvar_ItemVideo_Quando_ArquivoVideoId_For_Zero()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.ArquivoVideoId = 0;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);

            await useCase.Executar(itemRascunhoDto);

            mediatorMock.Verify(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Deve_Salvar_Item_Alternativas_Audio_E_Video_Em_Fluxo_Completo()
        {
            var itemRascunhoDto = ObterItemRascunhoDtoNovoMock();
            itemRascunhoDto.AlternativasDto = ObterAlternativasRascunhoDtoMock();
            itemRascunhoDto.ArquivoAudioId = 500;
            itemRascunhoDto.ArquivoVideoId = 600;
            var areaConhecimento = ObterAreaConhecimentoMock();
            var disciplina = ObterDisciplinaMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<ObterAreaConhecimentoPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(areaConhecimento);
            mediatorMock.Setup(m => m.Send(It.IsAny<ObterDisciplinaPorIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(disciplina);
            mediatorMock.Setup(m => m.Send(It.IsAny<GeraCodigoItemQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(CodigoItemGerado);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(ItemIdValido);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarAlternativaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemAudioCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);
            mediatorMock.Setup(m => m.Send(It.IsAny<SalvarItemVideoCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1L);

            var resultado = await useCase.Executar(itemRascunhoDto);

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
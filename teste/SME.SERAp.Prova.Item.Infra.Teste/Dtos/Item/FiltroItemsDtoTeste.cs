using SME.SERAp.Prova.Item.Infra.Dtos;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class FiltroItemsDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var areaConhecimentoId = 10L;
            var codigoItem = "ITEM-001";
            var disciplinaId = 20L;
            var matrizId = 30L;
            var competenciaId = 40L;
            var dificuldadeSugeridaId = 50L;
            var situacao = 1L;
            var habilidadeId = 60L;
            var pagina = 1;
            var tamanhoPagina = 10;

            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = areaConhecimentoId,
                CodigoItem = codigoItem,
                DisciplinaId = disciplinaId,
                MatrizId = matrizId,
                CompetenciaId = competenciaId,
                DificuldadeSugeridaId = dificuldadeSugeridaId,
                Situacao = situacao,
                HabilidadeId = habilidadeId,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina
            };

            Assert.Equal(areaConhecimentoId, dto.AreaConhecimentoId);
            Assert.Equal(codigoItem, dto.CodigoItem);
            Assert.Equal(disciplinaId, dto.DisciplinaId);
            Assert.Equal(matrizId, dto.MatrizId);
            Assert.Equal(competenciaId, dto.CompetenciaId);
            Assert.Equal(dificuldadeSugeridaId, dto.DificuldadeSugeridaId);
            Assert.Equal(situacao, dto.Situacao);
            Assert.Equal(habilidadeId, dto.HabilidadeId);
            Assert.Equal(pagina, dto.Pagina);
            Assert.Equal(tamanhoPagina, dto.TamanhoPagina);
        }

        [Fact]
        public void Deve_Criar_FiltroItemsDto_Vazio()
        {
            var dto = new FiltroItemsDto();

            Assert.Null(dto.AreaConhecimentoId);
            Assert.Null(dto.CodigoItem);
            Assert.Null(dto.DisciplinaId);
            Assert.Null(dto.MatrizId);
            Assert.Null(dto.CompetenciaId);
            Assert.Null(dto.DificuldadeSugeridaId);
            Assert.Null(dto.Situacao);
            Assert.Null(dto.HabilidadeId);
            Assert.Null(dto.Pagina);
            Assert.Null(dto.TamanhoPagina);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_AreaConhecimentoId()
        {
            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = 5L
            };

            Assert.Equal(5L, dto.AreaConhecimentoId);
            Assert.Null(dto.DisciplinaId);
            Assert.Null(dto.MatrizId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_CodigoItem()
        {
            var dto = new FiltroItemsDto
            {
                CodigoItem = "ITEM-123"
            };

            Assert.Equal("ITEM-123", dto.CodigoItem);
            Assert.Null(dto.DisciplinaId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_DisciplinaId()
        {
            var dto = new FiltroItemsDto
            {
                DisciplinaId = 15L
            };

            Assert.Equal(15L, dto.DisciplinaId);
            Assert.Null(dto.AreaConhecimentoId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_MatrizId()
        {
            var dto = new FiltroItemsDto
            {
                MatrizId = 25L
            };

            Assert.Equal(25L, dto.MatrizId);
            Assert.Null(dto.CompetenciaId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_CompetenciaId()
        {
            var dto = new FiltroItemsDto
            {
                CompetenciaId = 35L
            };

            Assert.Equal(35L, dto.CompetenciaId);
            Assert.Null(dto.HabilidadeId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_DificuldadeSugeridaId()
        {
            var dto = new FiltroItemsDto
            {
                DificuldadeSugeridaId = 3L
            };

            Assert.Equal(3L, dto.DificuldadeSugeridaId);
            Assert.Null(dto.Situacao);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_Situacao()
        {
            var dto = new FiltroItemsDto
            {
                Situacao = 1L
            };

            Assert.Equal(1L, dto.Situacao);
            Assert.Null(dto.DificuldadeSugeridaId);
        }

        [Fact]
        public void Deve_Filtrar_Apenas_Por_HabilidadeId()
        {
            var dto = new FiltroItemsDto
            {
                HabilidadeId = 45L
            };

            Assert.Equal(45L, dto.HabilidadeId);
            Assert.Null(dto.CompetenciaId);
        }

        [Fact]
        public void Deve_Filtrar_Com_Multiplos_Criterios()
        {
            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = 10L,
                DisciplinaId = 20L,
                MatrizId = 30L,
                Situacao = 1L
            };

            Assert.Equal(10L, dto.AreaConhecimentoId);
            Assert.Equal(20L, dto.DisciplinaId);
            Assert.Equal(30L, dto.MatrizId);
            Assert.Equal(1L, dto.Situacao);
            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Definir_Paginacao_Primeira_Pagina()
        {
            var dto = new FiltroItemsDto
            {
                Pagina = 1,
                TamanhoPagina = 10
            };

            Assert.Equal(1, dto.Pagina);
            Assert.Equal(10, dto.TamanhoPagina);
        }

        [Fact]
        public void Deve_Definir_Paginacao_Sem_Pagina()
        {
            var dto = new FiltroItemsDto
            {
                TamanhoPagina = 20
            };

            Assert.Null(dto.Pagina);
            Assert.Equal(20, dto.TamanhoPagina);
        }

        [Fact]
        public void Deve_Definir_Paginacao_Sem_TamanhoPagina()
        {
            var dto = new FiltroItemsDto
            {
                Pagina = 5
            };

            Assert.Equal(5, dto.Pagina);
            Assert.Null(dto.TamanhoPagina);
        }

        [Fact]
        public void Deve_Filtrar_Com_Codigo_Item_Vazio()
        {
            var dto = new FiltroItemsDto
            {
                CodigoItem = string.Empty
            };

            Assert.Equal(string.Empty, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Filtrar_Com_Diferentes_Tamanhos_Pagina()
        {
            var dto10 = new FiltroItemsDto { Pagina = 1, TamanhoPagina = 10 };
            var dto25 = new FiltroItemsDto { Pagina = 1, TamanhoPagina = 25 };
            var dto50 = new FiltroItemsDto { Pagina = 1, TamanhoPagina = 50 };
            var dto100 = new FiltroItemsDto { Pagina = 1, TamanhoPagina = 100 };

            Assert.Equal(10, dto10.TamanhoPagina);
            Assert.Equal(25, dto25.TamanhoPagina);
            Assert.Equal(50, dto50.TamanhoPagina);
            Assert.Equal(100, dto100.TamanhoPagina);
        }

        [Fact]
        public void Deve_Filtrar_Hierarquia_Completa()
        {
            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = 1L,
                DisciplinaId = 2L,
                MatrizId = 3L,
                CompetenciaId = 4L,
                HabilidadeId = 5L
            };

            Assert.Equal(1L, dto.AreaConhecimentoId);
            Assert.Equal(2L, dto.DisciplinaId);
            Assert.Equal(3L, dto.MatrizId);
            Assert.Equal(4L, dto.CompetenciaId);
            Assert.Equal(5L, dto.HabilidadeId);
        }

        [Fact]
        public void Deve_Permitir_Modificacao_Das_Propriedades()
        {
            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = 10L,
                Pagina = 1
            };

            Assert.Equal(10L, dto.AreaConhecimentoId);
            Assert.Equal(1, dto.Pagina);

            dto.AreaConhecimentoId = 20L;
            dto.Pagina = 2;

            Assert.Equal(20L, dto.AreaConhecimentoId);
            Assert.Equal(2, dto.Pagina);
        }

        [Fact]
        public void Deve_Filtrar_Com_Codigo_Item_Com_Diferentes_Formatos()
        {
            var dto1 = new FiltroItemsDto { CodigoItem = "ITEM-001" };
            var dto2 = new FiltroItemsDto { CodigoItem = "IT_2024_001" };
            var dto3 = new FiltroItemsDto { CodigoItem = "123456" };

            Assert.Equal("ITEM-001", dto1.CodigoItem);
            Assert.Equal("IT_2024_001", dto2.CodigoItem);
            Assert.Equal("123456", dto3.CodigoItem);
        }

        [Fact]
        public void Deve_Filtrar_Com_Todas_Propriedades_E_Paginacao()
        {
            var dto = new FiltroItemsDto
            {
                AreaConhecimentoId = 1L,
                CodigoItem = "ITEM-999",
                DisciplinaId = 2L,
                MatrizId = 3L,
                CompetenciaId = 4L,
                DificuldadeSugeridaId = 5L,
                Situacao = 1L,
                HabilidadeId = 6L,
                Pagina = 2,
                TamanhoPagina = 25
            };

            Assert.Equal(1L, dto.AreaConhecimentoId);
            Assert.Equal("ITEM-999", dto.CodigoItem);
            Assert.Equal(2L, dto.DisciplinaId);
            Assert.Equal(3L, dto.MatrizId);
            Assert.Equal(4L, dto.CompetenciaId);
            Assert.Equal(5L, dto.DificuldadeSugeridaId);
            Assert.Equal(1L, dto.Situacao);
            Assert.Equal(6L, dto.HabilidadeId);
            Assert.Equal(2, dto.Pagina);
            Assert.Equal(25, dto.TamanhoPagina);
        }
    }
}
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class CodigoItemDtoTeste
    {
        [Fact]
        public void Deve_Criar_Instancia_Vazia()
        {
            var dto = new CodigoItemDto();

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Id);
            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 123456L;
            var codigoItem = 789012L;

            var dto = new CodigoItemDto
            {
                Id = id,
                CodigoItem = codigoItem
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(codigoItem, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo()
        {
            var dto = new CodigoItemDto
            {
                Id = 999999L,
                CodigoItem = 111111L
            };

            Assert.Equal(999999L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero()
        {
            var dto = new CodigoItemDto
            {
                Id = 0L,
                CodigoItem = 111111L
            };

            Assert.Equal(0L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo()
        {
            var dto = new CodigoItemDto
            {
                Id = -1L,
                CodigoItem = 111111L
            };

            Assert.Equal(-1L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Positivo()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = 888888L
            };

            Assert.Equal(888888L, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Zero()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = 0L
            };

            Assert.Equal(0L, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Negativo()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = -999L
            };

            Assert.Equal(-999L, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Nulo()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = null
            };

            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Maximo_Long()
        {
            var dto = new CodigoItemDto
            {
                Id = long.MaxValue,
                CodigoItem = 123L
            };

            Assert.Equal(long.MaxValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Minimo_Long()
        {
            var dto = new CodigoItemDto
            {
                Id = long.MinValue,
                CodigoItem = 123L
            };

            Assert.Equal(long.MinValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Com_Valor_Maximo_Long()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = long.MaxValue
            };

            Assert.Equal(long.MaxValue, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Com_Valor_Minimo_Long()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = long.MinValue
            };

            Assert.Equal(long.MinValue, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Permitir_Modificar_Propriedades_Apos_Criacao()
        {
            var dto = new CodigoItemDto
            {
                Id = 100L,
                CodigoItem = 200L
            };

            dto.Id = 300L;
            dto.CodigoItem = 400L;

            Assert.Equal(300L, dto.Id);
            Assert.Equal(400L, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Permitir_Alterar_CodigoItem_De_Valor_Para_Nulo()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = 500L
            };

            dto.CodigoItem = null;

            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Permitir_Alterar_CodigoItem_De_Nulo_Para_Valor()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = null
            };

            dto.CodigoItem = 600L;

            Assert.Equal(600L, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Verificar_Que_CodigoItem_Eh_Nullable()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = null
            };

            Assert.False(dto.CodigoItem.HasValue);
        }

        [Fact]
        public void Deve_Verificar_Que_CodigoItem_Possui_Valor_Quando_Preenchido()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = 700L
            };

            Assert.True(dto.CodigoItem.HasValue);
            Assert.Equal(700L, dto.CodigoItem.Value);
        }

        [Fact]
        public void Deve_Aceitar_Ambas_Propriedades_Com_Valores_Identicos()
        {
            var dto = new CodigoItemDto
            {
                Id = 999L,
                CodigoItem = 999L
            };

            Assert.Equal(dto.Id, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Ambas_Propriedades_Com_Valores_Diferentes()
        {
            var dto = new CodigoItemDto
            {
                Id = 100L,
                CodigoItem = 200L
            };

            Assert.NotEqual(dto.Id, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo_E_CodigoItem_Nulo()
        {
            var dto = new CodigoItemDto
            {
                Id = 123L,
                CodigoItem = null
            };

            Assert.Equal(123L, dto.Id);
            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero_E_CodigoItem_Nulo()
        {
            var dto = new CodigoItemDto
            {
                Id = 0L,
                CodigoItem = null
            };

            Assert.Equal(0L, dto.Id);
            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Maximos_Para_Ambas_Propriedades()
        {
            var dto = new CodigoItemDto
            {
                Id = long.MaxValue,
                CodigoItem = long.MaxValue
            };

            Assert.Equal(long.MaxValue, dto.Id);
            Assert.Equal(long.MaxValue, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Minimos_Para_Ambas_Propriedades()
        {
            var dto = new CodigoItemDto
            {
                Id = long.MinValue,
                CodigoItem = long.MinValue
            };

            Assert.Equal(long.MinValue, dto.Id);
            Assert.Equal(long.MinValue, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Permitir_Multiplas_Modificacoes_Em_CodigoItem()
        {
            var dto = new CodigoItemDto
            {
                Id = 1L,
                CodigoItem = 100L
            };

            dto.CodigoItem = 200L;
            Assert.Equal(200L, dto.CodigoItem);

            dto.CodigoItem = null;
            Assert.Null(dto.CodigoItem);

            dto.CodigoItem = 300L;
            Assert.Equal(300L, dto.CodigoItem);
        }
    }
}

using SME.SERAp.Prova.Item.Infra.Dtos.Habilidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Habilidade
{
    public class RetornoHabilidadeDtoTeste
    {
        [Fact]
        public void Deve_Criar_Instancia_Vazia()
        {
            var dto = new RetornoHabilidadeDto();

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Id);
            Assert.Null(dto.Codigo);
            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 123456L;
            var codigo = "EF05MA01";
            var descricao = "Identificar e representar frações, comparando quantidades e estabelecendo relações entre elas";

            var dto = new RetornoHabilidadeDto
            {
                Id = id,
                Codigo = codigo,
                Descricao = descricao
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(codigo, dto.Codigo);
            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 999999L,
                Codigo = "EF01LP01",
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(999999L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 0L,
                Codigo = "EF01LP01",
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(0L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = -1L,
                Codigo = "EF01LP01",
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(-1L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Vazio()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = string.Empty,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(string.Empty, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Nulo()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = null,
                Descricao = "Habilidade Teste"
            };

            Assert.Null(dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF01LP01",
                Descricao = string.Empty
            };

            Assert.Equal(string.Empty, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Nula()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF01LP01",
                Descricao = null
            };

            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Formato_Padrao_BNCC()
        {
            var codigo = "EF67LP28";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade BNCC"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Letras_Maiusculas()
        {
            var codigo = "HABILIDADE123";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Letras_Minusculas()
        {
            var codigo = "habilidade123";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Alfanumerico()
        {
            var codigo = "HAB123XYZ456";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Caracteres_Especiais()
        {
            var codigo = "EF-05-MA-01";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Longa()
        {
            var descricaoLonga = "Identificar e representar frações menores que o inteiro, associando-as ao resultado de uma divisão ou à ideia de parte de um todo, utilizando diferentes representações gráficas e estabelecendo relações entre números fracionários e decimais";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF05MA01",
                Descricao = descricaoLonga
            };

            Assert.Equal(descricaoLonga, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Caracteres_Especiais()
        {
            var descricao = "Identificar (reconhecer) e representar: frações, decimais & porcentagens";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF05MA01",
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Numeros()
        {
            var descricao = "Resolver problemas envolvendo as 4 operações básicas";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF05MA07",
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Permitir_Modificar_Propriedades_Apos_Criacao()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF01LP01",
                Descricao = "Descrição Original"
            };

            dto.Id = 2L;
            dto.Codigo = "EF02LP02";
            dto.Descricao = "Descrição Modificada";

            Assert.Equal(2L, dto.Id);
            Assert.Equal("EF02LP02", dto.Codigo);
            Assert.Equal("Descrição Modificada", dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Maximo_Long()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = long.MaxValue,
                Codigo = "EF01LP01",
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(long.MaxValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Minimo_Long()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = long.MinValue,
                Codigo = "EF01LP01",
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(long.MinValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Espacos_Em_Branco()
        {
            var codigo = "  EF 01 LP 01  ";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Espacos_Em_Branco()
        {
            var descricao = "   Habilidade com espaços   ";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = "EF01LP01",
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Apenas_Numerico()
        {
            var codigo = "123456";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Apenas_Alfabetico()
        {
            var codigo = "ABCDEFGH";

            var dto = new RetornoHabilidadeDto
            {
                Id = 1L,
                Codigo = codigo,
                Descricao = "Habilidade Teste"
            };

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Todas_Propriedades_Nulas_Ou_Padrao()
        {
            var dto = new RetornoHabilidadeDto
            {
                Id = 0L,
                Codigo = null,
                Descricao = null
            };

            Assert.Equal(0L, dto.Id);
            Assert.Null(dto.Codigo);
            Assert.Null(dto.Descricao);
        }
    }
}
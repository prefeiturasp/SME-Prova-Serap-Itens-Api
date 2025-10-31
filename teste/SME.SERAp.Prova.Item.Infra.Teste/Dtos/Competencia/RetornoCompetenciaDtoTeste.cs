using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Competencia
{
    public class RetornoCompetenciaDtoTeste
    {
        [Fact]
        public void Deve_Criar_Instancia_Vazia()
        {
            var dto = new RetornoCompetenciaDto();

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Id);
            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 123456L;
            var descricao = "Competência de Leitura e Interpretação de Textos";

            var dto = new RetornoCompetenciaDto
            {
                Id = id,
                Descricao = descricao
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = 999999L,
                Descricao = "Competência Teste"
            };

            Assert.Equal(999999L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = 0L,
                Descricao = "Competência Teste"
            };

            Assert.Equal(0L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = -1L,
                Descricao = "Competência Teste"
            };

            Assert.Equal(-1L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Vazia()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = string.Empty
            };

            Assert.Equal(string.Empty, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Nula()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = null
            };

            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Longa()
        {
            var descricaoLonga = "Competência de Leitura, Interpretação e Análise Crítica de Textos Diversos, incluindo Narrativos, Descritivos, Argumentativos e Dissertativos, com foco em Compreensão Textual Profunda";

            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = descricaoLonga
            };

            Assert.Equal(descricaoLonga, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Caracteres_Especiais()
        {
            var descricao = "Competência: Matemática & Raciocínio Lógico (1º ao 5º ano)";

            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Numeros()
        {
            var descricao = "Competência 01 - Matemática Básica";

            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Permitir_Modificar_Propriedades_Apos_Criacao()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = "Descrição Original"
            };

            dto.Id = 2L;
            dto.Descricao = "Descrição Modificada";

            Assert.Equal(2L, dto.Id);
            Assert.Equal("Descrição Modificada", dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Maximo_Long()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = long.MaxValue,
                Descricao = "Competência Teste"
            };

            Assert.Equal(long.MaxValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Minimo_Long()
        {
            var dto = new RetornoCompetenciaDto
            {
                Id = long.MinValue,
                Descricao = "Competência Teste"
            };

            Assert.Equal(long.MinValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Com_Espacos_Em_Branco()
        {
            var descricao = "   Competência com espaços   ";

            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Aceitar_Descricao_Apenas_Com_Espacos()
        {
            var descricao = "     ";

            var dto = new RetornoCompetenciaDto
            {
                Id = 1L,
                Descricao = descricao
            };

            Assert.Equal(descricao, dto.Descricao);
        }
    }
}
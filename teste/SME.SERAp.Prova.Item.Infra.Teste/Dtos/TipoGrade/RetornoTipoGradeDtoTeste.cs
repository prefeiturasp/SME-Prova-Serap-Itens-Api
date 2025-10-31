using SME.SERAp.Prova.Item.Infra.Dtos.TipoGrade;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.TipoGrade
{
    public class RetornoTipoGradeDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 100L;
            var descricao = "Grade Curricular de Matemática";

            var dto = new RetornoTipoGradeDto
            {
                Id = id,
                Descricao = descricao
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(descricao, dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Id_Zero()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 0,
                Descricao = "Grade Padrão"
            };

            Assert.Equal(0, dto.Id);
            Assert.Equal("Grade Padrão", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Descricao_Nula()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 50,
                Descricao = null
            };

            Assert.Equal(50, dto.Id);
            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Descricao_Vazia()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 75,
                Descricao = string.Empty
            };

            Assert.Equal(75, dto.Id);
            Assert.Equal(string.Empty, dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Vazio()
        {
            var dto = new RetornoTipoGradeDto();

            Assert.Equal(0, dto.Id);
            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Diferentes_Tipos_Grade()
        {
            var gradeMatematica = new RetornoTipoGradeDto { Id = 1, Descricao = "Grade de Matemática" };
            var gradePortugues = new RetornoTipoGradeDto { Id = 2, Descricao = "Grade de Língua Portuguesa" };
            var gradeCiencias = new RetornoTipoGradeDto { Id = 3, Descricao = "Grade de Ciências" };

            Assert.Equal(1, gradeMatematica.Id);
            Assert.Equal("Grade de Matemática", gradeMatematica.Descricao);
            Assert.Equal(2, gradePortugues.Id);
            Assert.Equal("Grade de Língua Portuguesa", gradePortugues.Descricao);
            Assert.Equal(3, gradeCiencias.Id);
            Assert.Equal("Grade de Ciências", gradeCiencias.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Id_Positivo_Grande()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = long.MaxValue,
                Descricao = "Grade com ID máximo"
            };

            Assert.Equal(long.MaxValue, dto.Id);
            Assert.Equal("Grade com ID máximo", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Id_Negativo()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = -1,
                Descricao = "Grade com ID negativo"
            };

            Assert.Equal(-1, dto.Id);
            Assert.Equal("Grade com ID negativo", dto.Descricao);
        }

        [Fact]
        public void Deve_Permitir_Modificacao_Das_Propriedades()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 100,
                Descricao = "Grade Inicial"
            };

            Assert.Equal(100, dto.Id);
            Assert.Equal("Grade Inicial", dto.Descricao);

            dto.Id = 200;
            dto.Descricao = "Grade Modificada";

            Assert.Equal(200, dto.Id);
            Assert.Equal("Grade Modificada", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Descricao_Longa()
        {
            var descricaoLonga = "Grade Curricular Completa de Ensino Fundamental II - Anos Finais - Matemática e suas Tecnologias - Turno Integral";

            var dto = new RetornoTipoGradeDto
            {
                Id = 500,
                Descricao = descricaoLonga
            };

            Assert.Equal(500, dto.Id);
            Assert.Equal(descricaoLonga, dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Descricao_Contendo_Caracteres_Especiais()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 300,
                Descricao = "Grade - Matemática & Ciências (Básico)"
            };

            Assert.Equal(300, dto.Id);
            Assert.Equal("Grade - Matemática & Ciências (Básico)", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_RetornoTipoGradeDto_Com_Descricao_Contendo_Numeros()
        {
            var dto = new RetornoTipoGradeDto
            {
                Id = 400,
                Descricao = "Grade 2024 - 1º Semestre"
            };

            Assert.Equal(400, dto.Id);
            Assert.Equal("Grade 2024 - 1º Semestre", dto.Descricao);
        }
    }
}
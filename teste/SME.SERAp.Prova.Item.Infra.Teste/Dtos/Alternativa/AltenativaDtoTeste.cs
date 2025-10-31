using SME.SERAp.Prova.Item.Infra.Dtos.Alterantiva;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Alternativa
{
    public class AltenativaDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 100L;
            var descricao = "Alternativa A - Resposta correta sobre matemática";
            var justificativa = "Esta é a resposta correta porque...";
            var numeracao = "A";
            var correta = true;
            var ordem = 1;

            var dto = new AltenativaDto
            {
                Id = id,
                Descricao = descricao,
                Justificativa = justificativa,
                Numeracao = numeracao,
                Correta = correta,
                Ordem = ordem
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(descricao, dto.Descricao);
            Assert.Equal(justificativa, dto.Justificativa);
            Assert.Equal(numeracao, dto.Numeracao);
            Assert.Equal(correta, dto.Correta);
            Assert.Equal(ordem, dto.Ordem);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Id_Nulo()
        {
            var dto = new AltenativaDto
            {
                Id = null,
                Descricao = "Nova alternativa",
                Numeracao = "B",
                Correta = false,
                Ordem = 2
            };

            Assert.Null(dto.Id);
            Assert.Equal("Nova alternativa", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Correta()
        {
            var dto = new AltenativaDto
            {
                Descricao = "Alternativa correta",
                Correta = true,
                Ordem = 1
            };

            Assert.True(dto.Correta);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Incorreta()
        {
            var dto = new AltenativaDto
            {
                Descricao = "Alternativa incorreta",
                Correta = false,
                Ordem = 2
            };

            Assert.False(dto.Correta);
        }

        [Fact]
        public void Deve_Validar_Descricao_Obrigatoria()
        {
            var dto = new AltenativaDto
            {
                Descricao = null,
                Numeracao = "A",
                Correta = true,
                Ordem = 1
            };

            var contexto = new ValidationContext(dto);
            var resultados = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.False(isValid);
            Assert.Contains(resultados, r => r.ErrorMessage == "É necessário informar a descrição da alternativa");
        }

        [Fact]
        public void Deve_Validar_Com_Sucesso_Quando_Descricao_Preenchida()
        {
            var dto = new AltenativaDto
            {
                Descricao = "Descrição válida",
                Numeracao = "A",
                Correta = true,
                Ordem = 1
            };

            var contexto = new ValidationContext(dto);
            var resultados = new System.Collections.Generic.List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.True(isValid);
            Assert.Empty(resultados);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Diferentes_Numeracoes()
        {
            var dtoA = new AltenativaDto { Descricao = "Alt A", Numeracao = "A", Ordem = 1 };
            var dtoB = new AltenativaDto { Descricao = "Alt B", Numeracao = "B", Ordem = 2 };
            var dtoC = new AltenativaDto { Descricao = "Alt C", Numeracao = "C", Ordem = 3 };
            var dtoD = new AltenativaDto { Descricao = "Alt D", Numeracao = "D", Ordem = 4 };

            Assert.Equal("A", dtoA.Numeracao);
            Assert.Equal("B", dtoB.Numeracao);
            Assert.Equal("C", dtoC.Numeracao);
            Assert.Equal("D", dtoD.Numeracao);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Diferentes_Ordens()
        {
            var dto1 = new AltenativaDto { Descricao = "Primeira", Ordem = 1 };
            var dto2 = new AltenativaDto { Descricao = "Segunda", Ordem = 2 };
            var dto3 = new AltenativaDto { Descricao = "Terceira", Ordem = 3 };

            Assert.Equal(1, dto1.Ordem);
            Assert.Equal(2, dto2.Ordem);
            Assert.Equal(3, dto3.Ordem);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Justificativa_Preenchida()
        {
            var dto = new AltenativaDto
            {
                Descricao = "Alternativa A",
                Justificativa = "Esta alternativa está correta porque aborda o conceito de forma adequada.",
                Correta = true,
                Ordem = 1
            };

            Assert.NotNull(dto.Justificativa);
            Assert.Equal("Esta alternativa está correta porque aborda o conceito de forma adequada.", dto.Justificativa);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Com_Justificativa_Nula()
        {
            var dto = new AltenativaDto
            {
                Descricao = "Alternativa B",
                Justificativa = null,
                Correta = false,
                Ordem = 2
            };

            Assert.Null(dto.Justificativa);
        }

        [Fact]
        public void Deve_Criar_Alternativa_Completa_Com_Todos_Campos()
        {
            var dto = new AltenativaDto
            {
                Id = 999,
                Descricao = "A água é composta por hidrogênio e oxigênio",
                Justificativa = "A fórmula química da água é H2O",
                Numeracao = "C",
                Correta = true,
                Ordem = 3
            };

            Assert.Equal(999, dto.Id);
            Assert.Equal("A água é composta por hidrogênio e oxigênio", dto.Descricao);
            Assert.Equal("A fórmula química da água é H2O", dto.Justificativa);
            Assert.Equal("C", dto.Numeracao);
            Assert.True(dto.Correta);
            Assert.Equal(3, dto.Ordem);
        }
    }
}
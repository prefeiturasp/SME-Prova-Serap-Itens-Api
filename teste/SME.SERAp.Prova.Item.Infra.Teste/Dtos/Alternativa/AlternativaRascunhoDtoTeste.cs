using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Alternativa
{
    public class AlternativaRascunhoDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 100L;
            var descricao = "Alternativa A - Resposta rascunho sobre matemática";
            var justificativa = "Esta é a resposta que está sendo elaborada...";
            var numeracao = "A";
            var correta = true;
            var ordem = 1;

            var dto = new AlternativaRascunhoDto
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
        public void Deve_Criar_AlternativaRascunho_Com_Id_Nulo()
        {
            var dto = new AlternativaRascunhoDto
            {
                Id = null,
                Descricao = "Nova alternativa em rascunho",
                Numeracao = "B",
                Correta = false,
                Ordem = 2
            };

            Assert.Null(dto.Id);
            Assert.Equal("Nova alternativa em rascunho", dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Correta()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = "Alternativa correta em rascunho",
                Correta = true,
                Ordem = 1
            };

            Assert.True(dto.Correta);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Incorreta()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = "Alternativa incorreta em rascunho",
                Correta = false,
                Ordem = 2
            };

            Assert.False(dto.Correta);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Descricao_Nula()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = null,
                Numeracao = "A",
                Correta = true,
                Ordem = 1
            };

            Assert.Null(dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Descricao_Vazia()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = string.Empty,
                Numeracao = "B",
                Correta = false,
                Ordem = 2
            };

            Assert.Equal(string.Empty, dto.Descricao);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Diferentes_Numeracoes()
        {
            var dtoA = new AlternativaRascunhoDto { Descricao = "Alt A", Numeracao = "A", Ordem = 1 };
            var dtoB = new AlternativaRascunhoDto { Descricao = "Alt B", Numeracao = "B", Ordem = 2 };
            var dtoC = new AlternativaRascunhoDto { Descricao = "Alt C", Numeracao = "C", Ordem = 3 };
            var dtoD = new AlternativaRascunhoDto { Descricao = "Alt D", Numeracao = "D", Ordem = 4 };

            Assert.Equal("A", dtoA.Numeracao);
            Assert.Equal("B", dtoB.Numeracao);
            Assert.Equal("C", dtoC.Numeracao);
            Assert.Equal("D", dtoD.Numeracao);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Diferentes_Ordens()
        {
            var dto1 = new AlternativaRascunhoDto { Descricao = "Primeira", Ordem = 1 };
            var dto2 = new AlternativaRascunhoDto { Descricao = "Segunda", Ordem = 2 };
            var dto3 = new AlternativaRascunhoDto { Descricao = "Terceira", Ordem = 3 };

            Assert.Equal(1, dto1.Ordem);
            Assert.Equal(2, dto2.Ordem);
            Assert.Equal(3, dto3.Ordem);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Justificativa_Preenchida()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = "Alternativa A",
                Justificativa = "Justificativa em elaboração para esta alternativa.",
                Correta = true,
                Ordem = 1
            };

            Assert.NotNull(dto.Justificativa);
            Assert.Equal("Justificativa em elaboração para esta alternativa.", dto.Justificativa);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Justificativa_Nula()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = "Alternativa B",
                Justificativa = null,
                Correta = false,
                Ordem = 2
            };

            Assert.Null(dto.Justificativa);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Completa_Com_Todos_Campos()
        {
            var dto = new AlternativaRascunhoDto
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

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Vazia()
        {
            var dto = new AlternativaRascunhoDto();

            Assert.Null(dto.Id);
            Assert.Null(dto.Descricao);
            Assert.Null(dto.Justificativa);
            Assert.Null(dto.Numeracao);
            Assert.False(dto.Correta);
            Assert.Equal(0, dto.Ordem);
        }

        [Fact]
        public void Deve_Criar_AlternativaRascunho_Com_Numeracao_Nula()
        {
            var dto = new AlternativaRascunhoDto
            {
                Descricao = "Alternativa sem numeração",
                Numeracao = null,
                Ordem = 1
            };

            Assert.Null(dto.Numeracao);
        }
    }
}
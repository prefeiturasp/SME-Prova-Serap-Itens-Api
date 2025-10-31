using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class ItemListaDtoTeste
    {
        [Fact]
        public void Deve_Criar_Instancia_Vazia()
        {
            var dto = new ItemListaDto();

            Assert.NotNull(dto);
            Assert.Equal(0, dto.Id);
            Assert.Null(dto.CodigoItem);
            Assert.Null(dto.Enunciado);
            Assert.Null(dto.Disciplina);
            Assert.Null(dto.Dificuldade);
            Assert.Null(dto.Situacao);
            Assert.Null(dto.SituacaoDesc);
            Assert.Null(dto.DataCriacao);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 123456L;
            var codigoItem = "ITEM-2024-001";
            var enunciado = "Qual é a capital do Brasil?";
            var disciplina = "Geografia";
            var dificuldade = "Média";
            var situacao = 1L;
            var situacaoDesc = "Ativo";
            var dataCriacao = new DateTime(2024, 10, 15);

            var dto = new ItemListaDto
            {
                Id = id,
                CodigoItem = codigoItem,
                Enunciado = enunciado,
                Disciplina = disciplina,
                Dificuldade = dificuldade,
                Situacao = situacao,
                SituacaoDesc = situacaoDesc,
                DataCriacao = dataCriacao
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(codigoItem, dto.CodigoItem);
            Assert.Equal(enunciado, dto.Enunciado);
            Assert.Equal(disciplina, dto.Disciplina);
            Assert.Equal(dificuldade, dto.Dificuldade);
            Assert.Equal(situacao, dto.Situacao);
            Assert.Equal(situacaoDesc, dto.SituacaoDesc);
            Assert.Equal(dataCriacao, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Positivo()
        {
            var dto = new ItemListaDto
            {
                Id = 999999L
            };

            Assert.Equal(999999L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Zero()
        {
            var dto = new ItemListaDto
            {
                Id = 0L
            };

            Assert.Equal(0L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Negativo()
        {
            var dto = new ItemListaDto
            {
                Id = -1L
            };

            Assert.Equal(-1L, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Vazio()
        {
            var dto = new ItemListaDto
            {
                CodigoItem = string.Empty
            };

            Assert.Equal(string.Empty, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Nulo()
        {
            var dto = new ItemListaDto
            {
                CodigoItem = null
            };

            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Com_Formato_Padrao()
        {
            var codigoItem = "ITEM-2024-12345";

            var dto = new ItemListaDto
            {
                CodigoItem = codigoItem
            };

            Assert.Equal(codigoItem, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Enunciado_Vazio()
        {
            var dto = new ItemListaDto
            {
                Enunciado = string.Empty
            };

            Assert.Equal(string.Empty, dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Enunciado_Nulo()
        {
            var dto = new ItemListaDto
            {
                Enunciado = null
            };

            Assert.Null(dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Enunciado_Longo()
        {
            var enunciadoLongo = "Leia o texto a seguir e responda: Em uma escola, há 450 alunos distribuídos em 15 turmas. Se cada turma tem o mesmo número de alunos, quantos alunos há em cada turma? Considere que não há divisão fracionária de alunos.";

            var dto = new ItemListaDto
            {
                Enunciado = enunciadoLongo
            };

            Assert.Equal(enunciadoLongo, dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Enunciado_Com_Caracteres_Especiais()
        {
            var enunciado = "Qual é o resultado da operação: (5 + 3) × 2 - 4 ÷ 2?";

            var dto = new ItemListaDto
            {
                Enunciado = enunciado
            };

            Assert.Equal(enunciado, dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Disciplina_Vazia()
        {
            var dto = new ItemListaDto
            {
                Disciplina = string.Empty
            };

            Assert.Equal(string.Empty, dto.Disciplina);
        }

        [Fact]
        public void Deve_Aceitar_Disciplina_Nula()
        {
            var dto = new ItemListaDto
            {
                Disciplina = null
            };

            Assert.Null(dto.Disciplina);
        }

        [Fact]
        public void Deve_Aceitar_Diferentes_Disciplinas()
        {
            var disciplinas = new[] { "Matemática", "Português", "História", "Geografia", "Ciências" };

            foreach (var disciplina in disciplinas)
            {
                var dto = new ItemListaDto
                {
                    Disciplina = disciplina
                };

                Assert.Equal(disciplina, dto.Disciplina);
            }
        }

        [Fact]
        public void Deve_Aceitar_Dificuldade_Vazia()
        {
            var dto = new ItemListaDto
            {
                Dificuldade = string.Empty
            };

            Assert.Equal(string.Empty, dto.Dificuldade);
        }

        [Fact]
        public void Deve_Aceitar_Dificuldade_Nula()
        {
            var dto = new ItemListaDto
            {
                Dificuldade = null
            };

            Assert.Null(dto.Dificuldade);
        }

        [Fact]
        public void Deve_Aceitar_Diferentes_Niveis_De_Dificuldade()
        {
            var dificuldades = new[] { "Fácil", "Média", "Difícil", "Muito Difícil" };

            foreach (var dificuldade in dificuldades)
            {
                var dto = new ItemListaDto
                {
                    Dificuldade = dificuldade
                };

                Assert.Equal(dificuldade, dto.Dificuldade);
            }
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Positiva()
        {
            var dto = new ItemListaDto
            {
                Situacao = 1L
            };

            Assert.Equal(1L, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Zero()
        {
            var dto = new ItemListaDto
            {
                Situacao = 0L
            };

            Assert.Equal(0L, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Negativa()
        {
            var dto = new ItemListaDto
            {
                Situacao = -1L
            };

            Assert.Equal(-1L, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Nula()
        {
            var dto = new ItemListaDto
            {
                Situacao = null
            };

            Assert.Null(dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_SituacaoDesc_Vazia()
        {
            var dto = new ItemListaDto
            {
                SituacaoDesc = string.Empty
            };

            Assert.Equal(string.Empty, dto.SituacaoDesc);
        }

        [Fact]
        public void Deve_Aceitar_SituacaoDesc_Nula()
        {
            var dto = new ItemListaDto
            {
                SituacaoDesc = null
            };

            Assert.Null(dto.SituacaoDesc);
        }

        [Fact]
        public void Deve_Aceitar_Diferentes_Descricoes_De_Situacao()
        {
            var situacoes = new[] { "Ativo", "Inativo", "Em Revisão", "Aprovado", "Reprovado" };

            foreach (var situacao in situacoes)
            {
                var dto = new ItemListaDto
                {
                    SituacaoDesc = situacao
                };

                Assert.Equal(situacao, dto.SituacaoDesc);
            }
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Nula()
        {
            var dto = new ItemListaDto
            {
                DataCriacao = null
            };

            Assert.Null(dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Data_Passada()
        {
            var dataPassada = new DateTime(2020, 1, 1);

            var dto = new ItemListaDto
            {
                DataCriacao = dataPassada
            };

            Assert.Equal(dataPassada, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Data_Atual()
        {
            var dataAtual = DateTime.Now;

            var dto = new ItemListaDto
            {
                DataCriacao = dataAtual
            };

            Assert.Equal(dataAtual, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Data_Futura()
        {
            var dataFutura = new DateTime(2030, 12, 31);

            var dto = new ItemListaDto
            {
                DataCriacao = dataFutura
            };

            Assert.Equal(dataFutura, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Hora_Minuto_Segundo()
        {
            var dataCompleta = new DateTime(2024, 10, 15, 14, 30, 45);

            var dto = new ItemListaDto
            {
                DataCriacao = dataCompleta
            };

            Assert.Equal(dataCompleta, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Permitir_Modificar_Propriedades_Apos_Criacao()
        {
            var dto = new ItemListaDto
            {
                Id = 1L,
                CodigoItem = "ITEM-001",
                Enunciado = "Enunciado Original",
                Disciplina = "Matemática",
                Dificuldade = "Fácil",
                Situacao = 1L,
                SituacaoDesc = "Ativo",
                DataCriacao = new DateTime(2024, 1, 1)
            };

            dto.Id = 2L;
            dto.CodigoItem = "ITEM-002";
            dto.Enunciado = "Enunciado Modificado";
            dto.Disciplina = "Português";
            dto.Dificuldade = "Difícil";
            dto.Situacao = 2L;
            dto.SituacaoDesc = "Inativo";
            dto.DataCriacao = new DateTime(2024, 12, 31);

            Assert.Equal(2L, dto.Id);
            Assert.Equal("ITEM-002", dto.CodigoItem);
            Assert.Equal("Enunciado Modificado", dto.Enunciado);
            Assert.Equal("Português", dto.Disciplina);
            Assert.Equal("Difícil", dto.Dificuldade);
            Assert.Equal(2L, dto.Situacao);
            Assert.Equal("Inativo", dto.SituacaoDesc);
            Assert.Equal(new DateTime(2024, 12, 31), dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Maximo_Long()
        {
            var dto = new ItemListaDto
            {
                Id = long.MaxValue
            };

            Assert.Equal(long.MaxValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Id_Com_Valor_Minimo_Long()
        {
            var dto = new ItemListaDto
            {
                Id = long.MinValue
            };

            Assert.Equal(long.MinValue, dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Com_Valor_Maximo_Long()
        {
            var dto = new ItemListaDto
            {
                Situacao = long.MaxValue
            };

            Assert.Equal(long.MaxValue, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Com_Valor_Minimo_Long()
        {
            var dto = new ItemListaDto
            {
                Situacao = long.MinValue
            };

            Assert.Equal(long.MinValue, dto.Situacao);
        }

        [Fact]
        public void Deve_Verificar_Que_Situacao_Eh_Nullable()
        {
            var dto = new ItemListaDto
            {
                Situacao = null
            };

            Assert.False(dto.Situacao.HasValue);
        }

        [Fact]
        public void Deve_Verificar_Que_Situacao_Possui_Valor_Quando_Preenchida()
        {
            var dto = new ItemListaDto
            {
                Situacao = 5L
            };

            Assert.True(dto.Situacao.HasValue);
            Assert.Equal(5L, dto.Situacao.Value);
        }

        [Fact]
        public void Deve_Verificar_Que_DataCriacao_Eh_Nullable()
        {
            var dto = new ItemListaDto
            {
                DataCriacao = null
            };

            Assert.False(dto.DataCriacao.HasValue);
        }

        [Fact]
        public void Deve_Verificar_Que_DataCriacao_Possui_Valor_Quando_Preenchida()
        {
            var data = new DateTime(2024, 5, 20);

            var dto = new ItemListaDto
            {
                DataCriacao = data
            };

            Assert.True(dto.DataCriacao.HasValue);
            Assert.Equal(data, dto.DataCriacao.Value);
        }

        [Fact]
        public void Deve_Aceitar_Todas_Propriedades_String_Nulas()
        {
            var dto = new ItemListaDto
            {
                CodigoItem = null,
                Enunciado = null,
                Disciplina = null,
                Dificuldade = null,
                SituacaoDesc = null
            };

            Assert.Null(dto.CodigoItem);
            Assert.Null(dto.Enunciado);
            Assert.Null(dto.Disciplina);
            Assert.Null(dto.Dificuldade);
            Assert.Null(dto.SituacaoDesc);
        }

        [Fact]
        public void Deve_Aceitar_Todas_Propriedades_String_Vazias()
        {
            var dto = new ItemListaDto
            {
                CodigoItem = string.Empty,
                Enunciado = string.Empty,
                Disciplina = string.Empty,
                Dificuldade = string.Empty,
                SituacaoDesc = string.Empty
            };

            Assert.Equal(string.Empty, dto.CodigoItem);
            Assert.Equal(string.Empty, dto.Enunciado);
            Assert.Equal(string.Empty, dto.Disciplina);
            Assert.Equal(string.Empty, dto.Dificuldade);
            Assert.Equal(string.Empty, dto.SituacaoDesc);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Data_Minima()
        {
            var dto = new ItemListaDto
            {
                DataCriacao = DateTime.MinValue
            };

            Assert.Equal(DateTime.MinValue, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_DataCriacao_Com_Data_Maxima()
        {
            var dto = new ItemListaDto
            {
                DataCriacao = DateTime.MaxValue
            };

            Assert.Equal(DateTime.MaxValue, dto.DataCriacao);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Situacao_De_Valor_Para_Nulo()
        {
            var dto = new ItemListaDto
            {
                Situacao = 10L
            };

            dto.Situacao = null;

            Assert.Null(dto.Situacao);
        }

        [Fact]
        public void Deve_Permitir_Alterar_DataCriacao_De_Valor_Para_Nulo()
        {
            var dto = new ItemListaDto
            {
                DataCriacao = DateTime.Now
            };

            dto.DataCriacao = null;

            Assert.Null(dto.DataCriacao);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Com_Caracteres_Especiais()
        {
            var codigoItem = "ITEM-2024/MAT-001_v2";

            var dto = new ItemListaDto
            {
                CodigoItem = codigoItem
            };

            Assert.Equal(codigoItem, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Enunciado_Com_Quebras_De_Linha()
        {
            var enunciado = "Primeira linha\nSegunda linha\nTerceira linha";

            var dto = new ItemListaDto
            {
                Enunciado = enunciado
            };

            Assert.Equal(enunciado, dto.Enunciado);
        }
    }
}
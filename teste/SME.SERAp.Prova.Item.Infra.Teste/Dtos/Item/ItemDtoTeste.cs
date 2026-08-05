using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alterantiva;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class ItemDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 100L;
            var codigoItem = "ITEM-2024-001";
            var areaConhecimentoId = 1L;
            var disciplinaId = 2L;
            var matrizId = 3L;
            var competenciaId = 4L;
            var habilidadeId = 5L;
            var anoMatrizId = 6L;
            var dificuldadeSugeridaId = 7L;
            var discriminacao = 0.85m;
            var acertoCasual = 0.25m;
            var dificuldade = 0.65m;
            var assuntoId = 8L;
            var subAssuntoId = 9L;
            var situacao = SituacaoItem.Ativo;
            var tipo = TipoItem.Dicotômico;
            var quantidadeAlternativasId = 4L;
            var palavrasChave = new[] { "matemática", "álgebra", "equação" };
            var parametroBTransformado = 1.5m;
            var mediaEhDesvio = "0.5 / 0.2";
            var observacao = "Item de nível médio";
            var sentencaDescritora = "Resolver equações do primeiro grau";
            var nivelItem = 3.5m;
            var textoBase = "Texto base do item para contextualização";
            var fonte = "Livro de Matemática - 8º ano - Editora Moderna";
            var enunciado = "Qual é o valor de x na equação 2x + 5 = 15?";
            var arquivoVideoId = 10L;
            var arquivoAudioId = 11L;

            var alternativas = new List<AltenativaDto>
            {
                new AltenativaDto { Id = 1, Descricao = "x = 5", Correta = true, Ordem = 1, Numeracao = "A" },
                new AltenativaDto { Id = 2, Descricao = "x = 10", Correta = false, Ordem = 2, Numeracao = "B" },
                new AltenativaDto { Id = 3, Descricao = "x = 20", Correta = false, Ordem = 3, Numeracao = "C" },
                new AltenativaDto { Id = 4, Descricao = "x = 15", Correta = false, Ordem = 4, Numeracao = "D" }
            };

            var dto = new ItemDto
            {
                Id = id,
                CodigoItem = codigoItem,
                AreaConhecimentoId = areaConhecimentoId,
                DisciplinaId = disciplinaId,
                MatrizId = matrizId,
                CompetenciaId = competenciaId,
                HabilidadeId = habilidadeId,
                AnoMatrizId = anoMatrizId,
                DificuldadeSugeridaId = dificuldadeSugeridaId,
                Discriminacao = discriminacao,
                AcertoCasual = acertoCasual,
                Dificuldade = dificuldade,
                AssuntoId = assuntoId,
                SubAssuntoId = subAssuntoId,
                Situacao = situacao,
                Tipo = tipo,
                QuantidadeAlternativasId = quantidadeAlternativasId,
                PalavrasChave = palavrasChave,
                ParametroBTransformado = parametroBTransformado,
                MediaEhDesvio = mediaEhDesvio,
                Observacao = observacao,
                SentencaDescritora = sentencaDescritora,
                NivelItem = nivelItem,
                TextoBase = textoBase,
                Fonte = fonte,
                Enunciado = enunciado,
                AlternativasDto = alternativas,
                ArquivoVideoId = arquivoVideoId,
                ArquivoAudioId = arquivoAudioId
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal(codigoItem, dto.CodigoItem);
            Assert.Equal(areaConhecimentoId, dto.AreaConhecimentoId);
            Assert.Equal(disciplinaId, dto.DisciplinaId);
            Assert.Equal(matrizId, dto.MatrizId);
            Assert.Equal(competenciaId, dto.CompetenciaId);
            Assert.Equal(habilidadeId, dto.HabilidadeId);
            Assert.Equal(anoMatrizId, dto.AnoMatrizId);
            Assert.Equal(dificuldadeSugeridaId, dto.DificuldadeSugeridaId);
            Assert.Equal(discriminacao, dto.Discriminacao);
            Assert.Equal(acertoCasual, dto.AcertoCasual);
            Assert.Equal(dificuldade, dto.Dificuldade);
            Assert.Equal(assuntoId, dto.AssuntoId);
            Assert.Equal(subAssuntoId, dto.SubAssuntoId);
            Assert.Equal(situacao, dto.Situacao);
            Assert.Equal(tipo, dto.Tipo);
            Assert.Equal(quantidadeAlternativasId, dto.QuantidadeAlternativasId);
            Assert.Equal(palavrasChave, dto.PalavrasChave);
            Assert.Equal(parametroBTransformado, dto.ParametroBTransformado);
            Assert.Equal(mediaEhDesvio, dto.MediaEhDesvio);
            Assert.Equal(observacao, dto.Observacao);
            Assert.Equal(sentencaDescritora, dto.SentencaDescritora);
            Assert.Equal(nivelItem, dto.NivelItem);
            Assert.Equal(textoBase, dto.TextoBase);
            Assert.Equal(fonte, dto.Fonte);
            Assert.Equal(enunciado, dto.Enunciado);
            Assert.Equal(4, dto.AlternativasDto.Count);
            Assert.Equal(arquivoVideoId, dto.ArquivoVideoId);
            Assert.Equal(arquivoAudioId, dto.ArquivoAudioId);
        }

        [Fact]
        public void Deve_Validar_Observacao_MaxLength()
        {
            var dto = new ItemDto
            {
                Observacao = new string('A', 101)
            };

            var contexto = new ValidationContext(dto);
            var resultados = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.False(isValid);
            Assert.Contains(resultados, r => r.ErrorMessage.Contains("observação"));
        }

        [Fact]
        public void Deve_Validar_SentencaDescritora_MaxLength()
        {
            var dto = new ItemDto
            {
                SentencaDescritora = new string('B', 101)
            };

            var contexto = new ValidationContext(dto);
            var resultados = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.False(isValid);
            Assert.Contains(resultados, r => r.ErrorMessage.Contains("Sentença Descritora"));
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Id_Nulo()
        {
            var dto = new ItemDto
            {
                Id = null,
                CodigoItem = "NOVO-ITEM-001"
            };

            Assert.Null(dto.Id);
            Assert.Equal("NOVO-ITEM-001", dto.CodigoItem);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Situacao_Inativo()
        {
            var dto = new ItemDto { Situacao = SituacaoItem.Inativo };
            Assert.Equal(SituacaoItem.Inativo, dto.Situacao);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Situacao_Ativo()
        {
            var dto = new ItemDto { Situacao = SituacaoItem.Ativo };
            Assert.Equal(SituacaoItem.Ativo, dto.Situacao);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Situacao_Rascunho()
        {
            var dto = new ItemDto { Situacao = SituacaoItem.Rascunho };
            Assert.Equal(SituacaoItem.Rascunho, dto.Situacao);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Tipo_Dicotomico()
        {
            var dto = new ItemDto { Tipo = TipoItem.Dicotômico };
            Assert.Equal(TipoItem.Dicotômico, dto.Tipo);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Tipo_Politomico()
        {
            var dto = new ItemDto { Tipo = TipoItem.Politômico };
            Assert.Equal(TipoItem.Politômico, dto.Tipo);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Uma_Palavra_Chave()
        {
            var dto = new ItemDto { PalavrasChave = new[] { "matemática" } };

            Assert.Single(dto.PalavrasChave);
            Assert.Equal("matemática", dto.PalavrasChave[0]);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Multiplas_Palavras_Chave()
        {
            var palavras = new[] { "matemática", "álgebra", "equação", "primeiro grau", "incógnita" };

            var dto = new ItemDto { PalavrasChave = palavras };

            Assert.Equal(5, dto.PalavrasChave.Length);
            Assert.Contains("matemática", dto.PalavrasChave);
            Assert.Contains("álgebra", dto.PalavrasChave);
            Assert.Contains("equação", dto.PalavrasChave);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Lista_Alternativas_Vazia()
        {
            var dto = new ItemDto { AlternativasDto = new List<AltenativaDto>() };

            Assert.NotNull(dto.AlternativasDto);
            Assert.Empty(dto.AlternativasDto);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Quatro_Alternativas()
        {
            var alternativas = new List<AltenativaDto>
            {
                new AltenativaDto { Descricao = "Alternativa A", Correta = false, Ordem = 1 },
                new AltenativaDto { Descricao = "Alternativa B", Correta = true, Ordem = 2 },
                new AltenativaDto { Descricao = "Alternativa C", Correta = false, Ordem = 3 },
                new AltenativaDto { Descricao = "Alternativa D", Correta = false, Ordem = 4 }
            };

            var dto = new ItemDto { AlternativasDto = alternativas };

            Assert.Equal(4, dto.AlternativasDto.Count);
            Assert.Single(dto.AlternativasDto.Where(a => a.Correta));
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Propriedades_Opcionais_Nulas()
        {
            var dto = new ItemDto();

            Assert.Null(dto.Id);
            Assert.Null(dto.CodigoItem);
            Assert.Null(dto.Discriminacao);
            Assert.Null(dto.AcertoCasual);
            Assert.Null(dto.Dificuldade);
            Assert.Null(dto.AssuntoId);
            Assert.Null(dto.SubAssuntoId);
            Assert.Null(dto.ParametroBTransformado);
            Assert.Null(dto.MediaEhDesvio);
            Assert.Null(dto.Observacao);
            Assert.Null(dto.SentencaDescritora);
            Assert.Null(dto.NivelItem);
            Assert.Null(dto.TextoBase);
            Assert.Null(dto.Fonte);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Parametros_TRI()
        {
            var dto = new ItemDto
            {
                Discriminacao = 0.85m,
                AcertoCasual = 0.25m,
                Dificuldade = 0.65m,
                ParametroBTransformado = 1.5m,
                NivelItem = 3.5m
            };

            Assert.Equal(0.85m, dto.Discriminacao);
            Assert.Equal(0.25m, dto.AcertoCasual);
            Assert.Equal(0.65m, dto.Dificuldade);
            Assert.Equal(1.5m, dto.ParametroBTransformado);
            Assert.Equal(3.5m, dto.NivelItem);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_TextoBase_E_Fonte()
        {
            var dto = new ItemDto
            {
                TextoBase = "A fotossíntese é um processo realizado pelas plantas...",
                Fonte = "Livro de Ciências - 7º ano - Editora Moderna - 2023"
            };

            Assert.Equal("A fotossíntese é um processo realizado pelas plantas...", dto.TextoBase);
            Assert.Equal("Livro de Ciências - 7º ano - Editora Moderna - 2023", dto.Fonte);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Arquivos_Multimidia()
        {
            var dto = new ItemDto { ArquivoVideoId = 100, ArquivoAudioId = 200 };

            Assert.Equal(100, dto.ArquivoVideoId);
            Assert.Equal(200, dto.ArquivoAudioId);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Assunto_E_SubAssunto()
        {
            var dto = new ItemDto { AssuntoId = 10, SubAssuntoId = 25 };

            Assert.Equal(10, dto.AssuntoId);
            Assert.Equal(25, dto.SubAssuntoId);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_Observacao_Maxima_Permitida()
        {
            var dto = new ItemDto { Observacao = new string('A', 100) };

            var contexto = new ValidationContext(dto);
            var resultados = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.True(isValid);
            Assert.Equal(100, dto.Observacao.Length);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Com_SentencaDescritora_Maxima_Permitida()
        {
            var dto = new ItemDto { SentencaDescritora = new string('B', 100) };

            var contexto = new ValidationContext(dto);
            var resultados = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, contexto, resultados, true);

            Assert.True(isValid);
            Assert.Equal(100, dto.SentencaDescritora.Length);
        }

        [Fact]
        public void Deve_Criar_ItemDto_Completo()
        {
            var dto = new ItemDto
            {
                Id = 1,
                CodigoItem = "ITEM-001",
                AreaConhecimentoId = 1,
                DisciplinaId = 2,
                MatrizId = 3,
                CompetenciaId = 4,
                HabilidadeId = 5,
                AnoMatrizId = 6,
                DificuldadeSugeridaId = 7,
                Discriminacao = 0.75m,
                AcertoCasual = 0.20m,
                Dificuldade = 0.50m,
                AssuntoId = 8,
                SubAssuntoId = 9,
                Situacao = SituacaoItem.Ativo,
                Tipo = TipoItem.Dicotômico,
                QuantidadeAlternativasId = 4,
                PalavrasChave = new[] { "teste", "avaliação" },
                ParametroBTransformado = 2.0m,
                MediaEhDesvio = "0.6 / 0.3",
                Observacao = "Observação de teste",
                SentencaDescritora = "Sentença descritora de teste",
                NivelItem = 4.0m,
                TextoBase = "Texto base de teste",
                Fonte = "Fonte de teste",
                Enunciado = "Enunciado de teste",
                AlternativasDto = new List<AltenativaDto>
                {
                    new AltenativaDto { Id = 1, Descricao = "Alt A", Correta = true, Ordem = 1 }
                },
                ArquivoVideoId = 10,
                ArquivoAudioId = 20
            };

            Assert.Equal(1, dto.Id);
            Assert.Equal("ITEM-001", dto.CodigoItem);
            Assert.Equal(1, dto.AreaConhecimentoId);
            Assert.Equal(2, dto.DisciplinaId);
            Assert.Equal(3, dto.MatrizId);
            Assert.Equal(4, dto.CompetenciaId);
            Assert.Equal(5, dto.HabilidadeId);
            Assert.Equal(6, dto.AnoMatrizId);
            Assert.Equal(7, dto.DificuldadeSugeridaId);
            Assert.Equal(0.75m, dto.Discriminacao);
            Assert.Equal(0.20m, dto.AcertoCasual);
            Assert.Equal(0.50m, dto.Dificuldade);
            Assert.Equal(8, dto.AssuntoId);
            Assert.Equal(9, dto.SubAssuntoId);
            Assert.Equal(SituacaoItem.Ativo, dto.Situacao);
            Assert.Equal(TipoItem.Dicotômico, dto.Tipo);
            Assert.Equal(4, dto.QuantidadeAlternativasId);
            Assert.Equal(2, dto.PalavrasChave.Length);
            Assert.Equal(2.0m, dto.ParametroBTransformado);
            Assert.Equal("0.6 / 0.3", dto.MediaEhDesvio);
            Assert.Equal("Observação de teste", dto.Observacao);
            Assert.Equal("Sentença descritora de teste", dto.SentencaDescritora);
            Assert.Equal(4.0m, dto.NivelItem);
            Assert.Equal("Texto base de teste", dto.TextoBase);
            Assert.Equal("Fonte de teste", dto.Fonte);
            Assert.Equal("Enunciado de teste", dto.Enunciado);
            Assert.Single(dto.AlternativasDto);
            Assert.Equal(10, dto.ArquivoVideoId);
            Assert.Equal(20, dto.ArquivoAudioId);
        }
    }
}
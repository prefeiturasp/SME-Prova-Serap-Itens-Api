using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Item
{
    public class ItemRascunhoDtoTeste
    {
        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var id = 1L;
            var codigoItem = "ITEM001";
            var areaConhecimentoId = 10L;
            var disciplinaId = 20L;
            var matrizId = 30L;
            var competenciaId = 40L;
            var habilidadeId = 50L;
            var anoMatrizId = 60L;
            var dificuldadeSugeridaId = 70L;
            var discriminacao = 0.75m;
            var acertoCasual = 0.25m;
            var dificuldade = 0.50m;
            var assuntoId = 80L;
            var subAssuntoId = 90L;
            var situacao = SituacaoItem.Ativo;
            var tipo = TipoItem.Dicotômico;
            var quantidadeAlternativasId = 100L;
            var palavrasChave = new[] { "palavra1", "palavra2" };
            var parametroBTransformado = 1.5m;
            var mediaEhDesvio = "Média: 5.5, Desvio: 1.2";
            var observacao = "Observação do item";
            var sentencaDescritora = "Sentença descritora do item";
            var nivelItem = 3.5m;
            var textoBase = "Texto base do item";
            var fonte = "Fonte bibliográfica";
            var enunciado = "Enunciado da questão";
            var arquivoVideoId = 110L;
            var arquivoAudioId = 120L;
            var alternativas = new List<AlternativaRascunhoDto>
            {
                new AlternativaRascunhoDto { Id = 1, Descricao = "Alt 1", Correta = true, Ordem = 1 }
            };

            var dto = new ItemRascunhoDto
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
            Assert.Equal(alternativas, dto.AlternativasDto);
            Assert.Equal(arquivoVideoId, dto.ArquivoVideoId);
            Assert.Equal(arquivoAudioId, dto.ArquivoAudioId);
        }

        [Fact]
        public void Deve_Aceitar_Id_Nulo()
        {
            var dto = new ItemRascunhoDto
            {
                Id = null,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Null(dto.Id);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Vazio()
        {
            var dto = new ItemRascunhoDto
            {
                CodigoItem = string.Empty,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(string.Empty, dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_CodigoItem_Nulo()
        {
            var dto = new ItemRascunhoDto
            {
                CodigoItem = null,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Null(dto.CodigoItem);
        }

        [Fact]
        public void Deve_Aceitar_Propriedades_Nulas_Opcionais()
        {
            var dto = new ItemRascunhoDto
            {
                AreaConhecimentoId = 1,
                DisciplinaId = 1,
                MatrizId = null,
                CompetenciaId = null,
                HabilidadeId = null,
                AnoMatrizId = null,
                DificuldadeSugeridaId = null,
                Discriminacao = null,
                AcertoCasual = null,
                Dificuldade = null,
                AssuntoId = null,
                SubAssuntoId = null,
                Situacao = null,
                Tipo = null,
                QuantidadeAlternativasId = null,
                ParametroBTransformado = null,
                NivelItem = null
            };

            Assert.Null(dto.MatrizId);
            Assert.Null(dto.CompetenciaId);
            Assert.Null(dto.HabilidadeId);
            Assert.Null(dto.AnoMatrizId);
            Assert.Null(dto.DificuldadeSugeridaId);
            Assert.Null(dto.Discriminacao);
            Assert.Null(dto.AcertoCasual);
            Assert.Null(dto.Dificuldade);
            Assert.Null(dto.AssuntoId);
            Assert.Null(dto.SubAssuntoId);
            Assert.Null(dto.Situacao);
            Assert.Null(dto.Tipo);
            Assert.Null(dto.QuantidadeAlternativasId);
            Assert.Null(dto.ParametroBTransformado);
            Assert.Null(dto.NivelItem);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Inativo()
        {
            var dto = new ItemRascunhoDto
            {
                Situacao = SituacaoItem.Inativo,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(SituacaoItem.Inativo, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Ativo()
        {
            var dto = new ItemRascunhoDto
            {
                Situacao = SituacaoItem.Ativo,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(SituacaoItem.Ativo, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Pendente()
        {
            var dto = new ItemRascunhoDto
            {
                Situacao = SituacaoItem.Pendente,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(SituacaoItem.Pendente, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Situacao_Rascunho()
        {
            var dto = new ItemRascunhoDto
            {
                Situacao = SituacaoItem.Rascunho,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(SituacaoItem.Rascunho, dto.Situacao);
        }

        [Fact]
        public void Deve_Aceitar_Tipo_Dicotomico()
        {
            var dto = new ItemRascunhoDto
            {
                Tipo = TipoItem.Dicotômico,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(TipoItem.Dicotômico, dto.Tipo);
        }

        [Fact]
        public void Deve_Aceitar_Tipo_Politomico()
        {
            var dto = new ItemRascunhoDto
            {
                Tipo = TipoItem.Politômico,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(TipoItem.Politômico, dto.Tipo);
        }

        [Fact]
        public void Deve_Aceitar_PalavrasChave_Vazias()
        {
            var dto = new ItemRascunhoDto
            {
                PalavrasChave = new string[] { },
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Empty(dto.PalavrasChave);
        }

        [Fact]
        public void Deve_Aceitar_PalavrasChave_Nulas()
        {
            var dto = new ItemRascunhoDto
            {
                PalavrasChave = null,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Null(dto.PalavrasChave);
        }

        [Fact]
        public void Deve_Aceitar_Multiplas_PalavrasChave()
        {
            var palavrasChave = new[] { "palavra1", "palavra2", "palavra3", "palavra4" };

            var dto = new ItemRascunhoDto
            {
                PalavrasChave = palavrasChave,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(4, dto.PalavrasChave.Length);
            Assert.Equal(palavrasChave, dto.PalavrasChave);
        }

        [Fact]
        public void Deve_Aceitar_AlternativasDto_Vazia()
        {
            var dto = new ItemRascunhoDto
            {
                AlternativasDto = new List<AlternativaRascunhoDto>(),
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Empty(dto.AlternativasDto);
        }

        [Fact]
        public void Deve_Aceitar_AlternativasDto_Nula()
        {
            var dto = new ItemRascunhoDto
            {
                AlternativasDto = null,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Null(dto.AlternativasDto);
        }

        [Fact]
        public void Deve_Aceitar_Multiplas_Alternativas()
        {
            var alternativas = new List<AlternativaRascunhoDto>
            {
                new AlternativaRascunhoDto { Id = 1, Descricao = "Alternativa A", Correta = true, Ordem = 1 },
                new AlternativaRascunhoDto { Id = 2, Descricao = "Alternativa B", Correta = false, Ordem = 2 },
                new AlternativaRascunhoDto { Id = 3, Descricao = "Alternativa C", Correta = false, Ordem = 3 }
            };

            var dto = new ItemRascunhoDto
            {
                AlternativasDto = alternativas,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(3, dto.AlternativasDto.Count);
            Assert.Equal(alternativas, dto.AlternativasDto);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Decimais_Zero()
        {
            var dto = new ItemRascunhoDto
            {
                Discriminacao = 0m,
                AcertoCasual = 0m,
                Dificuldade = 0m,
                ParametroBTransformado = 0m,
                NivelItem = 0m,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(0m, dto.Discriminacao);
            Assert.Equal(0m, dto.AcertoCasual);
            Assert.Equal(0m, dto.Dificuldade);
            Assert.Equal(0m, dto.ParametroBTransformado);
            Assert.Equal(0m, dto.NivelItem);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Decimais_Negativos()
        {
            var dto = new ItemRascunhoDto
            {
                Discriminacao = -1.5m,
                AcertoCasual = -0.25m,
                Dificuldade = -2.0m,
                ParametroBTransformado = -3.5m,
                NivelItem = -1.0m,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(-1.5m, dto.Discriminacao);
            Assert.Equal(-0.25m, dto.AcertoCasual);
            Assert.Equal(-2.0m, dto.Dificuldade);
            Assert.Equal(-3.5m, dto.ParametroBTransformado);
            Assert.Equal(-1.0m, dto.NivelItem);
        }

        [Fact]
        public void Deve_Aceitar_Valores_Decimais_Com_Precisao()
        {
            var dto = new ItemRascunhoDto
            {
                Discriminacao = 0.123456789m,
                AcertoCasual = 0.987654321m,
                Dificuldade = 0.555555555m,
                ParametroBTransformado = 1.234567890m,
                NivelItem = 9.876543210m,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(0.123456789m, dto.Discriminacao);
            Assert.Equal(0.987654321m, dto.AcertoCasual);
            Assert.Equal(0.555555555m, dto.Dificuldade);
            Assert.Equal(1.234567890m, dto.ParametroBTransformado);
            Assert.Equal(9.876543210m, dto.NivelItem);
        }

        [Fact]
        public void Deve_Aceitar_Strings_Vazias()
        {
            var dto = new ItemRascunhoDto
            {
                CodigoItem = string.Empty,
                MediaEhDesvio = string.Empty,
                Observacao = string.Empty,
                SentencaDescritora = string.Empty,
                TextoBase = string.Empty,
                Fonte = string.Empty,
                Enunciado = string.Empty,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(string.Empty, dto.CodigoItem);
            Assert.Equal(string.Empty, dto.MediaEhDesvio);
            Assert.Equal(string.Empty, dto.Observacao);
            Assert.Equal(string.Empty, dto.SentencaDescritora);
            Assert.Equal(string.Empty, dto.TextoBase);
            Assert.Equal(string.Empty, dto.Fonte);
            Assert.Equal(string.Empty, dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Strings_Nulas()
        {
            var dto = new ItemRascunhoDto
            {
                CodigoItem = null,
                MediaEhDesvio = null,
                Observacao = null,
                SentencaDescritora = null,
                TextoBase = null,
                Fonte = null,
                Enunciado = null,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Null(dto.CodigoItem);
            Assert.Null(dto.MediaEhDesvio);
            Assert.Null(dto.Observacao);
            Assert.Null(dto.SentencaDescritora);
            Assert.Null(dto.TextoBase);
            Assert.Null(dto.Fonte);
            Assert.Null(dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Observacao_Com_100_Caracteres()
        {
            var observacao = new string('A', 100);

            var dto = new ItemRascunhoDto
            {
                Observacao = observacao,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(100, dto.Observacao.Length);
            Assert.Equal(observacao, dto.Observacao);
        }

        [Fact]
        public void Deve_Aceitar_Observacao_Com_Menos_De_100_Caracteres()
        {
            var observacao = "Observação válida";

            var dto = new ItemRascunhoDto
            {
                Observacao = observacao,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.True(dto.Observacao.Length < 100);
            Assert.Equal(observacao, dto.Observacao);
        }

        [Fact]
        public void Deve_Aceitar_Textos_Longos()
        {
            var textoLongo = new string('A', 5000);

            var dto = new ItemRascunhoDto
            {
                TextoBase = textoLongo,
                Enunciado = textoLongo,
                SentencaDescritora = textoLongo,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(5000, dto.TextoBase.Length);
            Assert.Equal(5000, dto.Enunciado.Length);
            Assert.Equal(5000, dto.SentencaDescritora.Length);
        }

        [Fact]
        public void Deve_Aceitar_ArquivoVideoId_Zero()
        {
            var dto = new ItemRascunhoDto
            {
                ArquivoVideoId = 0,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(0, dto.ArquivoVideoId);
        }

        [Fact]
        public void Deve_Aceitar_ArquivoAudioId_Zero()
        {
            var dto = new ItemRascunhoDto
            {
                ArquivoAudioId = 0,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(0, dto.ArquivoAudioId);
        }

        [Fact]
        public void Deve_Aceitar_Textos_Com_HTML()
        {
            var textoComHtml = "<p>Texto com <strong>HTML</strong></p>";

            var dto = new ItemRascunhoDto
            {
                TextoBase = textoComHtml,
                Enunciado = textoComHtml,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(textoComHtml, dto.TextoBase);
            Assert.Equal(textoComHtml, dto.Enunciado);
        }

        [Fact]
        public void Deve_Aceitar_Textos_Com_Caracteres_Especiais()
        {
            var textoEspecial = "Texto com @#$%&*()_+-=[]{}|;':\"<>,.?/\\~`";

            var dto = new ItemRascunhoDto
            {
                Fonte = textoEspecial,
                MediaEhDesvio = textoEspecial,
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            Assert.Equal(textoEspecial, dto.Fonte);
            Assert.Equal(textoEspecial, dto.MediaEhDesvio);
        }

        [Fact]
        public void Deve_Validar_AreaConhecimentoId_Obrigatorio()
        {
            var dto = new ItemRascunhoDto
            {
                DisciplinaId = 1
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("AreaConhecimentoId"));
        }

        [Fact]
        public void Deve_Validar_DisciplinaId_Obrigatorio()
        {
            var dto = new ItemRascunhoDto
            {
                AreaConhecimentoId = 1
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("DisciplinaId"));
        }

        [Fact]
        public void Deve_Validar_AreaConhecimentoId_Maior_Que_Zero()
        {
            var dto = new ItemRascunhoDto
            {
                AreaConhecimentoId = 0,
                DisciplinaId = 1
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("AreaConhecimentoId") &&
                                          r.ErrorMessage.Contains("maior que zero"));
        }

        [Fact]
        public void Deve_Validar_DisciplinaId_Maior_Que_Zero()
        {
            var dto = new ItemRascunhoDto
            {
                AreaConhecimentoId = 1,
                DisciplinaId = 0
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("DisciplinaId") &&
                                          r.ErrorMessage.Contains("maior que zero"));
        }

        [Fact]
        public void Deve_Passar_Validacao_Com_Campos_Obrigatorios_Preenchidos()
        {
            var dto = new ItemRascunhoDto
            {
                AreaConhecimentoId = 1,
                DisciplinaId = 1
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, true);

            Assert.True(isValid);
            Assert.Empty(results);
        }
    }
}
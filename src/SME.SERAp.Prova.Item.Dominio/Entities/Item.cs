using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("item")]
    public class Item : EntidadeBase
    {
        public Item() { }

        public Item(string codigoItem, long areaconhecimentoId, long disciplinaId,
            long? matrizId, long? competenciaId, long? habilidadeId, long? anoMatrizId, long? dificuldadeSugeridaId,
            decimal? discriminacao, decimal? acertoCasual, decimal? dificuldade, long? assuntoId, long? subassuntoId,
            SituacaoItem? situacao, TipoItem? tipo, long? quantidadeAlternativaId, string palavrasChave,
            decimal? parametroBTransformado, string mediaEhDesvio, string observacao, string sentencaDescritora,
            decimal? nivelItem, long versaoItem, string textoBase, string fonte, string enunciado)
        {
            CodigoItem = codigoItem;
            AreaconhecimentoId = areaconhecimentoId;
            MatrizId = matrizId;
            DisciplinaId = disciplinaId;
            CompetenciaId = competenciaId;
            HabilidadeId = habilidadeId;
            AnoMatrizId = anoMatrizId;
            DificuldadeSugeridaId = dificuldadeSugeridaId;
            Discriminacao = discriminacao;
            AcertoCasual = acertoCasual;
            Dificuldade = dificuldade;
            AssuntoId = assuntoId;
            SubAssuntoId = subassuntoId;
            Situacao = situacao;
            Tipo = tipo;
            QuantidadeAlternativasId = quantidadeAlternativaId;
            PalavrasChave = palavrasChave;
            ParametroBTransformado = parametroBTransformado;
            MediaEhDesvio = mediaEhDesvio;
            Observacao = observacao;
            SentencaDescritora = sentencaDescritora;
            NivelItem = nivelItem;
            VersaoItem = versaoItem;
            TextoBase = textoBase;
            Fonte = fonte;
            Enunciado = enunciado;
        }

        [Column("versao_item")]
        public long VersaoItem { get; set; }

        [Column("codigo_item")]
        public string CodigoItem { get; set; }

        [Column("area_conhecimento_id")]
        public long AreaconhecimentoId { get; set; }

        [Column("disciplina_id")]
        public long DisciplinaId { get; set; }

        [Column("matriz_id")]
        public long? MatrizId { get; set; }

        [Column("competencia_id")]
        public long? CompetenciaId { get; set; }

        [Column("habilidade_id")]
        public long? HabilidadeId { get; set; }

        [Column("tipo_grade_id")]
        public long? AnoMatrizId { get; set; }

        [Column("dificuldade_sugerida_id")]
        public long? DificuldadeSugeridaId { get; set; }

        [Column("discriminacao")]
        public decimal? Discriminacao { get; set; }

        [Column("acerto_casual")]
        public decimal? AcertoCasual { get; set; }

        [Column("dificuldade")]
        public decimal? Dificuldade { get; set; }

        [Column("assunto_id")]
        public long? AssuntoId { get; set; }

        [Column("subassunto_id")]
        public long? SubAssuntoId { get; set; }

        [Column("situacao")]
        public SituacaoItem? Situacao { get; set; }

        [Column("tipo")]
        public TipoItem? Tipo { get; set; }

        [Column("quantidade_alternativa_id")]
        public long? QuantidadeAlternativasId { get; set; }

        [Column("palavras_chave")]
        public string PalavrasChave { get; set; }

        [Column("parametro_b_transformado")]
        public decimal? ParametroBTransformado { get; set; }

        [Column("media_eh_desvio")]
        public string MediaEhDesvio { get; set; }

        [Column("observacao")]
        public string Observacao { get; set; }

        [Column("sentencadescritora")]
        public string SentencaDescritora { get; set; }

        [Column("nivelitem_id")]
        public decimal? NivelItem { get; set; }

        [Column("criado_em")]
        public DateTime DataCriacao { get; set; }

        [Column("alterado_em")]
        public DateTime DataAlteracao { get; set; }

        [Column("texto_base")]
        public string TextoBase { get; set; }

        [Column("fonte")]
        public string Fonte { get; set; }

        [Column("enunciado")]
        public string Enunciado { get; set; }

        [NotMapped]
        public List<Alternativa> Alternativas { get; set; }
    }
}
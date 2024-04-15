using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Item : EntidadeBase
    {
        public Item() { }
        public Item(long? id, long codigoItem, long areaconhecimentoId, long disciplinaId,
            long? matrizId, long? competenciaId, long? habilidadeId, long? anoMatrizId, long? dificuldadeSugeridaId,
            decimal? discriminacao, decimal? acertoCasual, decimal? dificuldade, long? assuntoId, long? subassuntoId,
            SituacaoItem? situacao, TipoItem? tipo, long? quantidadeAlternativaId, string palavrasChave, 
            decimal? parametroBTransformado, string mediaEhDesvio, string observacao, DateTime dataCriacao)
        {
            if (id > 0 && id != null)
            {
                Id = (long)id;
                DataAlteracao = DateTime.Now;
            }


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
            DataCriacao= dataCriacao;
            Fonte = fonte;
            Enunciado = enunciado;
            TextoBase= textoBase;
        }

        public long CodigoItem { get; set; }
        public long AreaconhecimentoId { get; set; }
        public long DisciplinaId { get; set; }
        public long? MatrizId { get; set; }
        public long? CompetenciaId { get; set; }
        public long? HabilidadeId { get; set; }
        public long? AnoMatrizId { get; set; }
        public long? DificuldadeSugeridaId { get; set; }
        public decimal? Discriminacao { get; set; }
        public decimal? AcertoCasual { get; set; }
        public decimal? Dificuldade { get; set; }
        public long? AssuntoId { get; set; }
        public long? SubAssuntoId { get; set; }
        public SituacaoItem? Situacao { get; set; }
        public TipoItem? Tipo { get; set; }
        public long? QuantidadeAlternativasId { get; set; }
        public string PalavrasChave { get; set; }
        public decimal? ParametroBTransformado { get; set; }
        public string MediaEhDesvio { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string TextoBase { get; set; }
        public string Fonte { get; set; }
        public string Enunciado { get; set; }


    }
}

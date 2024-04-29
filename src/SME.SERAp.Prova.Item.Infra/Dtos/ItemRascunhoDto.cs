using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos.Alternativa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemRascunhoDto
    {
        public long? Id { get; set; }
        public long CodigoItem { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da area de conhecimento")]
        [Range(1, long.MaxValue, ErrorMessage = "Area de conhecimento tem que ser maior que zero ")]
        public long AreaConhecimentoId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da disciplina")]
        [Range(1, long.MaxValue, ErrorMessage = "Disciplina tem que ser maior que zero ")]
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
        public string[] PalavrasChave { get; set; }
        public decimal? ParametroBTransformado { get; set; }
        public string MediaEhDesvio { get; set; }
        [MaxLength(100, ErrorMessage = "A observação pode ter no máximo 100 caracteres.")]
        public string Observacao { get; set; }

        public string TextoBase { get; set; }
        public string Fonte { get; set; }

        public string Enunciado { get; set; }

        public List<AlternativaRascunhoDto> AlternativasDto { get; set; }

        public long ArquivoVideoId { get; set; }
        public long ArquivoAudioId { get; set; }
    }
}

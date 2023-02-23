using System;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemDto
    {
        public long? Id { get; set; }
        public long CodigoItem { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da area de conhecimento")]
        [Range(1, long.MaxValue, ErrorMessage = "Area de conhecimento tem que ser maior que zero ")]
        public long AreaConhecimentoId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da disciplina")]
        [Range(1, long.MaxValue, ErrorMessage = "Disciplina tem que ser maior que zero ")]
        public long DisciplinaId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da Matriz")]
        [Range(1, long.MaxValue, ErrorMessage = "MatrizId tem que ser maior que zero ")]
        public long MatrizId { get; set; }

        [Required(ErrorMessage = "É necessário informar o id da Competencia")]
        [Range(1, long.MaxValue, ErrorMessage = "CompetenciaId tem que ser maior que zero ")]
        public long? CompetenciaId { get; set; }
        [Required(ErrorMessage = "É necessário informar o id da Habilidade")]
        [Range(1, long.MaxValue, ErrorMessage = "HabilidadeId tem que ser maior que zero ")]
        public long? HabilidadeId { get; set; }
        [Required(ErrorMessage = "É necessário informar o id do AnoMatriz")]
        [Range(1, long.MaxValue, ErrorMessage = "AnoMatrizId tem que ser maior que zero ")]
        public long? AnoMatrizId { get; set; }
        [Required(ErrorMessage = "É necessário informar o id da Dificuldade Sugerida")]
        [Range(1, long.MaxValue, ErrorMessage = "DificuldadeSugeridaId tem que ser maior que zero ")]
        public long? DificuldadeSugeridaId { get; set; }
        public decimal? Discriminacao { get; set; }
        public decimal? AcertoCasual { get; set; }
        public decimal? Dificuldade { get; set; }
    }
}

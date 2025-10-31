

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class FiltroItemsDto
    {
        public long? AreaConhecimentoId { get; set; }

        public string CodigoItem { get; set; }

        public long? DisciplinaId { get; set; }

        public long? MatrizId { get; set; }

        public long? CompetenciaId { get; set; }

        public long? DificuldadeSugeridaId { get; set; }

        public long? Situacao { get; set; }

        public long? HabilidadeId { get; set; }

        public int? Pagina { get; set; }

        public int? TamanhoPagina { get; set; }

    }
}

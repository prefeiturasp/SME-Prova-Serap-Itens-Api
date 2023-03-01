namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Item : EntidadeBase
    {
        public Item() { }   
        public Item(long? id, long codigoItem, long areaconhecimentoId, long disciplinaId,
            long? matrizId, long? competenciaId, long? habilidadeId, long? anoMatrizId, long? dificuldadeSugeridaId,
            decimal? discriminacao, decimal? acertoCasual, decimal? dificuldade)
        {
            if (id > 0 && id != null)
                Id = (long)id;

            CodigoItem = codigoItem;
            AreaconhecimentoId = areaconhecimentoId;
            MatrizId = matrizId;
            DisciplinaId = disciplinaId;
            CompetenciaId= competenciaId;   
            HabilidadeId= habilidadeId;
            AnoMatrizId= anoMatrizId;
            DificuldadeSugeridaId = dificuldadeSugeridaId;
            Discriminacao= discriminacao;
            AcertoCasual= acertoCasual;
            Dificuldade= dificuldade;
        }

        public long CodigoItem  { get; set; }
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
    }
}

using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class ItemAudio : EntidadeBase
    {
        public ItemAudio() { }

        public ItemAudio(long arquivoId, long itemId, int situacao, DateTime criadoEm) : this()
        {
            if (Id == 0)
                AlteradoEm = null;

            ArquivoId = arquivoId;
            ItemId = itemId;
            Situacao = situacao;
            CriadoEm = criadoEm;
        }

        public long ArquivoId { get; set; }
        public long ItemId { get; set; }
        public int Situacao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
    }
}
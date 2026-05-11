using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("item_audio")]
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

        [Column("arquivo_id")]
        public long ArquivoId { get; set; }

        [Column("item_id")]
        public long ItemId { get; set; }

        [Column("situacao")]
        public int Situacao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }
    }
}
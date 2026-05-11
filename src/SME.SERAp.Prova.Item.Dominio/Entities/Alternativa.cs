using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("alternativa")]
    public class Alternativa : EntidadeBase
    {
        public Alternativa() { }

        public Alternativa(string descricao, string justificativa, string numeracao,
            bool correta, int ordem, DateTime criadoEm, long itemId)
        {
            this.AlteradoEm = null;
            Descricao = descricao;
            Justificativa = justificativa;
            Numeracao = numeracao;
            Correta = correta;
            Ordem = ordem;
            CriadoEm = criadoEm;
            ItemId = itemId;
        }

        public Alternativa(long? id, string descricao, string justificativa, string numeracao,
            bool correta, int ordem, DateTime criadoEm, long itemId)
        {
            if (id.HasValue && id.Value > 0)
            {
                this.Id = id.Value;
                this.AlteradoEm = DateTime.Now;
            }
            else
            {
                this.AlteradoEm = null;
            }
            Descricao = descricao;
            Justificativa = justificativa;
            Numeracao = numeracao;
            Correta = correta;
            Ordem = ordem;
            CriadoEm = criadoEm;
            ItemId = itemId;
        }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("justificativa")]
        public string Justificativa { get; set; }

        [Column("numeracao")]
        public string Numeracao { get; set; }

        [Column("correta")]
        public bool Correta { get; set; }

        [Column("ordem")]
        public int? Ordem { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }

        [Column("item_id")]
        public long ItemId { get; set; }
    }
}
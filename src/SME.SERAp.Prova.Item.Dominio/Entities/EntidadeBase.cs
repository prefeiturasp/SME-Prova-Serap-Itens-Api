using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public abstract class EntidadeBase
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
    }
}
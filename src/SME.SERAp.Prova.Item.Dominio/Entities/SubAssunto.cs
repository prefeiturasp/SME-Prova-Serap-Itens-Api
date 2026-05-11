using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("subassunto")]
    public class SubAssunto : EntidadeBase
    {
        public SubAssunto() { }

        public SubAssunto(long? id, long legadoId, long assuntoId, string descricao, StatusGeral status)
        {
            if (id == null)
            {
                CriadoEm = AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;
            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            AssuntoId = assuntoId;
            Descricao = descricao;
            Status = (int)status;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("assunto_id")]
        public long AssuntoId { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
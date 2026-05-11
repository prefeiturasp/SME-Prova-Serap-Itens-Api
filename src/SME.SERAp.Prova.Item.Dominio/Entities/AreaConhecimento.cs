using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("area_conhecimento")]
    public class AreaConhecimento : EntidadeBase
    {
        public AreaConhecimento() { }

        public AreaConhecimento(long? id, long legadoId, string descricao, StatusGeral status)
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
            Descricao = descricao;
            Status = (int)status;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("codigo")]
        public long Codigo { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
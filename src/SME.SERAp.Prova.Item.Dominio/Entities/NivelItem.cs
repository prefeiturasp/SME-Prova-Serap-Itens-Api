using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("nivel_item")]
    public class NivelItem : EntidadeBase
    {
        public NivelItem() { }

        public NivelItem(long? id, string descricao, int ordem, StatusGeral status)
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

            Descricao = descricao;
            Ordem = ordem;
            Status = (int)status;
        }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("ordem")]
        public int Ordem { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
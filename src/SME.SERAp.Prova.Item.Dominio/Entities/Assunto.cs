using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("assunto")]
    public class Assunto : EntidadeBase
    {
        public Assunto() { }

        public Assunto(long? id, long legadoId, long disciplinaId, string descricao, StatusGeral status)
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
            DisciplinaId = disciplinaId;
            Descricao = descricao;
            Status = (int)status;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("disciplina_id")]
        public long DisciplinaId { get; set; }

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
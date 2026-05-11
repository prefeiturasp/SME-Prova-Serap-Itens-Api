using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("tipo_grade")]
    public class TipoGrade : EntidadeBase
    {
        public TipoGrade() { }

        public TipoGrade(long? id, long legadoId, long matrizId, string descricao, long ordem, StatusGeral status)
        {
            if (id == null)
            {
                CriadoEm = DateTime.Now;
                AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;
            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            MatrizId = matrizId;
            Descricao = descricao;
            Ordem = ordem;
            Status = (int)status;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("matriz_id")]
        public long MatrizId { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("ordem")]
        public long Ordem { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
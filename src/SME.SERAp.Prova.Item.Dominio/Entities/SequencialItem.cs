using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("sequencial_item")]
    public class SequencialItem : EntidadeBase
    {
        public SequencialItem() { }

        public SequencialItem(long? id, long codigoAreaConhecimento, long codigoDisciplina, long sequencial, DateTime? criadoEm = null)
        {
            if (id <= 0 || id == null)
                CriadoEm = AlteradoEm = DateTime.Now;
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
                CriadoEm = (DateTime)criadoEm;
            }

            Sequencial = sequencial;
            CodigoAreaConhecimento = codigoAreaConhecimento;
            CodigoDisciplina = codigoDisciplina;
        }

        [Column("codigo_area_conhecimento")]
        public long CodigoAreaConhecimento { get; set; }

        [Column("codigo_disciplina")]
        public long CodigoDisciplina { get; set; }

        [Column("sequencial")]
        public long Sequencial { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }
    }
}
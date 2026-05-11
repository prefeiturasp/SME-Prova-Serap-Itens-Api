using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("disciplina")]
    public class Disciplina : EntidadeBase
    {
        public Disciplina() { }

        public Disciplina(long? id, long legadoId, long areaConhecimentoId, string descricao, string nivelEnsino, StatusGeral status)
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
            NivelEnsino = nivelEnsino;
            Status = (int)status;
            AreaConhecimentoId = areaConhecimentoId;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("nivel_ensino")]
        public string NivelEnsino { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime AlteradoEm { get; set; }

        [Column("codigo")]
        public long Codigo { get; set; }

        [Column("area_conhecimento_id")]
        public long AreaConhecimentoId { get; set; }

        [Column("status")]
        public int Status { get; set; }
    }
}
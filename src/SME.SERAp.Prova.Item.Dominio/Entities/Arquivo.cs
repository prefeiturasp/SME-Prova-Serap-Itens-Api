using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    [Table("arquivo")]
    public class Arquivo : EntidadeBase
    {
        public Arquivo() { }

        public Arquivo(long legadoId, string nome, string caminho,
            string contentType, int situacao, DateTime criadoEm) : this()
        {
            if (Id == 0)
                AlteradoEm = null;

            LegadoId = legadoId;
            Nome = nome;
            Caminho = caminho;
            ContentType = contentType;
            Situacao = situacao;
            CriadoEm = criadoEm;
        }

        [Column("legado_id")]
        public long LegadoId { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("caminho")]
        public string Caminho { get; set; }

        [Column("content_type")]
        public string ContentType { get; set; }

        [Column("situacao")]
        public int Situacao { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("alterado_em")]
        public DateTime? AlteradoEm { get; set; }
    }
}
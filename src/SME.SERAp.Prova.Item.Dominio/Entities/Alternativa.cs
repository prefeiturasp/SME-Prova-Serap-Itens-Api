using SME.SERAp.Prova.Item.Dominio.Enums;
using System;


namespace SME.SERAp.Prova.Item.Dominio.Entities
{
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
            bool correta, int ordem, DateTime criadoEm , long itemId)
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
        
        public string Descricao { get; set; }
        public string Justificativa { get; set; }
        public string Numeracao { get; set; }
        public bool Correta { get; set; }
        public int? Ordem { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AlteradoEm { get; set; }
        public long ItemId { get; set; }
    }
}

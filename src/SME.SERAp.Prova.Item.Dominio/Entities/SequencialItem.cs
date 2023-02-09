using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class SequencialItem : EntidadeBase
    {
        public SequencialItem() { }
        public SequencialItem(long? id,long  codigoAreaConhecimento, long codigoDisciplina, long sequencial, DateTime? criadoEm = null)
        {
            if (id <= 0)
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
        public long CodigoAreaConhecimento { get; set; }
        public long CodigoDisciplina { get; set; }
        public long Sequencial { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
    }
}

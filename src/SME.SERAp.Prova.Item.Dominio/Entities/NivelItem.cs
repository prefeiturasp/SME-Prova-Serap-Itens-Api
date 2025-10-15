using SME.SERAp.Prova.Item.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class NivelItem : EntidadeBase
    {
        public NivelItem()
        {

        }

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

        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }
    }
}

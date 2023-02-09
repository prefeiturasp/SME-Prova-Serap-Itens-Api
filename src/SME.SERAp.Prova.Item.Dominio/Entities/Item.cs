
using SME.SERAp.Prova.Item.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Item : EntidadeBase
    {
        public Item() { }   
        public Item(long? id, long codigoItem, long areaconhecimentoId, long? matrizId, long disciplinaId) 
        {
            if (id != null)
                Id = (long)id;

            CodigoItem = codigoItem;
            AreaconhecimentoId = areaconhecimentoId;
            MatrizId = matrizId;
            DisciplinaId = disciplinaId;
        }

        public long CodigoItem  { get; set; }
        public long AreaconhecimentoId { get; set; }
        public long? MatrizId { get; set; }
        public long DisciplinaId { get; set; }
    }
}

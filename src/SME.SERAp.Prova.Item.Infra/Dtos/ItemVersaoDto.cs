using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemVersaoDto
    {
        public long Id { get; set; }
        public long CodigoItem { get; set; }
        public long VersaoItem { get; set; }
        public string DataCriacao { get; set; }

        public ItemVersaoDto(long id, long codigoItem, long versaoItem, DateTime dataCriacao)
        {
            Id = id;
            CodigoItem = codigoItem;
            VersaoItem = versaoItem;
            DataCriacao = dataCriacao.ToString("dd/MM/yyyy");
        }
    }
}
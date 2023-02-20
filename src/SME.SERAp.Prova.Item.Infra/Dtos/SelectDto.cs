using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class SelectDto
    {
        public SelectDto()
        {

        }

        public SelectDto(long valor, string descricao)
        {
            Valor = valor;
            Descricao = descricao;
        }
        public long Valor { get; set; }
        public string Descricao { get; set; }
    }
}

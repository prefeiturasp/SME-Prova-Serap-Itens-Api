using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class AlternativaResumoDto
    {
        public AlternativaResumoDto(long id, long itemId, string descricao, int ordem, string numeracao)
        {
            Id = id;
            ItemId = itemId;
            Descricao = descricao;
            Ordem = ordem;
            Numeracao = numeracao;
        }

        public long Id { get; set; }
        public long ItemId { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        public string Numeracao { get; set; }
    }
}
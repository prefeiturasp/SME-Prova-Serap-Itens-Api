using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Itens
{
    public class ItemListaDto
    {
        public long Id { get; set; }
        public string CodigoItem { get; set; }

        public string Enunciado { get; set; }

        public string Disciplina { get; set; }

        public string Dificuldade { get; set; }

        public long? Situacao { get; set; }

        public string SituacaoDesc { get; set; }

        public DateTime? DataCriacao { get; set; }

    }
}

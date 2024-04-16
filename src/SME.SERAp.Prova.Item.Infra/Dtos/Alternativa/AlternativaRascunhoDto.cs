using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Alternativa
{
    public class AlternativaRascunhoDto
    {
        public long? Id { get; set; }

        public string Descricao { get; set; }

        public string Justificativa { get; set; }

        public string Numeracao { get; set; }

        public bool Correta { get; set; }

        public int Ordem { get; set; }
    }
}

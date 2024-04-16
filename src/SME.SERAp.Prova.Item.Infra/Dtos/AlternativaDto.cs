using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class AltenativaDto
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "É necessário informar a descrição da alternativa")]
        public string Descricao { get; set; }

        public string Justificativa { get; set; }

        public string Numeracao { get; set; }

        public bool Correta { get; set; }

        public int Ordem { get; set; }
    }
}

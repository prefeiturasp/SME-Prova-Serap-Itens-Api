
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemRascunhoDto
    {
        public long? Id { get; set; }
        public long CodigoItem { get; set; }

 
        [Required(ErrorMessage = "Informe o seu email")]
        public long AreaConhecimentoLegadoId { get; set; }
        [Required]
        public long DisciplinaLegadoId { get; set; }

        public long? MatrizLegadoId { get; set; }



    }
}

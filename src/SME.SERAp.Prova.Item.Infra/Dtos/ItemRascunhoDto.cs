﻿
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

        [Required(ErrorMessage = "É necessário informar o id legado da area de conhecimento")]
        [Range(1, long.MaxValue, ErrorMessage = "Area de conhecimento tem que ser maior que zero ")]
        public long AreaConhecimentoLegadoId { get; set; }
        [Required(ErrorMessage = "É necessário informar o id legado da disciplina")]
        [Range(1, long.MaxValue, ErrorMessage = "Disciplina tem que ser maior que zero ")]
        public long DisciplinaLegadoId { get; set; }

        public long? MatrizLegadoId { get; set; }



    }
}

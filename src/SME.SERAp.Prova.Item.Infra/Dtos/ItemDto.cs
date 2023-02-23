using System;
using System.ComponentModel.DataAnnotations;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemDto
    {
            public long? Id { get; set; }
            public long CodigoItem { get; set; }

            [Required(ErrorMessage = "É necessário informar o id da area de conhecimento")]
            [Range(1, long.MaxValue, ErrorMessage = "Area de conhecimento tem que ser maior que zero ")]
            public long AreaConhecimentoId { get; set; }

            [Required(ErrorMessage = "É necessário informar o id da disciplina")]
            [Range(1, long.MaxValue, ErrorMessage = "Disciplina tem que ser maior que zero ")]
            public long DisciplinaId { get; set; }

            [Required(ErrorMessage = "É necessário informar o id da Matriz")]
            [Range(1, long.MaxValue, ErrorMessage = "Matriz tem que ser maior que zero ")]
            public long MatrizId { get; set; }
    }
}

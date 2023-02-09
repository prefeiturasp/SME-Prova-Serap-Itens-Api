using SME.SERAp.Prova.Item.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Matriz : EntidadeBase
    {

        public Matriz()
        {

        }

        public Matriz(long? id, long legadoId, long disciplinaId, string descricao, string modelo, StatusGeral status)
        {
            if (id == null)
            {
                CriadoEm = AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;

            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }

            LegadoId = legadoId;
            Descricao = descricao;
            Modelo = modelo;
            DisciplinaId = disciplinaId;
            Status = (int)status;
        }

        public long LegadoId { get; set; }
        public string Descricao { get; set; }
        public string Modelo { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public long DisciplinaId { get; set; }
        public int Status { get; set; }

    }
}

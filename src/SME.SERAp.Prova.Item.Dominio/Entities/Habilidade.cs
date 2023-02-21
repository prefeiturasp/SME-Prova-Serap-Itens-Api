using System;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Habilidade : EntidadeBase
    {
        public Habilidade(long? id, string codigo, long legadoId, long competenciaId, string descricao, StatusGeral status)
        {
            if (id == null)
            {
                CriadoEm = DateTime.Now;
                AlteradoEm = DateTime.Now;
                Status = (int)StatusGeral.Ativo;
            }
            else
            {
                Id = (long)id;
                AlteradoEm = DateTime.Now;
            }            
            
            Codigo = codigo;
            LegadoId = legadoId;
            CompetenciaId = competenciaId;
            Descricao = descricao;
            Status = (int)status;
        }

        public string Codigo { get; set; }
        public long LegadoId { get; set; }
        public long CompetenciaId { get; set; }
        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }        
        public int Status { get; set; }        
    }
}
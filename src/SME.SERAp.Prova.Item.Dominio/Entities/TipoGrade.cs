using System;
using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class TipoGrade : EntidadeBase
    {
        public TipoGrade(long? id, long legadoId, long matrizId, string descricao, long ordem, StatusGeral status)
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

            LegadoId = legadoId;
            MatrizId = matrizId;
            Descricao = descricao;
            Ordem = ordem;
            Status = (int)status;
        }

        public long LegadoId { get; set; }
        public long MatrizId { get; set; }
        public string Descricao { get; set; }
        public long Ordem { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }
        public int Status { get; set; }
    }
}
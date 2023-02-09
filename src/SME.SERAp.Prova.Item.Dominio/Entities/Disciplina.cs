using SME.SERAp.Prova.Item.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Disciplina : EntidadeBase
    {
        public Disciplina()
        {
        }

        public Disciplina(long? id, long legadoId, long areaConhecimentoId, string descricao, string nivelEnsino, StatusGeral status)
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
            NivelEnsino = nivelEnsino;
            Status = (int)status;
            AreaConhecimentoId = areaConhecimentoId;
        }
        public long LegadoId { get; set; }
        public string Descricao { get; set; }
        public string NivelEnsino { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }

        public long Codigo { get; set; }
        public long AreaConhecimentoId { get; set; }
        public int Status { get; set; }

    }
}
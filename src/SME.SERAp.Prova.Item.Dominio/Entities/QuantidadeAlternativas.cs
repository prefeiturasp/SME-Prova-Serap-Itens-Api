using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class QuantidadeAlternativas : EntidadeBase
    {

        public QuantidadeAlternativas()
        {

        }

        public QuantidadeAlternativas(long? id, long legadoId, string descricao, bool ehPadrao, int qtdAlternativas,  StatusGeral status)
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
            EhPadrao= ehPadrao;
            QtdAlternativas = qtdAlternativas;
            Status = (int)status;
        }

        public long LegadoId { get; set; }
        public bool EhPadrao { get; set; }
        public int QtdAlternativas { get; set; }

        public string Descricao { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime AlteradoEm { get; set; }

        public long Codigo { get; set; }
        public int Status { get; set; }

    }
}
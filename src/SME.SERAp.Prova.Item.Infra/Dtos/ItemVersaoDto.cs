using SME.SERAp.Prova.Item.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemVersaoDto
    {
        public long Id { get; set; }
        public string CodigoItem { get; set; }
        public long VersaoItem { get; set; }
        public string DataCriacao { get; set; }
        public SituacaoItem? Situacao { get; set; }

        public ItemVersaoDto(long id, string codigoItem, long versaoItem, DateTime dataCriacao, SituacaoItem? situacao)
        {
            Id = id;
            CodigoItem = codigoItem;
            VersaoItem = versaoItem;
            DataCriacao = dataCriacao.ToString("dd/MM/yyyy");
            Situacao = situacao;
        }
    }
}
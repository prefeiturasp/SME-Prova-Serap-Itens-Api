using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class ItemResumoDto
    {
        public ItemResumoDto() { }

        public ItemResumoDto(long id, string codigoItem, string textoBase, string enunciado, string fonte, long versaoItem, long quantidadeVersoes)
        {
            Id = id;
            CodigoItem = codigoItem;
            TextoBase = textoBase;
            Enunciado = enunciado;
            Fonte = fonte;
            VersaoItem = versaoItem;
            QuantidadeVersoes = quantidadeVersoes;
            VersoesDisponiveis = new List<ItemVersaoDto>();
            Alternativas = new List<AlternativaResumoDto>();
        }

        public long Id { get; set; }
        public string CodigoItem { get; set; }
        public string TextoBase { get; set; }
        public string Enunciado { get; set; }
        public string Fonte { get; set; }
        public long VersaoItem { get; set; }
        public long QuantidadeVersoes { get; set; }
        public List<ItemVersaoDto> VersoesDisponiveis { get; set; }
        public List<AlternativaResumoDto> Alternativas { get; set; }
    }
}
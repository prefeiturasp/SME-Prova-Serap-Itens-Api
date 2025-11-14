using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Infra.Dtos.Video
{
    public class ItemVideoDto
    {
        public long Id { get; set; }
        public long ArquivoId { get; set; }
        public string NomeArquivo { get; set; }
        public string ContentType { get; set; }
        public string Caminho { get; set; }
    }
}
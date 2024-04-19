using SME.SERAp.Prova.Item.Dominio.Enums;

namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class UploadArquivoDto
    {
        public int ContentLength { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string InputStream { get; set; } = string.Empty;
        public TipoArquivo FileType { get; set; }            
    }
}
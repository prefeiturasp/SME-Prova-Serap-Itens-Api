namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class RetornoUploadArquivoDto
    {
        public bool Success { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string FileLink { get; set; } = string.Empty;
        public long IdFile { get; set; }        
    }
}
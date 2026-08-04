namespace SME.SERAp.Prova.Item.Infra.Dtos.Itens
{
    public class AtualizarSituacaoItemDto
    {
        public string CodigoItem { get; set; }
        public long VersaoItem { get; set; }
        public int Situacao { get; set; }
    }
}
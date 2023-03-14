namespace SME.SERAp.Prova.Item.Infra.Dtos
{
    public class SelectDto
    {
        public SelectDto()
        {

        }

        public SelectDto(long valor, string descricao)
        {
            Valor = valor;
            Descricao = descricao;
        }
        public long Valor { get; set; }
        public string Descricao { get; set; }
    }
}

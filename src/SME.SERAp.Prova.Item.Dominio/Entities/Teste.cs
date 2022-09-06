namespace SME.SERAp.Prova.Item.Dominio.Entities
{
    public class Teste : EntidadeBase
    {
        public Teste() { }
        public Teste(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; set; }
    }
}

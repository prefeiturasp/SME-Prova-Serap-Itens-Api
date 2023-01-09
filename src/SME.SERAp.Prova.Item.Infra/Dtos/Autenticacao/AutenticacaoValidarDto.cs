namespace SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao
{
    public class AutenticacaoValidarDto
    {
        public AutenticacaoValidarDto(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}

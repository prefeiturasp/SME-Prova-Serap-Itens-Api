namespace SME.SERAp.Prova.Item.Infra.EnvironmentVariables
{
    public class ConnectionStringOptions
    {
        public static string Secao => "ConnectionStrings";
        public string ApiSerapItem { get; set; }
        public string ApiSerapItemLeitura { get; set; }
    }
}

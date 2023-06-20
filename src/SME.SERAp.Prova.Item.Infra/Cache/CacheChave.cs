namespace SME.SERAp.Prova.Item.Infra.Cache
{
    public static class CacheChave
    {
        public static string ObterChave(string chave, params object[] parametros)
        {
            return string.Format(chave, parametros);
        }

        /// <summary>
        /// Autenticação
        /// 0 - Código
        /// </summary>
        public const string Autenticacao = "serap-itens-auth-{0}";
    }
}

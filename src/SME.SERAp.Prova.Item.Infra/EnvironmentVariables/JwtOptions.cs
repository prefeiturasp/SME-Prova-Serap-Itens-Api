﻿namespace SME.SERAp.Prova.Item.Infra.EnvironmentVariables
{
    public class JwtOptions
    {
        public static string Secao => "Jwt";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiresInMinutes { get; set; }
        public string IssuerSigningKey { get; set; }
    }
}

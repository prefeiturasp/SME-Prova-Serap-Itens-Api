using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Autenticacao
{
    public class AutenticacaoRevalidarDtoTeste
    {
        [Fact]
        public void Deve_Criar_AutenticacaoRevalidarDto_Com_Construtor_Padrao()
        {
            var dto = new AutenticacaoRevalidarDto();

            Assert.NotNull(dto);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Vazio()
        {
            var dto = new AutenticacaoRevalidarDto
            {
                Token = string.Empty
            };

            Assert.Equal(string.Empty, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Nulo()
        {
            var dto = new AutenticacaoRevalidarDto
            {
                Token = null
            };

            Assert.Null(dto.Token);
        }

        [Fact]
        public void Deve_Inicializar_Token_Como_Null_Por_Padrao()
        {
            var dto = new AutenticacaoRevalidarDto();

            Assert.Null(dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Longo()
        {
            var tokenLongo = new string('A', 5000);

            var dto = new AutenticacaoRevalidarDto
            {
                Token = tokenLongo
            };

            Assert.Equal(tokenLongo, dto.Token);
            Assert.Equal(5000, dto.Token.Length);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Caracteres_Especiais()
        {
            var token = "token@#$%&*()_+-=[]{}|;':\"<>,.?/\\~`";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_JWT_Valido()
        {
            var tokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = tokenJWT
            };

            Assert.Equal(tokenJWT, dto.Token);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Token_Apos_Criacao()
        {
            var dto = new AutenticacaoRevalidarDto
            {
                Token = "tokenInicial"
            };

            dto.Token = "tokenAlterado";

            Assert.Equal("tokenAlterado", dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Espacos()
        {
            var token = "token com espaços no meio";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Quebras_De_Linha()
        {
            var token = "token\ncom\nquebras\nde\nlinha";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Unicode()
        {
            var token = "token_with_unicode_🔐🔑🗝️";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Tabs()
        {
            var token = "token\tcom\ttabs";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Com_Valores_Diferentes()
        {
            var dto1 = new AutenticacaoRevalidarDto { Token = "token1" };
            var dto2 = new AutenticacaoRevalidarDto { Token = "token2" };
            var dto3 = new AutenticacaoRevalidarDto { Token = "token3" };

            Assert.NotEqual(dto1.Token, dto2.Token);
            Assert.NotEqual(dto2.Token, dto3.Token);
            Assert.NotEqual(dto1.Token, dto3.Token);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Com_Mesmo_Token()
        {
            var token = "mesmToken123";
            var dto1 = new AutenticacaoRevalidarDto { Token = token };
            var dto2 = new AutenticacaoRevalidarDto { Token = token };

            Assert.Equal(dto1.Token, dto2.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Apenas_Com_Numeros()
        {
            var token = "1234567890";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Apenas_Com_Letras()
        {
            var token = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Alfanumerico()
        {
            var token = "Token123ABC456def789";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Underscores_E_Hifens()
        {
            var token = "token_com-underscores_e-hifens";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Pontos()
        {
            var token = "token.com.pontos.separadores";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Alteracao_De_Token_Para_Null()
        {
            var dto = new AutenticacaoRevalidarDto
            {
                Token = "tokenInicial"
            };

            dto.Token = null;

            Assert.Null(dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Alteracao_De_Token_Para_Vazio()
        {
            var dto = new AutenticacaoRevalidarDto
            {
                Token = "tokenInicial"
            };

            dto.Token = string.Empty;

            Assert.Equal(string.Empty, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Espacos_No_Inicio_E_Fim()
        {
            var token = "  token com espacos  ";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Base64()
        {
            var token = "VG9rZW5CYXNlNjRFbmNvZGVk";

            var dto = new AutenticacaoRevalidarDto
            {
                Token = token
            };

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Manter_Case_Sensitivity_Do_Token()
        {
            var tokenMinusculo = "tokenminusculo";
            var tokenMaiusculo = "TOKENMAIUSCULO";
            var tokenMisto = "TokenMisto";

            var dto1 = new AutenticacaoRevalidarDto { Token = tokenMinusculo };
            var dto2 = new AutenticacaoRevalidarDto { Token = tokenMaiusculo };
            var dto3 = new AutenticacaoRevalidarDto { Token = tokenMisto };

            Assert.Equal(tokenMinusculo, dto1.Token);
            Assert.Equal(tokenMaiusculo, dto2.Token);
            Assert.Equal(tokenMisto, dto3.Token);
            Assert.NotEqual(dto1.Token, dto2.Token);
        }
    }
}
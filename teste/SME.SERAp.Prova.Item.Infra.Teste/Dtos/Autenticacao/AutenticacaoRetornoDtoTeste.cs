using System;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Autenticacao
{
    public class AutenticacaoRetornoDtoTeste
    {
        [Fact]
        public void Deve_Criar_AutenticacaoRetornoDto_Com_Construtor()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
            var dataHoraExpiracao = new DateTime(2024, 12, 31, 23, 59, 59);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(token, dto.Token);
            Assert.Equal(dataHoraExpiracao, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var token = "abc123token456xyz";
            var dataHoraExpiracao = new DateTime(2025, 6, 15, 14, 30, 0);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao)
            {
                Token = "novoToken789",
                DataHoraExpiracao = new DateTime(2025, 7, 20, 10, 0, 0)
            };

            Assert.Equal("novoToken789", dto.Token);
            Assert.Equal(new DateTime(2025, 7, 20, 10, 0, 0), dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_Token_Vazio()
        {
            var token = string.Empty;
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(string.Empty, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Nulo()
        {
            string token = null;
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Null(dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Longo()
        {
            var tokenLongo = new string('A', 5000);
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(tokenLongo, dataHoraExpiracao);

            Assert.Equal(tokenLongo, dto.Token);
            Assert.Equal(5000, dto.Token.Length);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Caracteres_Especiais()
        {
            var token = "token@#$%&*()_+-=[]{}|;':\"<>,.?/\\~`";
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_DataHoraExpiracao_No_Passado()
        {
            var token = "tokenExpirado";
            var dataHoraExpiracao = new DateTime(2020, 1, 1, 0, 0, 0);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(dataHoraExpiracao, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_DataHoraExpiracao_No_Futuro()
        {
            var token = "tokenFuturo";
            var dataHoraExpiracao = new DateTime(2099, 12, 31, 23, 59, 59);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(dataHoraExpiracao, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_DataHoraExpiracao_Atual()
        {
            var token = "tokenAtual";
            var dataHoraExpiracao = DateTime.Now;

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(dataHoraExpiracao, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_DataHoraExpiracao_MinValue()
        {
            var token = "tokenMin";
            var dataHoraExpiracao = DateTime.MinValue;

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(DateTime.MinValue, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_DataHoraExpiracao_MaxValue()
        {
            var token = "tokenMax";
            var dataHoraExpiracao = DateTime.MaxValue;

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(DateTime.MaxValue, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Preservar_Milissegundos_Na_DataHoraExpiracao()
        {
            var token = "tokenPreciso";
            var dataHoraExpiracao = new DateTime(2024, 6, 15, 14, 30, 45, 123);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(2024, dto.DataHoraExpiracao.Year);
            Assert.Equal(6, dto.DataHoraExpiracao.Month);
            Assert.Equal(15, dto.DataHoraExpiracao.Day);
            Assert.Equal(14, dto.DataHoraExpiracao.Hour);
            Assert.Equal(30, dto.DataHoraExpiracao.Minute);
            Assert.Equal(45, dto.DataHoraExpiracao.Second);
            Assert.Equal(123, dto.DataHoraExpiracao.Millisecond);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Token_Apos_Criacao()
        {
            var dto = new AutenticacaoRetornoDto("tokenInicial", DateTime.Now);

            dto.Token = "tokenAlterado";

            Assert.Equal("tokenAlterado", dto.Token);
        }

        [Fact]
        public void Deve_Permitir_Alterar_DataHoraExpiracao_Apos_Criacao()
        {
            var dataInicial = new DateTime(2024, 1, 1);
            var dto = new AutenticacaoRetornoDto("token", dataInicial);

            var novaData = new DateTime(2025, 12, 31);
            dto.DataHoraExpiracao = novaData;

            Assert.Equal(novaData, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Criar_Dto_Com_Token_JWT_Valido()
        {
            var tokenJWT = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            var dataHoraExpiracao = new DateTime(2024, 12, 31);

            var dto = new AutenticacaoRetornoDto(tokenJWT, dataHoraExpiracao);

            Assert.Equal(tokenJWT, dto.Token);
            Assert.Equal(dataHoraExpiracao, dto.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Com_Valores_Diferentes()
        {
            var dto1 = new AutenticacaoRetornoDto("token1", new DateTime(2024, 1, 1));
            var dto2 = new AutenticacaoRetornoDto("token2", new DateTime(2024, 2, 1));
            var dto3 = new AutenticacaoRetornoDto("token3", new DateTime(2024, 3, 1));

            Assert.NotEqual(dto1.Token, dto2.Token);
            Assert.NotEqual(dto2.Token, dto3.Token);
            Assert.NotEqual(dto1.DataHoraExpiracao, dto2.DataHoraExpiracao);
            Assert.NotEqual(dto2.DataHoraExpiracao, dto3.DataHoraExpiracao);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Espacos()
        {
            var token = "token com espaços no meio";
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Com_Quebras_De_Linha()
        {
            var token = "token\ncom\nquebras\nde\nlinha";
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(token, dto.Token);
        }

        [Fact]
        public void Deve_Aceitar_Token_Unicode()
        {
            var token = "token_with_unicode_🔐🔑🗝️";
            var dataHoraExpiracao = new DateTime(2024, 1, 1);

            var dto = new AutenticacaoRetornoDto(token, dataHoraExpiracao);

            Assert.Equal(token, dto.Token);
        }
    }
}
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Dtos.Autenticacao
{
    public class AutenticacaoValidarDtoTeste
    {
        [Fact]
        public void Deve_Criar_AutenticacaoValidarDto_Com_Construtor()
        {
            var codigo = "ABC123";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Atribuir_Propriedades_Corretamente()
        {
            var codigo = "XYZ789";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Vazio()
        {
            var codigo = string.Empty;

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(string.Empty, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Nulo()
        {
            string codigo = null;

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Null(dto.Codigo);
        }

        [Fact]
        public void Deve_Permitir_Alterar_Codigo_Apos_Criacao()
        {
            var dto = new AutenticacaoValidarDto("codigoInicial");

            dto.Codigo = "codigoAlterado";

            Assert.Equal("codigoAlterado", dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Numerico()
        {
            var codigo = "123456";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Alfanumerico()
        {
            var codigo = "ABC123XYZ";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Apenas_Letras()
        {
            var codigo = "ABCDEFGH";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Caracteres_Especiais()
        {
            var codigo = "codigo@#$%&*()_+-=";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Longo()
        {
            var codigoLongo = new string('A', 5000);

            var dto = new AutenticacaoValidarDto(codigoLongo);

            Assert.Equal(codigoLongo, dto.Codigo);
            Assert.Equal(5000, dto.Codigo.Length);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Um_Caractere()
        {
            var codigo = "A";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Espacos()
        {
            var codigo = "codigo com espacos";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Espacos_No_Inicio_E_Fim()
        {
            var codigo = "  codigo  ";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Quebras_De_Linha()
        {
            var codigo = "codigo\ncom\nquebras";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Tabs()
        {
            var codigo = "codigo\tcom\ttabs";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Unicode()
        {
            var codigo = "código_çãõáéíóú_🔐";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Hifens()
        {
            var codigo = "ABC-123-XYZ-789";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Underscores()
        {
            var codigo = "codigo_com_underscores";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Pontos()
        {
            var codigo = "codigo.com.pontos";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Com_Valores_Diferentes()
        {
            var dto1 = new AutenticacaoValidarDto("codigo1");
            var dto2 = new AutenticacaoValidarDto("codigo2");
            var dto3 = new AutenticacaoValidarDto("codigo3");

            Assert.NotEqual(dto1.Codigo, dto2.Codigo);
            Assert.NotEqual(dto2.Codigo, dto3.Codigo);
            Assert.NotEqual(dto1.Codigo, dto3.Codigo);
        }

        [Fact]
        public void Deve_Criar_Multiplos_Dtos_Com_Mesmo_Codigo()
        {
            var codigo = "mesmoCodigo123";
            var dto1 = new AutenticacaoValidarDto(codigo);
            var dto2 = new AutenticacaoValidarDto(codigo);

            Assert.Equal(dto1.Codigo, dto2.Codigo);
        }

        [Fact]
        public void Deve_Manter_Case_Sensitivity_Do_Codigo()
        {
            var codigoMinusculo = "codigominusculo";
            var codigoMaiusculo = "CODIGOMAIUSCULO";
            var codigoMisto = "CodigoMisto";

            var dto1 = new AutenticacaoValidarDto(codigoMinusculo);
            var dto2 = new AutenticacaoValidarDto(codigoMaiusculo);
            var dto3 = new AutenticacaoValidarDto(codigoMisto);

            Assert.Equal(codigoMinusculo, dto1.Codigo);
            Assert.Equal(codigoMaiusculo, dto2.Codigo);
            Assert.Equal(codigoMisto, dto3.Codigo);
            Assert.NotEqual(dto1.Codigo, dto2.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Alteracao_De_Codigo_Para_Null()
        {
            var dto = new AutenticacaoValidarDto("codigoInicial");

            dto.Codigo = null;

            Assert.Null(dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Alteracao_De_Codigo_Para_Vazio()
        {
            var dto = new AutenticacaoValidarDto("codigoInicial");

            dto.Codigo = string.Empty;

            Assert.Equal(string.Empty, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_GUID()
        {
            var codigo = "550e8400-e29b-41d4-a716-446655440000";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Base64()
        {
            var codigo = "Q29kaWdvQmFzZTY0RW5jb2RlZA==";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Hexadecimal()
        {
            var codigo = "0x1A2B3C4D5E6F";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Barras()
        {
            var codigo = "codigo/com/barras";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Backslashes()
        {
            var codigo = "codigo\\com\\backslashes";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Parenteses()
        {
            var codigo = "codigo(com)parenteses";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Colchetes()
        {
            var codigo = "codigo[com]colchetes";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Chaves()
        {
            var codigo = "codigo{com}chaves";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Simbolos_Matematicos()
        {
            var codigo = "codigo+com-simbolos*matematicos/e=outros";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Aspas_Simples()
        {
            var codigo = "codigo'com'aspas'simples";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }

        [Fact]
        public void Deve_Aceitar_Codigo_Com_Aspas_Duplas()
        {
            var codigo = "codigo\"com\"aspas\"duplas";

            var dto = new AutenticacaoValidarDto(codigo);

            Assert.Equal(codigo, dto.Codigo);
        }
    }
}
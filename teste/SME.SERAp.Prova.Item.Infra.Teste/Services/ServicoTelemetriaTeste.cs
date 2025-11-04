using System;
using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Services;
using Xunit;

namespace SME.SERAp.Prova.Item.Infra.Teste.Services
{
    public class ServicoTelemetriaTeste
    {
        private TelemetriaOptions telemetriaOptions;
        private ServicoTelemetria servicoTelemetria;

        public ServicoTelemetriaTeste()
        {
        }

        private void InicializarServico(bool apm = false)
        {
            telemetriaOptions = new TelemetriaOptions
            {
                Apm = apm
            };

            servicoTelemetria = new ServicoTelemetria(telemetriaOptions);
        }

        [Fact]
        public void Construtor_Com_TelemetriaOptions_Null_Deve_Lancar_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => new ServicoTelemetria(null));
        }

        [Fact]
        public void Construtor_Com_TelemetriaOptions_Valido_Deve_Criar_Instancia()
        {
            InicializarServico(apm: false);

            Assert.NotNull(servicoTelemetria);
        }

        [Fact]
        public void Propriedade_Apm_Deve_Retornar_Valor_De_TelemetriaOptions_True()
        {
            InicializarServico(apm: true);

            Assert.True(servicoTelemetria.Apm);
        }

        [Fact]
        public void Propriedade_Apm_Deve_Retornar_Valor_De_TelemetriaOptions_False()
        {
            InicializarServico(apm: false);

            Assert.False(servicoTelemetria.Apm);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Executar_Acao()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<string>(
                () => Task.FromResult<object>("teste"),
                "acao",
                "telemetria",
                "valor");

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_Objeto_Complexo()
        {
            InicializarServico(apm: false);

            var objetoComplexo = new { Id = 1, Nome = "Teste" };
            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<object>(
                () => Task.FromResult<object>(objetoComplexo),
                "acao",
                "telemetria",
                "valor");

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Teste", resultado.Nome);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_Null()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<string>(
                () => Task.FromResult<object>(null),
                "acao",
                "telemetria",
                "valor");

            Assert.Null(resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_Numero()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<int>(
                () => Task.FromResult<object>(42),
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(42, resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_Boolean()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<bool>(
                () => Task.FromResult<object>(true),
                "acao",
                "telemetria",
                "valor");

            Assert.True(resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Executar_Acao()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<string>(
                () => "teste",
                "acao",
                "telemetria",
                "valor");

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_Objeto_Complexo()
        {
            InicializarServico(apm: false);

            var objetoComplexo = new { Id = 1, Nome = "Teste" };
            var resultado = servicoTelemetria.RegistrarComRetorno<object>(
                () => objetoComplexo,
                "acao",
                "telemetria",
                "valor");

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("Teste", resultado.Nome);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_Null()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<string>(
                () => null,
                "acao",
                "telemetria",
                "valor");

            Assert.Null(resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_Numero()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<int>(
                () => 100,
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(100, resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_Decimal()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<decimal>(
                () => 99.99m,
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(99.99m, resultado);
        }

        [Fact]
        public void Registrar_Apm_False_Deve_Executar_Acao()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(
                () => executou = true,
                "acao",
                "telemetria",
                "valor");

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Deve_Executar_Acao_Multiplas_Vezes()
        {
            InicializarServico(apm: false);

            int contador = 0;
            servicoTelemetria.Registrar(() => contador++, "acao1", "telemetria", "valor");
            servicoTelemetria.Registrar(() => contador++, "acao2", "telemetria", "valor");
            servicoTelemetria.Registrar(() => contador++, "acao3", "telemetria", "valor");

            Assert.Equal(3, contador);
        }

        [Fact]
        public void Registrar_Apm_False_Deve_Executar_Acao_Com_Efeito_Colateral()
        {
            InicializarServico(apm: false);

            var lista = new System.Collections.Generic.List<string>();
            servicoTelemetria.Registrar(
                () => lista.Add("item"),
                "acao",
                "telemetria",
                "valor");

            Assert.Single(lista);
            Assert.Equal("item", lista[0]);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Deve_Executar_Acao()
        {
            InicializarServico(apm: false);

            bool executou = false;
            await servicoTelemetria.RegistrarAsync(
                async () => { await Task.Delay(1); executou = true; },
                "acao",
                "telemetria",
                "valor");

            Assert.True(executou);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Deve_Executar_Acao_Sem_Delay()
        {
            InicializarServico(apm: false);

            bool executou = false;
            await servicoTelemetria.RegistrarAsync(
                () => { executou = true; return Task.CompletedTask; },
                "acao",
                "telemetria",
                "valor");

            Assert.True(executou);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Deve_Executar_Acao_Multiplas_Vezes()
        {
            InicializarServico(apm: false);

            int contador = 0;
            await servicoTelemetria.RegistrarAsync(async () => { await Task.Delay(1); contador++; }, "acao1", "telemetria", "valor");
            await servicoTelemetria.RegistrarAsync(async () => { await Task.Delay(1); contador++; }, "acao2", "telemetria", "valor");
            await servicoTelemetria.RegistrarAsync(async () => { await Task.Delay(1); contador++; }, "acao3", "telemetria", "valor");

            Assert.Equal(3, contador);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Deve_Executar_Acao_Com_Efeito_Colateral()
        {
            InicializarServico(apm: false);

            var lista = new System.Collections.Generic.List<string>();
            await servicoTelemetria.RegistrarAsync(
                async () => { await Task.Delay(1); lista.Add("item"); },
                "acao",
                "telemetria",
                "valor");

            Assert.Single(lista);
            Assert.Equal("item", lista[0]);
        }

        [Fact]
        public void Registrar_Apm_False_Com_AcaoNome_Vazio_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, string.Empty, "telemetria", "valor");

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Com_AcaoNome_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, null, "telemetria", "valor");

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Com_TelemetriaNome_Vazio_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, "acao", string.Empty, "valor");

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Com_TelemetriaNome_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, "acao", null, "valor");

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Com_TelemetriaValor_Vazio_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, "acao", "telemetria", string.Empty);

            Assert.True(executou);
        }

        [Fact]
        public void Registrar_Apm_False_Com_TelemetriaValor_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            servicoTelemetria.Registrar(() => executou = true, "acao", "telemetria", null);

            Assert.True(executou);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Com_Parametros_Vazios_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            await servicoTelemetria.RegistrarAsync(
                () => { executou = true; return Task.CompletedTask; },
                string.Empty,
                string.Empty,
                string.Empty);

            Assert.True(executou);
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Com_Parametros_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            bool executou = false;
            await servicoTelemetria.RegistrarAsync(
                () => { executou = true; return Task.CompletedTask; },
                null,
                null,
                null);

            Assert.True(executou);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Com_Parametros_Vazios_Deve_Executar()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<string>(
                () => Task.FromResult<object>("teste"),
                string.Empty,
                string.Empty,
                string.Empty);

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Com_Parametros_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<string>(
                () => Task.FromResult<object>("teste"),
                null,
                null,
                null);

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Com_Parametros_Vazios_Deve_Executar()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<string>(
                () => "teste",
                string.Empty,
                string.Empty,
                string.Empty);

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Com_Parametros_Null_Deve_Executar()
        {
            InicializarServico(apm: false);

            var resultado = servicoTelemetria.RegistrarComRetorno<string>(
                () => "teste",
                null,
                null,
                null);

            Assert.Equal("teste", resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_Lista()
        {
            InicializarServico(apm: false);

            var lista = new System.Collections.Generic.List<int> { 1, 2, 3 };
            var resultado = servicoTelemetria.RegistrarComRetorno<System.Collections.Generic.List<int>>(
                () => lista,
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(3, resultado.Count);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_Lista()
        {
            InicializarServico(apm: false);

            var lista = new System.Collections.Generic.List<int> { 1, 2, 3 };
            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<System.Collections.Generic.List<int>>(
                () => Task.FromResult<object>(lista),
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(3, resultado.Count);
            Assert.Equal(lista, resultado);
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Deve_Retornar_DateTime()
        {
            InicializarServico(apm: false);

            var dataEsperada = new DateTime(2024, 1, 1);
            var resultado = servicoTelemetria.RegistrarComRetorno<DateTime>(
                () => dataEsperada,
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(dataEsperada, resultado);
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Deve_Retornar_DateTime()
        {
            InicializarServico(apm: false);

            var dataEsperada = new DateTime(2024, 1, 1);
            var resultado = await servicoTelemetria.RegistrarComRetornoAsync<DateTime>(
                () => Task.FromResult<object>(dataEsperada),
                "acao",
                "telemetria",
                "valor");

            Assert.Equal(dataEsperada, resultado);
        }

        [Fact]
        public void Registrar_Apm_False_Nao_Deve_Capturar_Excecao()
        {
            InicializarServico(apm: false);

            Assert.Throws<InvalidOperationException>(() =>
                servicoTelemetria.Registrar(
                    () => throw new InvalidOperationException("Erro intencional"),
                    "acao",
                    "telemetria",
                    "valor"));
        }

        [Fact]
        public async Task RegistrarAsync_Apm_False_Nao_Deve_Capturar_Excecao()
        {
            InicializarServico(apm: false);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await servicoTelemetria.RegistrarAsync(
                    () => throw new InvalidOperationException("Erro intencional"),
                    "acao",
                    "telemetria",
                    "valor"));
        }

        [Fact]
        public void RegistrarComRetorno_Apm_False_Nao_Deve_Capturar_Excecao()
        {
            InicializarServico(apm: false);

            Assert.Throws<InvalidOperationException>(() =>
                servicoTelemetria.RegistrarComRetorno<string>(
                    () => throw new InvalidOperationException("Erro intencional"),
                    "acao",
                    "telemetria",
                    "valor"));
        }

        [Fact]
        public async Task RegistrarComRetornoAsync_Apm_False_Nao_Deve_Capturar_Excecao()
        {
            InicializarServico(apm: false);

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await servicoTelemetria.RegistrarComRetornoAsync<string>(
                    () => throw new InvalidOperationException("Erro intencional"),
                    "acao",
                    "telemetria",
                    "valor"));
        }
    }
}

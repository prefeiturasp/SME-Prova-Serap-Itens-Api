using Microsoft.Extensions.Logging;
using Moq;
using SME.SERAp.Prova.Item.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Item.Infra.Interfaces;
using SME.SERAp.Prova.Item.Infra.Services;
using System;
using Xunit;
using static SME.SERAp.Prova.Item.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Item.Infra.Teste.Services
{
    public class ServicoLogTeste
    {
        private readonly Mock<ILogger<ServicoLog>> mockLogger;
        private readonly Mock<IServicoTelemetria> mockServicoTelemetria;
        private readonly RabbitLogOptions rabbitLogOptions;

        public ServicoLogTeste()
        {
            mockLogger = new Mock<ILogger<ServicoLog>>();
            mockServicoTelemetria = new Mock<IServicoTelemetria>();
            rabbitLogOptions = new RabbitLogOptions
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_Logger_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ServicoLog(null, mockServicoTelemetria.Object, rabbitLogOptions));
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_ServicoTelemetria_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ServicoLog(mockLogger.Object, null, rabbitLogOptions));
        }

        [Fact]
        public void Deve_Lancar_ArgumentNullException_Quando_RabbitLogOptions_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, null));
        }

        [Fact]
        public void Deve_Criar_ServicoLog_Com_Dependencias_Validas()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            Assert.NotNull(servico);
        }

        [Fact]
        public void Deve_Registrar_Exception()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var exception = new Exception("Erro de teste");

            servico.Registrar(exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_Nivel_Erro_E_Observacoes()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var nivel = LogNivel.Critico;
            var erro = "Erro crítico";
            var observacoes = "Observações do erro";
            var stackTrace = "Stack trace do erro";

            servico.Registrar(nivel, erro, observacoes, stackTrace);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Mensagem_Com_Exception()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var mensagem = "Erro ao processar requisição";
            var exception = new Exception("Detalhes da exceção");

            servico.Registrar(mensagem, exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_LogMensagem()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var logMensagem = new LogMensagem(
                "Mensagem de teste",
                LogNivel.Informacao,
                "Observação de teste",
                "Stack trace",
                "Exceção interna"
            );

            servico.Registrar(logMensagem);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_LogNivel_E_Exception()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var nivel = LogNivel.Negocio;
            var exception = new Exception("Erro de negócio");

            servico.Registrar(nivel, exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_Propriedades_Corretas()
        {
            var mensagem = "Teste de mensagem";
            var nivel = LogNivel.Informacao;
            var observacao = "Observação de teste";
            var rastreamento = "Stack trace teste";
            var excecaoInterna = "Exceção interna teste";
            var projeto = "SME_SERAp_Prova_Item_Api";

            var dataAntes = DateTime.Now;
            var logMensagem = new LogMensagem(mensagem, nivel, observacao, rastreamento, excecaoInterna, projeto);
            var dataDepois = DateTime.Now;

            Assert.Equal(mensagem, logMensagem.Mensagem);
            Assert.Equal(nivel, logMensagem.Nivel);
            Assert.Equal(observacao, logMensagem.Observacao);
            Assert.Equal(projeto, logMensagem.Projeto);
            Assert.Equal(rastreamento, logMensagem.Rastreamento);
            Assert.Equal(excecaoInterna, logMensagem.ExcecaoInterna);
            Assert.InRange(logMensagem.DataHora, dataAntes, dataDepois);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_Projeto_Padrao()
        {
            var logMensagem = new LogMensagem("Teste", LogNivel.Informacao, "Obs");

            Assert.Equal("SME_SERAp_Prova_Item_Api", logMensagem.Projeto);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_Rastreamento_Nulo()
        {
            var logMensagem = new LogMensagem("Teste", LogNivel.Informacao, "Obs", null);

            Assert.Null(logMensagem.Rastreamento);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_ExcecaoInterna_Nula()
        {
            var logMensagem = new LogMensagem("Teste", LogNivel.Informacao, "Obs", "Stack", null);

            Assert.Null(logMensagem.ExcecaoInterna);
        }

        [Fact]
        public void Deve_Ter_LogNivel_Informacao_Com_Valor_1()
        {
            Assert.Equal(1, (int)LogNivel.Informacao);
        }

        [Fact]
        public void Deve_Ter_LogNivel_Critico_Com_Valor_2()
        {
            Assert.Equal(2, (int)LogNivel.Critico);
        }

        [Fact]
        public void Deve_Ter_LogNivel_Negocio_Com_Valor_3()
        {
            Assert.Equal(3, (int)LogNivel.Negocio);
        }

        [Fact]
        public void Deve_Registrar_Exception_Com_Nivel_Critico()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var exception = new InvalidOperationException("Operação inválida");

            servico.Registrar(exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_Nivel_Informacao()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            servico.Registrar(LogNivel.Informacao, "Informação", "Obs", "Stack");

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_Nivel_Negocio()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            servico.Registrar(LogNivel.Negocio, "Regra de negócio", "Obs", "Stack");

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Permitir_Modificacao_Propriedades_LogMensagem()
        {
            var logMensagem = new LogMensagem("Teste", LogNivel.Informacao, "Obs");

            logMensagem.Mensagem = "Nova mensagem";
            logMensagem.Nivel = LogNivel.Critico;
            logMensagem.Observacao = "Nova observação";
            logMensagem.Projeto = "Novo projeto";
            logMensagem.Rastreamento = "Novo rastreamento";
            logMensagem.ExcecaoInterna = "Nova exceção";
            var novaData = DateTime.Now.AddDays(-1);
            logMensagem.DataHora = novaData;

            Assert.Equal("Nova mensagem", logMensagem.Mensagem);
            Assert.Equal(LogNivel.Critico, logMensagem.Nivel);
            Assert.Equal("Nova observação", logMensagem.Observacao);
            Assert.Equal("Novo projeto", logMensagem.Projeto);
            Assert.Equal("Novo rastreamento", logMensagem.Rastreamento);
            Assert.Equal("Nova exceção", logMensagem.ExcecaoInterna);
            Assert.Equal(novaData, logMensagem.DataHora);
        }

        [Fact]
        public void Deve_Registrar_Multiplas_Exceptions()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            servico.Registrar(new Exception("Erro 1"));
            servico.Registrar(new Exception("Erro 2"));
            servico.Registrar(new Exception("Erro 3"));

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Exactly(3));
        }

        [Fact]
        public void Deve_Registrar_Com_StackTrace_Vazio()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            servico.Registrar(LogNivel.Informacao, "Erro", "Obs", string.Empty);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_StackTrace_Nulo()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);

            servico.Registrar(LogNivel.Informacao, "Erro", "Obs", null);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_Diferentes_Niveis()
        {
            var logInfo = new LogMensagem("Teste", LogNivel.Informacao, "Obs");
            var logCritico = new LogMensagem("Teste", LogNivel.Critico, "Obs");
            var logNegocio = new LogMensagem("Teste", LogNivel.Negocio, "Obs");

            Assert.Equal(LogNivel.Informacao, logInfo.Nivel);
            Assert.Equal(LogNivel.Critico, logCritico.Nivel);
            Assert.Equal(LogNivel.Negocio, logNegocio.Nivel);
        }

        [Fact]
        public void Deve_Registrar_Exception_Com_Message_E_StackTrace()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var exception = new Exception("Mensagem de erro");

            servico.Registrar(exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Registrar_Com_Mensagem_E_Exception_ArgumentNullException()
        {
            var servico = new ServicoLog(mockLogger.Object, mockServicoTelemetria.Object, rabbitLogOptions);
            var exception = new ArgumentNullException("parametro", "Parâmetro não pode ser nulo");

            servico.Registrar("Erro ao processar parâmetro", exception);

            mockServicoTelemetria.Verify(
                x => x.Registrar(It.IsAny<Action>(), "RabbitMQ", "Salvar Log Via Rabbit", "ApplicationLog"),
                Times.Once);
        }

        [Fact]
        public void Deve_Criar_LogMensagem_Com_DataHora_Atual()
        {
            var dataAntes = DateTime.Now;
            var logMensagem = new LogMensagem("Teste", LogNivel.Informacao, "Obs");
            var dataDepois = DateTime.Now;

            Assert.InRange(logMensagem.DataHora, dataAntes, dataDepois);
        }
    }
}
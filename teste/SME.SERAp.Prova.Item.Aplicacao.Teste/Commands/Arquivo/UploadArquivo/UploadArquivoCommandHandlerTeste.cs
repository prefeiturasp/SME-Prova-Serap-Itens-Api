using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Exceptions;
using System.Net;
using System.Text;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.Commands.Arquivo
{
    public class UploadArquivoCommandHandlerTeste
    {
        private readonly Mock<IHttpClientFactory> httpClientFactory;
        private readonly Mock<IMediator> mediator;
        private readonly UploadArquivoCommandHandler handler;

        public UploadArquivoCommandHandlerTeste()
        {
            httpClientFactory = new Mock<IHttpClientFactory>();
            mediator = new Mock<IMediator>();
            handler = new UploadArquivoCommandHandler(httpClientFactory.Object, mediator.Object);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_HttpClientFactory_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new UploadArquivoCommandHandler(null, mediator.Object));
            Assert.Equal("httpClientFactory", ex.ParamName);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Mediator_For_Nulo()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new UploadArquivoCommandHandler(httpClientFactory.Object, null));
            Assert.Equal("mediator", ex.ParamName);
        }

        [Fact]
        public async Task Deve_Lancar_NegocioException_Quando_Tipo_Invalido()
        {
            var mockFile = new Mock<IFormFile>();
            var command = new UploadArquivoCommand(mockFile.Object,TipoArquivo.BaseText);

            await Assert.ThrowsAsync<NegocioException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Deve_Fazer_Upload_Com_Sucesso()
        {
            var mockFile = new Mock<IFormFile>();
            var content = Encoding.UTF8.GetBytes("fake content");
            var stream = new MemoryStream(content);

            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.Length).Returns(content.Length);
            mockFile.Setup(f => f.FileName).Returns("teste.pdf");
            mockFile.Setup(f => f.ContentType).Returns("application/pdf");

            var command = new UploadArquivoCommand(mockFile.Object, TipoArquivo.Audio);

            var retornoEsperado = new RetornoUploadArquivoDto
            {
                FileLink = "https://url/arquivo.mp3",
                IdFile = 123,
                Message = "Sucesso",
                Success = true,
                Type = "file"
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(retornoEsperado), Encoding.UTF8, "application/json")
            };

            var handlerMessage = new Mock<HttpMessageHandler>();
            handlerMessage
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(handlerMessage.Object)
            {
                BaseAddress = new Uri("https://fakeapi/")
            };

            httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

            mediator.Setup(m => m.Send(It.IsAny<SalvarArquivoCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(999);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal("https://url/arquivo.mp3", result.FileLink);
            Assert.Equal(999, result.IdFile);

            mediator.Verify(m => m.Send(It.IsAny<SalvarArquivoCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Deve_Lancar_ErroException_Quando_HttpClient_Falhar()
        {
            var mockFile = new Mock<IFormFile>();
            var content = Encoding.UTF8.GetBytes("fake content");
            var stream = new MemoryStream(content);

            mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
            mockFile.Setup(f => f.Length).Returns(content.Length);
            mockFile.Setup(f => f.FileName).Returns("teste.mp3");
            mockFile.Setup(f => f.ContentType).Returns("audio/mp3");

            var command = new UploadArquivoCommand(mockFile.Object, TipoArquivo.Audio);

            var handlerMessage = new Mock<HttpMessageHandler>();
            handlerMessage
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new ErroException("Falha de rede"));

            var httpClient = new HttpClient(handlerMessage.Object)
            {
                BaseAddress = new Uri("https://fakeapi/")
            };
            httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var ex = await Assert.ThrowsAsync<ErroException>(() => handler.Handle(command, CancellationToken.None));

            Assert.Equal("Falha de rede", ex.Message);
        }
    }
}

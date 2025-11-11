using Moq;
using Xunit;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using SME.SERAp.Prova.Item.Aplicacao;
using MediatR;

namespace SME.SERAp.Prova.Item.Aplicacao.Teste.UseCase
{
    public class UploadArquivoUseCaseTeste
    {
        private readonly Mock<IMediator> mediatorMock;
        private readonly UploadArquivoUseCase useCase;

        public UploadArquivoUseCaseTeste()
        {
            mediatorMock = new Mock<IMediator>();
            useCase = new UploadArquivoUseCase(mediatorMock.Object);
        }

        private Mock<IFormFile> CriarFormFileMock(string nomeArquivo, long tamanho, string contentType = "application/octet-stream")
        {
            var fileMock = new Mock<IFormFile>();
            var content = new byte[tamanho];
            var stream = new MemoryStream(content);

            fileMock.Setup(f => f.FileName).Returns(nomeArquivo);
            fileMock.Setup(f => f.Length).Returns(tamanho);
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.ContentType).Returns(contentType);

            return fileMock;
        }

        private ArquivoDto CriarArquivoDtoMock(string nomeArquivo = "teste.jpg", long tamanho = 1024, string contentType = "image/jpeg")
        {
            var formFileMock = CriarFormFileMock(nomeArquivo, tamanho, contentType);
            return new ArquivoDto { File = formFileMock.Object };
        }

        private RetornoUploadArquivoDto CriarRetornoUploadSucessoMock(long idFile = 123, string fileLink = "https://storage.example.com/files/arquivo.jpg")
        {
            return new RetornoUploadArquivoDto
            {
                Success = true,
                Type = "success",
                Message = "Arquivo enviado com sucesso",
                FileLink = fileLink,
                IdFile = idFile
            };
        }

        private RetornoUploadArquivoDto CriarRetornoUploadErroMock(string mensagemErro = "Erro ao enviar arquivo")
        {
            return new RetornoUploadArquivoDto
            {
                Success = false,
                Type = "error",
                Message = mensagemErro,
                FileLink = string.Empty,
                IdFile = 0
            };
        }

        #region Testes de Construtor

        [Fact]
        public void Construtor_Deve_Lancar_ArgumentNullException_Quando_Mediator_For_Nulo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new UploadArquivoUseCase(null));
        }

        [Fact]
        public void Construtor_Deve_Inicializar_Corretamente_Com_Mediator_Valido()
        {
            var mediator = new Mock<IMediator>();
            var useCaseInstance = new UploadArquivoUseCase(mediator.Object);

            Assert.NotNull(useCaseInstance);
        }

        #endregion

        #region Testes de Upload - Tipos Comuns

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Video()
        {
            var arquivoDto = CriarArquivoDtoMock("video.mp4", 10240, "video/mp4");
            var tipoArquivo = TipoArquivo.Video;
            var retornoEsperado = CriarRetornoUploadSucessoMock(100);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal("success", resultado.Type);
            Assert.Equal("Arquivo enviado com sucesso", resultado.Message);
            Assert.NotEmpty(resultado.FileLink);
            Assert.Equal(100, resultado.IdFile);

            mediatorMock.Verify(m => m.Send(
                It.Is<UploadArquivoCommand>(cmd =>
                    cmd.Arquivo == arquivoDto.File &&
                    cmd.Tipo == tipoArquivo),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Audio()
        {
            var arquivoDto = CriarArquivoDtoMock("audio.mp3", 5120, "audio/mp3");
            var tipoArquivo = TipoArquivo.Audio;
            var retornoEsperado = CriarRetornoUploadSucessoMock(200);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(200, resultado.IdFile);

            mediatorMock.Verify(m => m.Send(
                It.Is<UploadArquivoCommand>(cmd => cmd.Tipo == TipoArquivo.Audio),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Thumbnail_Video()
        {
            var arquivoDto = CriarArquivoDtoMock("thumbnail.jpg", 2048, "image/jpeg");
            var tipoArquivo = TipoArquivo.ThumbnailVideo;
            var retornoEsperado = CriarRetornoUploadSucessoMock(300);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(300, resultado.IdFile);

            mediatorMock.Verify(m => m.Send(
                It.Is<UploadArquivoCommand>(cmd => cmd.Tipo == TipoArquivo.ThumbnailVideo),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        #endregion

        #region Testes de Upload - Tipos de Item

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Texto_Base()
        {
            var arquivoDto = CriarArquivoDtoMock("texto_base.html", 1024, "text/html");
            var tipoArquivo = TipoArquivo.BaseText;
            var retornoEsperado = CriarRetornoUploadSucessoMock(400);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(400, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Alternativa()
        {
            var arquivoDto = CriarArquivoDtoMock("alternativa.jpg", 2048, "image/jpeg");
            var tipoArquivo = TipoArquivo.Alternative;
            var retornoEsperado = CriarRetornoUploadSucessoMock(500);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(500, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Enunciado()
        {
            var arquivoDto = CriarArquivoDtoMock("enunciado.html", 1536, "text/html");
            var tipoArquivo = TipoArquivo.Statement;
            var retornoEsperado = CriarRetornoUploadSucessoMock(600);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(600, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Analise_Item()
        {
            var arquivoDto = CriarArquivoDtoMock("analise_item.pdf", 4096, "application/pdf");
            var tipoArquivo = TipoArquivo.AnalysisItem;
            var retornoEsperado = CriarRetornoUploadSucessoMock(700);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(700, resultado.IdFile);
        }

        #endregion

        #region Testes de Upload - Tipos de Prova e Folha de Resposta

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Modelo_Prova()
        {
            var arquivoDto = CriarArquivoDtoMock("modelo_prova.pdf", 8192, "application/pdf");
            var tipoArquivo = TipoArquivo.ModelTestHeader;
            var retornoEsperado = CriarRetornoUploadSucessoMock(800);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(800, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Folha_Resposta()
        {
            var arquivoDto = CriarArquivoDtoMock("folha_resposta.pdf", 6144, "application/pdf");
            var tipoArquivo = TipoArquivo.AnswerSheetStudentNumber;
            var retornoEsperado = CriarRetornoUploadSucessoMock(900);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(900, resultado.IdFile);
        }

        #endregion

        #region Testes de Upload - Ícones

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Icone_Link_Externo()
        {
            var arquivoDto = CriarArquivoDtoMock("icone_link.svg", 512, "image/svg+xml");
            var tipoArquivo = TipoArquivo.IconeLinkExterno;
            var retornoEsperado = CriarRetornoUploadSucessoMock(1000);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(1000, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Sucesso_Ao_Fazer_Upload_De_Icone_Ferramenta_Destaque()
        {
            var arquivoDto = CriarArquivoDtoMock("icone_destaque.png", 768, "image/png");
            var tipoArquivo = TipoArquivo.IconeFerramentaDestaque;
            var retornoEsperado = CriarRetornoUploadSucessoMock(1100);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(1100, resultado.IdFile);
        }

        #endregion

        #region Testes de Verificação de Command

        [Fact]
        public async Task Deve_Enviar_Command_Com_Parametros_Corretos()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.pdf", 4096, "application/pdf");
            var tipoArquivo = TipoArquivo.File;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            UploadArquivoCommand commandCapturado = null;

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .Callback<IRequest<RetornoUploadArquivoDto>, CancellationToken>((cmd, ct) =>
                        {
                            commandCapturado = cmd as UploadArquivoCommand;
                        })
                        .ReturnsAsync(retornoEsperado);

            await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(commandCapturado);
            Assert.Equal(arquivoDto.File, commandCapturado.Arquivo);
            Assert.Equal(tipoArquivo, commandCapturado.Tipo);
        }

        [Fact]
        public async Task Deve_Passar_IFormFile_Do_ArquivoDto_Para_Command()
        {
            var formFileMock = CriarFormFileMock("teste.jpg", 2048, "image/jpeg");
            var arquivoDto = new ArquivoDto { File = formFileMock.Object };
            var tipoArquivo = TipoArquivo.BaseText;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            mediatorMock.Verify(m => m.Send(
                It.Is<UploadArquivoCommand>(cmd => cmd.Arquivo == formFileMock.Object),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        #endregion

        #region Testes de Upload com Falha

        [Fact]
        public async Task Deve_Retornar_Falha_Quando_Upload_Nao_For_Bem_Sucedido()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;
            var retornoErro = CriarRetornoUploadErroMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoErro);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal("error", resultado.Type);
            Assert.Equal("Erro ao enviar arquivo", resultado.Message);
            Assert.Empty(resultado.FileLink);
            Assert.Equal(0, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_Falha_Com_Mensagem_Customizada()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.pdf", 2048);
            var tipoArquivo = TipoArquivo.Test;
            var mensagemErro = "Formato de arquivo não suportado";
            var retornoErro = CriarRetornoUploadErroMock(mensagemErro);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoErro);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal(mensagemErro, resultado.Message);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_Quando_Mediator_Lancar_Excecao()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Erro ao processar comando"));

            await Assert.ThrowsAsync<Exception>(() =>
                useCase.ExecutarAsync(arquivoDto, tipoArquivo));

            mediatorMock.Verify(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Propagar_Excecao_De_IO_Quando_Erro_Ao_Acessar_Arquivo()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo_corrompido.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new IOException("Erro ao ler arquivo"));

            await Assert.ThrowsAsync<IOException>(() =>
                useCase.ExecutarAsync(arquivoDto, tipoArquivo));
        }

        #endregion

        #region Testes com Diferentes Tamanhos de Arquivo

        [Theory]
        [InlineData(512)]
        [InlineData(1024)]
        [InlineData(102400)]
        [InlineData(1048576)]
        [InlineData(5242880)]
        public async Task Deve_Processar_Arquivos_De_Diferentes_Tamanhos(long tamanhoArquivo)
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.jpg", tamanhoArquivo);
            var tipoArquivo = TipoArquivo.File;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);

            mediatorMock.Verify(m => m.Send(
                It.Is<UploadArquivoCommand>(cmd => cmd.Arquivo.Length == tamanhoArquivo),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        #endregion

        #region Testes de Retorno de Dados

        [Fact]
        public async Task Deve_Retornar_FileLink_Valido_Quando_Upload_For_Sucesso()
        {
            var arquivoDto = CriarArquivoDtoMock("imagem.png", 2048, "image/png");
            var tipoArquivo = TipoArquivo.File;
            var fileLinkEsperado = "https://cdn.example.com/uploads/imagem.png";
            var retornoEsperado = CriarRetornoUploadSucessoMock(456, fileLinkEsperado);

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal(fileLinkEsperado, resultado.FileLink);
            Assert.Equal(456, resultado.IdFile);
        }

        [Fact]
        public async Task Deve_Retornar_IdFile_Zero_E_FileLink_Vazio_Quando_Upload_Falhar()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;
            var retornoErro = CriarRetornoUploadErroMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoErro);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.False(resultado.Success);
            Assert.Equal(0, resultado.IdFile);
            Assert.Empty(resultado.FileLink);
        }

        [Fact]
        public async Task Deve_Retornar_Todas_Propriedades_Preenchidas_Em_Caso_De_Sucesso()
        {
            var arquivoDto = CriarArquivoDtoMock("documento.pdf", 4096);
            var tipoArquivo = TipoArquivo.Test;
            var retornoEsperado = new RetornoUploadArquivoDto
            {
                Success = true,
                Type = "success",
                Message = "Upload concluído com êxito",
                FileLink = "https://storage.com/documento.pdf",
                IdFile = 789
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            var resultado = await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            Assert.NotNull(resultado);
            Assert.True(resultado.Success);
            Assert.Equal("success", resultado.Type);
            Assert.Equal("Upload concluído com êxito", resultado.Message);
            Assert.Equal("https://storage.com/documento.pdf", resultado.FileLink);
            Assert.Equal(789, resultado.IdFile);
        }

        #endregion

        #region Testes de Invocação do Mediator

        [Fact]
        public async Task Deve_Invocar_Mediator_Exatamente_Uma_Vez()
        {
            var arquivoDto = CriarArquivoDtoMock("teste.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            mediatorMock.Verify(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Chamar_Mediator_Com_CancellationToken()
        {
            var arquivoDto = CriarArquivoDtoMock("arquivo.jpg", 1024);
            var tipoArquivo = TipoArquivo.File;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            mediatorMock.Verify(m => m.Send(
                It.IsAny<UploadArquivoCommand>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task Nao_Deve_Invocar_Mediator_Multiplas_Vezes_Para_Mesmo_Upload()
        {
            var arquivoDto = CriarArquivoDtoMock("teste.pdf", 2048);
            var tipoArquivo = TipoArquivo.Test;
            var retornoEsperado = CriarRetornoUploadSucessoMock();

            mediatorMock.Setup(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(retornoEsperado);

            await useCase.ExecutarAsync(arquivoDto, tipoArquivo);

            mediatorMock.Verify(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()),
                Times.Once);

            mediatorMock.Verify(m => m.Send(It.IsAny<UploadArquivoCommand>(), It.IsAny<CancellationToken>()),
                Times.Exactly(1));
        }

        #endregion
    }
}
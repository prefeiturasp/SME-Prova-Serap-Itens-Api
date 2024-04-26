using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using SME.SERAp.Prova.Item.Dominio.Constantes;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Exceptions;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommandHandler : IRequestHandler<UploadArquivoCommand, RetornoUploadArquivoDto>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UploadArquivoCommandHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<RetornoUploadArquivoDto> Handle(UploadArquivoCommand request, CancellationToken cancellationToken)
        {
            if (request.Tipo != TipoArquivo.Audio && request.Tipo != TipoArquivo.File && request.Tipo != TipoArquivo.Video)
                throw new NegocioException("Formato do arquivo não aceito.");

            string urlUpload;
            switch (request.Tipo)
            {
                case TipoArquivo.File:
                    urlUpload = "Item/Arquivos/Upload";
                    break;        
                case TipoArquivo.Video:
                    urlUpload = "Item/Arquivos/UploadVideo";
                    break;
                case TipoArquivo.Audio:
                    urlUpload = "Item/Arquivos/UploadAudio";
                    break;
                case TipoArquivo.BaseText:
                case TipoArquivo.Alternative:
                case TipoArquivo.Justificative:
                case TipoArquivo.Statement:
                case TipoArquivo.Test:
                case TipoArquivo.ModelTestHeader:
                case TipoArquivo.AnswerSheetStudentNumber:
                case TipoArquivo.AnswerSheetBatch:
                case TipoArquivo.ModelTestFooter:
                case TipoArquivo.ModelTestCover:
                case TipoArquivo.AnswerSheetLot:
                case TipoArquivo.AnswerSheetQrCode:
                case TipoArquivo.AnalysisItem:
                case TipoArquivo.AnswerSheetBatchLog:
                case TipoArquivo.TestFeedback:
                case TipoArquivo.ExportCorrectionResult:
                case TipoArquivo.ExportAnswerSheetResult:
                case TipoArquivo.ExportFollowUpIdentification:
                case TipoArquivo.ZipFollowUpIdentification:
                case TipoArquivo.ExportReportCorrection:
                case TipoArquivo.ExportReportTestPerformance:
                case TipoArquivo.ExportReportItemPerformance:
                case TipoArquivo.ExportReportPercentageItemChoice:
                case TipoArquivo.ExportReportStudentPerformance:
                case TipoArquivo.IconeLinkExterno:
                case TipoArquivo.IconeFerramentaDestaque:
                case TipoArquivo.IconeFerramentas:
                case TipoArquivo.ThumbnailVideo:
                case TipoArquivo.ResultadosPsp:
                default:
                    throw new NegocioException("Formato do arquivo não aceito.");
            }

            byte[] bytes;
            var stream = request.Arquivo.OpenReadStream();
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream, cancellationToken);
                bytes = memoryStream.ToArray();
            }

            var uploadArquivo = new UploadArquivoDto
            {
                ContentLength = unchecked((int)request.Arquivo.Length),
                ContentType = request.Arquivo.ContentType,
                FileName = request.Arquivo.FileName,
                InputStream = Convert.ToBase64String(bytes),
                FileType = request.Tipo
            };
            
            var httpClientApiSerap = httpClientFactory.CreateClient(SerapConstants.ApiSerap);
            var parametros = JsonConvert.SerializeObject(uploadArquivo);

            var resposta = await httpClientApiSerap.PostAsync(urlUpload,
                new StringContent(parametros, Encoding.UTF8, "application/json"), cancellationToken);
            try
            {
                resposta.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<RetornoUploadArquivoDto>(
                    await resposta.Content.ReadAsStringAsync(cancellationToken)) ?? new RetornoUploadArquivoDto
                {
                    FileLink = string.Empty,
                    IdFile = 0,
                    Message = "Falha ao realizar o upload do arquivo.",
                    Success = false,
                    Type = string.Empty
                };
            }
            catch (Exception e)
            {
                throw new ErroException($"Erro ao realizar o upload do arquivo: {e.Message}");
            }
        }
    }
}
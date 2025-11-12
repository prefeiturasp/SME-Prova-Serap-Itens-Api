using Elastic.Apm.Api;
using MediatR;
using Newtonsoft.Json;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Constantes;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

using SME.SERAp.Prova.Item.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases.Arquivo
{
    public class UploadArquivoAudioVideo : IUploadArquivoAudioVideo
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IMediator mediator;

        public UploadArquivoAudioVideo(IHttpClientFactory httpClientFactory, IMediator mediator)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        public async Task<RetornoUploadArquivoDto> ExecutarAsync(UploadArquivoDto arquivoDto)
        {
            if (arquivoDto.FileType != TipoArquivo.Audio && arquivoDto.FileType != TipoArquivo.File && arquivoDto.FileType != TipoArquivo.Video)
                throw new NegocioException("Formato do arquivo não aceito.");

            string urlUpload;
            switch (arquivoDto.FileType)
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




            var httpClientApiSerap = httpClientFactory.CreateClient(SerapConstants.ApiSerap);
            var parametros = JsonConvert.SerializeObject(arquivoDto);

            var resposta = await httpClientApiSerap.PostAsync(urlUpload,
                new StringContent(parametros, Encoding.UTF8, "application/json"));
            try
            {
                resposta.EnsureSuccessStatusCode();

                if (!resposta.IsSuccessStatusCode)
                {
                    return new RetornoUploadArquivoDto
                    {
                        FileLink = string.Empty,
                        IdFile = 0,
                        Message = "Falha ao realizar o upload do arquivo.",
                        Success = false,
                        Type = string.Empty
                    };
                }

                var retornoUploadArquivo = JsonConvert.DeserializeObject<RetornoUploadArquivoDto>(
                    await resposta.Content.ReadAsStringAsync());

                retornoUploadArquivo.IdFile = await mediator.Send(new SalvarArquivoCommand(new Dominio.Entities.Arquivo(
                    retornoUploadArquivo.IdFile, arquivoDto.FileName, retornoUploadArquivo.FileLink,
                    arquivoDto.ContentType, 1, DateTime.Now)));
                retornoUploadArquivo.Type = arquivoDto.ContentType;
                return retornoUploadArquivo;
            }
            catch (Exception e)
            {
                throw new ErroException($"Erro ao realizar o upload do arquivo: {e.Message}");
            }
        }
    }
}


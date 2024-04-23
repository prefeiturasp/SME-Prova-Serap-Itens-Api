using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Aplicacao.Commands;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class UploadArquivoUseCase : AbstractUseCase, IUploadArquivoUseCase
    {
        public UploadArquivoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<RetornoUploadArquivoDto> ExecutarAsync(FormFile formFile, TipoArquivo tipoArquivo)
        {
            var arquivoUpload = new UploadArquivoDto();

            arquivoUpload.InputStream = formFile.OpenReadStream().ToString();
            arquivoUpload.ContentLength = Convert.ToInt32(formFile.Length);
            arquivoUpload.ContentType = formFile.ContentType;
            arquivoUpload.FileName = formFile.FileName;
            arquivoUpload.FileType = tipoArquivo;


            var updloadArquivoRet = await mediator.Send(new UploadArquivoCommand(arquivoUpload));

            var arquivo = new Arquivo(updloadArquivoRet.IdFile, arquivoUpload.FileName, updloadArquivoRet.FileLink, arquivoUpload.ContentType, 1, DateTime.Now);
            await mediator.Send(new SalvarArquivoCommand(arquivo));
            return updloadArquivoRet;
        }
    }
}
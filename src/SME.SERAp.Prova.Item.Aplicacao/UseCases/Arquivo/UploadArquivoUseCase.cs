using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class UploadArquivoUseCase : AbstractUseCase, IUploadArquivoUseCase
    {
        public UploadArquivoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<RetornoUploadArquivoDto> ExecutarAsync(IFormFile formFile, TipoArquivo tipoArquivo)
        {
            return await mediator.Send(new UploadArquivoCommand(formFile, tipoArquivo));
        }
    }
}
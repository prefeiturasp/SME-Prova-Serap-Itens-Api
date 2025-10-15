using MediatR;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class UploadArquivoUseCase : AbstractUseCase, IUploadArquivoUseCase
    {
        public UploadArquivoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<RetornoUploadArquivoDto> ExecutarAsync(ArquivoDto arquivoDto, TipoArquivo tipoArquivo)
        {
            return await mediator.Send(new UploadArquivoCommand(arquivoDto.File, tipoArquivo));
        }
    }
}
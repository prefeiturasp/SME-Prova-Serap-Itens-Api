using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class UploadArquivoUseCase : AbstractUseCase, IUploadArquivoUseCase
    {
        public UploadArquivoUseCase(IMediator mediator) : base(mediator)
        {
        }
        
        public async Task<RetornoUploadArquivoDto> ExecutarAsync(UploadArquivoDto uploadArquivo)
        {
            return await mediator.Send(new UploadArquivoCommand(uploadArquivo));            
        }
    }
}
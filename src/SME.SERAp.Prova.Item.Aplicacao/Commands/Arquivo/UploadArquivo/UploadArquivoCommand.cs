using MediatR;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommand : IRequest<RetornoUploadArquivoDto>
    {
        public UploadArquivoCommand(UploadArquivoDto uploadArquivo)
        {
            UploadArquivo = uploadArquivo;
        }

        public UploadArquivoDto UploadArquivo { get; }
    }
}
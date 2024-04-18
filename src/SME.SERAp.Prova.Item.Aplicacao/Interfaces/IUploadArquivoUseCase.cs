using System.Threading.Tasks;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IUploadArquivoUseCase
    {
        Task<RetornoUploadArquivoDto> ExecutarAsync(UploadArquivoDto uploadArquivo);
    }
}
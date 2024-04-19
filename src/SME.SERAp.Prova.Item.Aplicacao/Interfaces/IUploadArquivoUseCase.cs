using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IUploadArquivoUseCase
    {
        Task<RetornoUploadArquivoDto> ExecutarAsync(FormFile uploadArquivo, TipoArquivo tipoArquivo);
    }
}
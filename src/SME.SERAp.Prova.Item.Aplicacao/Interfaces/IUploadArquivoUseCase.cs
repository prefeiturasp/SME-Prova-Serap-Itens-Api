using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Interfaces
{
    public interface IUploadArquivoUseCase
    {
        Task<RetornoUploadArquivoDto> ExecutarAsync(ArquivoDto arquivoDto, TipoArquivo tipoArquivo);
    }
}
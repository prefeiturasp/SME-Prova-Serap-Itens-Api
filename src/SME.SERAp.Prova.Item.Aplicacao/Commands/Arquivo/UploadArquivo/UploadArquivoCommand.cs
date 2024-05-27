using MediatR;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommand : IRequest<RetornoUploadArquivoDto>
    {
        public UploadArquivoCommand(IFormFile arquivo, TipoArquivo tipo)
        {
            Arquivo = arquivo;
            Tipo = tipo;
        }

        public IFormFile Arquivo { get; }
        public TipoArquivo Tipo { get; }
    }
}
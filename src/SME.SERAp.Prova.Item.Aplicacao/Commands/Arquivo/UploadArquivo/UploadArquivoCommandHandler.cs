using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using SME.SERAp.Prova.Item.Dominio.Constantes;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Exceptions;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class UploadArquivoCommandHandler : IRequestHandler<UploadArquivoCommand, RetornoUploadArquivoDto>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UploadArquivoCommandHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<RetornoUploadArquivoDto> Handle(UploadArquivoCommand request, CancellationToken cancellationToken)
        {
            var httpClientApiSerap = httpClientFactory.CreateClient(SerapConstants.ApiSerap);
            var parametros = JsonConvert.SerializeObject(request.UploadArquivo);

            var resposta = await httpClientApiSerap.PostAsync("api/Item/Arquivos/Upload",
                new StringContent(parametros, Encoding.UTF8, "application/json"), cancellationToken);

            try
            {
                resposta.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<RetornoUploadArquivoDto>(
                    await resposta.Content.ReadAsStringAsync(cancellationToken)) ?? new RetornoUploadArquivoDto
                {
                    FileLink = string.Empty,
                    IdFile = 0,
                    Message = "Falha ao realizar o upload do arquivo.",
                    Success = false,
                    Type = string.Empty
                };
            }
            catch (Exception e)
            {
                throw new ErroException($"Erro ao realizar o upload do arquivo: {e.Message}");
            }
        }
    }
}
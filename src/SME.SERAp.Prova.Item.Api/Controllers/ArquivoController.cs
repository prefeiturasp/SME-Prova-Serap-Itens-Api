using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Api.Filters;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases.Arquivo;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Arquivo;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Arquivo")]
    //[Authorize]
    public class ArquivoController : ControllerBase
    {
        [ValidaDto]
        [ProducesResponseType(typeof(RetornoUploadArquivoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RetornoBaseDto), StatusCodes.Status500InternalServerError)]
        [HttpPost("upload/{tipoArquivo}", Name = nameof(UploadAsync))]
        public async Task<IActionResult> UploadAsync([FromRoute] TipoArquivo tipoArquivo,
            [FromForm] ArquivoDto arquivoDto, [FromServices] IUploadArquivoUseCase useCase)
        {
            return Ok(await useCase.ExecutarAsync(arquivoDto, tipoArquivo));
        }



        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ArquivosItemDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterItemComAlternativasPorId(long itemId, [FromServices] IObterAudioVideoPorItemIdUseCase obterAudioVideoPorItemIdUseCase)
        {
            return Ok(await obterAudioVideoPorItemIdUseCase.ExecutarAsync(itemId));
        }

        [ValidaDto]
        [ProducesResponseType(typeof(RetornoUploadArquivoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RetornoBaseDto), StatusCodes.Status500InternalServerError)]
        [HttpPost("Upload/AudioVideo", Name = nameof(UploadAudioVideoAsync))]
        public async Task<IActionResult> UploadAudioVideoAsync([FromBody] UploadArquivoDto uploadArquivoDto,
          IUploadArquivoAudioVideo useCase)
        {
            return Ok(await useCase.ExecutarAsync(uploadArquivoDto));
        }
    }
}
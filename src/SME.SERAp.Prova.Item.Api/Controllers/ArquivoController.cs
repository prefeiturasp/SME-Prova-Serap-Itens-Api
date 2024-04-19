using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Api.Filters;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Arquivo")]
    public class ArquivoController : ControllerBase
    {
        [ValidaDto]
        [ProducesResponseType(typeof(RetornoUploadArquivoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(RetornoBaseDto), StatusCodes.Status500InternalServerError)]
        [HttpPost("upload/{tipoArquivo}", Name = nameof(UploadAsync))]
        public async Task<IActionResult> UploadAsync([FromQuery] TipoArquivo tipoArquivo, [FromBody] FormFile request,
            [FromServices] IUploadArquivoUseCase useCase)
        {
            var ret = new RetornoUploadArquivoDto();
            if (TipoArquivo.Audio == tipoArquivo)
            {
             
                ret.FileLink = "https://serap.sme.prefeitura.sp.gov.br/Files/Audio/2023/11/977c57c6-56ca-4cde-8192-b2d006bed6f6.mp3";
                ret.IdFile = 2;
                ret.Success = true;
              
            }

            else if (TipoArquivo.Video == tipoArquivo)
            {
                  
                    ret.FileLink = "https://serap.sme.prefeitura.sp.gov.br/Files/Audio/2023/11/977c57c6-56ca-4cde-8192-b2d006bed6f6.mp3";
                    ret.IdFile = 2;
                    ret.Success = true;

            }

            else
            {
                return BadRequest("TipoDoArquivo não entontrado: " + tipoArquivo);
            }
            return Ok(ret);


        }
    }
}
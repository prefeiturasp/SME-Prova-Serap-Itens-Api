using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [Route("api/v1/Assuntos")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {

        [HttpGet("{disciplinaId}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAssuntos([Required] long disciplinaId, [FromServices] IObterAssuntosUseCase obterAssuntosUseCase)
        {
            return Ok(await obterAssuntosUseCase.Executar(disciplinaId));
        }


        [HttpGet("SubAssuntos/{assuntoId}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterSubAssuntos([Required] long assuntoId, [FromServices] IObterSubAssuntosPorAssuntoIdUseCase obterSubAssuntoPorAssuntoIdUseCase)
        {
            return Ok(await obterSubAssuntoPorAssuntoIdUseCase.Executar(assuntoId));
        }
    }
}


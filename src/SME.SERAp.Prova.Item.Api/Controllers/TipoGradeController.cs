using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/TipoGrade")]    
    public class TipoGradeController : ControllerBase
    {
        [HttpGet("Matriz/{idMatriz}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTiposGradePorMatrizId([Required] long idMatriz, 
            [FromServices] IObterTiposGradePorMatrizIdUseCase useCase)
        {
            return Ok(await useCase.Executar(idMatriz));
        }
    }
}
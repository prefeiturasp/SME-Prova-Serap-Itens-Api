using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Competencia;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Competencia")]
    [Authorize]
    public class CompetenciaController : ControllerBase
    {
        [HttpGet("Matriz/{idMatriz}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterCompetenciasPorMatrizId([Required] long idMatriz, 
            [FromServices] IObterCompetenciasPorMatrizIdUseCase useCase)
        {
            return Ok(await useCase.Executar(idMatriz));
        }        
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Habilidade")]    
    public class HabilidadeController : ControllerBase
    {
        [HttpGet("Competencia/{idCompetencia}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterHabilidadesPorCompetenciaId([Required] long idCompetencia, 
            [FromServices] IObterHabilidadesPorCompetenciaIdUseCase useCase)
        {
            return Ok(await useCase.Executar(idCompetencia));
        }
    }
}
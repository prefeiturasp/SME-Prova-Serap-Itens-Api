using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Disciplina")]
    public class DisciplinaController : ControllerBase
    {
        [HttpGet("AreaConhecimento/{idAreaConhecimento}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterDisciplinasPorAreaConhecimento(long idAreaConhecimento, [FromServices] IObterDisciplinasPorAreaConhecimento obterDisciplinas)
        {
            return Ok(await obterDisciplinas.Executar(idAreaConhecimento));
        }
    }
}

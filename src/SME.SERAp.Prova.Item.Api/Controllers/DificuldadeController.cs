using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Dificuldade")]
    public class DificuldadeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterDificuldades([FromServices] IObterDificuldadesUseCase obterDificuldadesUseCase)
        {
            return Ok(await obterDificuldadesUseCase.Executar());
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/QuantidadeAlternativa")]
    public class QuantidadeAlternativaController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterQuantidadesAlternativas([FromServices] IObterQuantidadesAlternativasUseCase obterQuantidadesAlternativas)
        {
            return Ok(await obterQuantidadesAlternativas.Executar());
        }
    }
}

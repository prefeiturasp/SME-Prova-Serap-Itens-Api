using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{

    [ApiController]
    [Route("/api/v1/AreaConhecimento")]
    public class AreaConhecimentoController : ControllerBase
    {

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<Teste>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterAreasConhecimento([FromServices] IObterAreasConhecimentoUseCase obterAreasConhecimento)
        {
            return Ok(await obterAreasConhecimento.Executar());
        }
    }
}


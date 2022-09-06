using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/teste")]
    public class TesteController : ControllerBase
    {
        [HttpPost("{descricao}")]
        [ProducesResponseType(typeof(long), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> InserirTeste(string descricao, [FromServices] IInserirTesteUseCase inserirTesteUseCase)
        {
            return Ok(await inserirTesteUseCase.Executar(descricao));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Teste>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTodos([FromServices] IObterTodosTesteUseCase obterTodosTesteUseCase)
        {
            return Ok(await obterTodosTesteUseCase.Executar());
        }
    }
}

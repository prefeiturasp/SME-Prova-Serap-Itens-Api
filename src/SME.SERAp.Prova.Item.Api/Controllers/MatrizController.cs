using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{

    [ApiController]
    [Route("/api/v1/Matriz")]
    public class MatrizController : ControllerBase
    {

        [HttpGet("Disciplina/{disciplinaId}")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterMatrizesPorDisciplinaId(long disciplinaId, [FromServices] IObterMatrizesPorDisciplinaUseCase obterMatrizesPorDisciplina)
        {
            return Ok(await obterMatrizesPorDisciplina.Executar(disciplinaId));
        }

        [HttpGet("{matrizId}")]
        [ProducesResponseType(typeof(Matriz), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterMatrizPorId(long matrizId, [FromServices] IObterMatrizPorIdUseCase obterMatrizPorIdUseCase)
        {
            return Ok(await obterMatrizPorIdUseCase.Executar(matrizId));
        }
    }
}

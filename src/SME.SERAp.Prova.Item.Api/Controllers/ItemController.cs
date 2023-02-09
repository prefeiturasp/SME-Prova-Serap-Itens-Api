using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Item")]
    public class ItemController : ControllerBase
    {
        [HttpPost("salvar-rascunho")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]

        public async Task<IActionResult> SalvarRascunho(ItemDto itemDto, [FromServices] ISalvarRascunhoItemUseCase inserirRascunhoUseCase)
        {
            return Ok(await inserirRascunhoUseCase.Executar(itemDto));
        }
    }
}

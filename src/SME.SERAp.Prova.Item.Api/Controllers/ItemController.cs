using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Api.Filters;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Item")]
    public class ItemController : ControllerBase
    {
        [HttpPost("salvar-rascunho")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 400)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ValidaDto]
        public async Task<IActionResult> SalvarRascunho(ItemRascunhoDto itemDto, [FromServices] ISalvarRascunhoItemUseCase inserirRascunhoUseCase)
        {
            return Ok(await inserirRascunhoUseCase.Executar(itemDto));
        }

        [HttpPost("salvar")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ValidaDto]
        public async Task<IActionResult> Salvar(ItemDto itemDto, [FromServices] ISalvarItemUseCase salvarItemUseCase)
        {
            return Ok(await salvarItemUseCase.Executar(itemDto));
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(typeof(ItemConsulta), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterItemPorId(long itemId, [FromServices] IObterItemPorIdUseCase obterItemPorIdUseCase)
        {
            return Ok(await obterItemPorIdUseCase.Executar(itemId));
        }


        [HttpGet("Situacoes")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterSituacoesItem([FromServices] IObterSituacoesItemUseCase obterSituacoesItem)
        {
            return Ok(await obterSituacoesItem.Executar());
        }


        [HttpGet("Tipos")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterTiposItem([FromServices] IObterTiposItemUseCase obterTiposItem)
        {
            return Ok(await obterTiposItem.Executar());
        }
    }
}

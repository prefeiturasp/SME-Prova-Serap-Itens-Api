using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Api.Filters;
using SME.SERAp.Prova.Item.Aplicacao;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemConsulta = SME.SERAp.Prova.Item.Dominio.Entities.Item;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/Item")]
    [Authorize]
    public class ItemController : ControllerBase
    {
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

        [HttpGet("{itemId}/Alternativas")]
        [ProducesResponseType(typeof(ItemComAlternativasDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterItemComAlternativasPorId(long itemId, [FromServices] IObterItemComAlternativaPorIdUseCase obterItemComAlternativaPorIdUseCase)
        {
            return Ok(await obterItemComAlternativaPorIdUseCase.Executar(itemId));
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

        [HttpGet("NivelItem")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterNivelItem([FromServices] IObterNivelItemUseCase obternivelitem)
        {
            return Ok(await obternivelitem.Executar());
        }

        [HttpGet("Codigos")]
        [ProducesResponseType(typeof(IEnumerable<SelectDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]

        public async Task<IActionResult> ObterListaCodigoItens([FromQuery] string codigoItem, [FromServices] IObterCodigosItensUseCase obterListaCodigoItensUseCase)
        {
            return Ok(await obterListaCodigoItensUseCase.Executar(codigoItem));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginacaoDto<ItemListaDto>), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]

        public async Task<IActionResult> ObterListaItens([FromQuery] FiltroItemsDto filtroDto, [FromServices] IObterListaItemsUseCase obterListaItemsUseCase)
        {
            return Ok(await obterListaItemsUseCase.Executar(filtroDto));
        }

        [HttpGet("resumo/{itemId}")]
        [ProducesResponseType(typeof(ItemResumoDto), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 404)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> ObterItemResumoPorId(long itemId, [FromServices] IObterItemResumoPorIdUseCase obterItemResumoPorIdUseCase)
        {
            var resumo = await obterItemResumoPorIdUseCase.Executar(itemId);

            if (resumo == null)
            {
                return NotFound(new RetornoBaseDto("Item não encontrado ou sem a última versão disponível."));
            }

            return Ok(resumo);
        }

        [HttpPatch("situacao")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(RetornoBaseDto), 400)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        public async Task<IActionResult> AtualizarSituacao(
            [FromBody] AtualizarSituacaoItemDto dto,
            [FromServices] IAtualizarSituacaoItemUseCase atualizarSituacaoItemUseCase)
        {
            var resultado = await atualizarSituacaoItemUseCase.Executar(
                dto.CodigoItem,
                dto.VersaoItem,
                dto.Situacao);

            if (!resultado)
                return BadRequest(new RetornoBaseDto("Não foi possível atualizar a situação do item."));

            return Ok(resultado);
        }
    }
}
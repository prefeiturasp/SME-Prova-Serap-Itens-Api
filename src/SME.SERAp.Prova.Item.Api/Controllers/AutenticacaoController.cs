﻿using Microsoft.AspNetCore.Mvc;
using SME.SERAp.Prova.Item.Api.Attributes;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Autenticacao;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Api.Controllers
{
    [ApiController]
    [Route("/api/v1/autenticacao")]
    public class AutenticacaoController : Controller
    {
        [HttpPost]
        [ChaveAutenticacaoApi]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoValidarDto), 200)]
        public async Task<IActionResult> Autenticar([FromServices] IAutenticacaoUseCase autenticarUseCase,
             [FromBody] AutenticacaoDto autenticacaoDto)
        {
            return Ok(await autenticarUseCase.Executar(autenticacaoDto));
        }

        [HttpPost("validar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoRetornoDto), 200)]
        public async Task<IActionResult> Validar([FromServices] IAutenticacaoValidarUseCase autenticacaoValidarUseCase,
           [FromBody] AutenticacaoValidarDto autenticacaoValidarDto)
        {
            return Ok(await autenticacaoValidarUseCase.Executar(autenticacaoValidarDto));
        }

        [HttpPost("revalidar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(RetornoBaseDto), 500)]
        [ProducesResponseType(typeof(AutenticacaoRetornoDto), 200)]
        public async Task<IActionResult> Revalidar([FromServices] IAutenticacaoRevalidarUseCase autenticacaoRevalidarUseCase,
            [FromBody] AutenticacaoRevalidarDto autenticacaoRevalidarDto)
        {
            return Ok(await autenticacaoRevalidarUseCase.Executar(autenticacaoRevalidarDto));
        }
    }
}

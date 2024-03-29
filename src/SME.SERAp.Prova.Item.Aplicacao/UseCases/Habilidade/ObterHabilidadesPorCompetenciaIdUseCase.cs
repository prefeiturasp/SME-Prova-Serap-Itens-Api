﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Aplicacao.UseCases;
using SME.SERAp.Prova.Item.Infra.Dtos;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterHabilidadesPorCompetenciaIdUseCase : AbstractUseCase, IObterHabilidadesPorCompetenciaIdUseCase
    {
        public ObterHabilidadesPorCompetenciaIdUseCase(IMediator mediator) : base(mediator)
        {
        }
        
        public async Task<IEnumerable<SelectDto>> Executar(long competenciaId)
        {
            var habilidades = await mediator.Send(new ObterHabilidadesPorCompetenciaIdQuery(competenciaId));
            
            return habilidades.Select(c => new SelectDto
            {
                Valor = c.Id,
                Descricao = string.IsNullOrEmpty(c.Codigo) ? c.Descricao : $"{c.Codigo} - {c.Descricao}"
            });            
        }
    }
}
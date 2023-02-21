using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterIdDescricaoOrdemQueryHandler : IRequestHandler<ObterIdDescricaoOrdemQuery, IEnumerable<Dificuldade>>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public ObterIdDescricaoOrdemQueryHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<IEnumerable<Dificuldade>> Handle(ObterIdDescricaoOrdemQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.ObterIdDescricaoOrdemAsync();
        }
    }
}

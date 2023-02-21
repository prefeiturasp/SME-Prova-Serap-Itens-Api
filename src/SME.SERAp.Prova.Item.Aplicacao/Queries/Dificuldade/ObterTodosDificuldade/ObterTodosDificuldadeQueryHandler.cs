using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosDificuldadeQueryHandler : IRequestHandler<ObterTodosDificuldadeQuery, IEnumerable<Dificuldade>>
    {
        private readonly IRepositorioDificuldade repositorioDificuldade;

        public ObterTodosDificuldadeQueryHandler(IRepositorioDificuldade repositorioDificuldade)
        {
            this.repositorioDificuldade = repositorioDificuldade ?? throw new ArgumentNullException(nameof(repositorioDificuldade));
        }

        public async Task<IEnumerable<Dificuldade>> Handle(ObterTodosDificuldadeQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDificuldade.ObterTudoAsync();
        }
    }
}

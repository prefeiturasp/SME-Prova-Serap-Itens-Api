using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosTestesQueryHandler : IRequestHandler<ObterTodosTestesQuery, IEnumerable<Teste>>
    {
        private readonly IRepositorioTeste repositorioTeste;

        public ObterTodosTestesQueryHandler(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste ?? throw new ArgumentNullException(nameof(repositorioTeste));
        }

        public async Task<IEnumerable<Teste>> Handle(ObterTodosTestesQuery request, CancellationToken cancellationToken)
        {
            return await repositorioTeste.ObterTudoAsync();
        }
    }
}

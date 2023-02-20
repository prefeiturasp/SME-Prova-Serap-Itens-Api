using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorIdQueryHandler : IRequestHandler<ObterMatrizPorIdQuery, Matriz>
    {
        private readonly IRepositorioMatriz repositorioMatriz;

        public ObterMatrizPorIdQueryHandler(IRepositorioMatriz repositorioMatriz)
        {
            this.repositorioMatriz = repositorioMatriz ?? throw new ArgumentNullException(nameof(repositorioMatriz));
        }

        public async Task<Matriz> Handle(ObterMatrizPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioMatriz.ObterPorIdAsync(request.MatrizId);
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizesPorDisciplinaIdQueryHandler : IRequestHandler<ObterMatrizesPorDisciplinaIdQuery, IEnumerable<Matriz>>
    {

        private readonly IRepositorioMatriz repositorioMatriz;

        public ObterMatrizesPorDisciplinaIdQueryHandler(IRepositorioMatriz repositorioMatriz)
        {
            this.repositorioMatriz = repositorioMatriz ?? throw new ArgumentNullException(nameof(repositorioMatriz));
        }

        public async Task<IEnumerable<Matriz>> Handle(ObterMatrizesPorDisciplinaIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioMatriz.ObterPorDisciplinaId(request.DisciplinaId);
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterSubAssuntoPorAssuntoIdQueryHandler : IRequestHandler<ObterSubAssuntoPorAssuntoIdQuery, IEnumerable<SubAssunto>>
    {

        private readonly IRepositorioSubAssunto repositorioSubAssunto;
        public ObterSubAssuntoPorAssuntoIdQueryHandler(IRepositorioSubAssunto repositorioSubAssunto)
        {
            this.repositorioSubAssunto = repositorioSubAssunto ?? throw new ArgumentNullException(nameof(repositorioSubAssunto));
        }

        public async Task<IEnumerable<SubAssunto>> Handle(ObterSubAssuntoPorAssuntoIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioSubAssunto.ObterSubAssuntosPorAssuntoId(request.AssuntoId);
        }
    }
}
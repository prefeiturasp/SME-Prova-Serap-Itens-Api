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
    public class ObterAssuntosQueryHandler : IRequestHandler<ObterAssuntosQuery, IEnumerable<Assunto>>
    {

        private readonly IRepositorioAssunto repositorioAssunto;

        public ObterAssuntosQueryHandler(IRepositorioAssunto repositorioAssunto)
        {
            this.repositorioAssunto = repositorioAssunto ?? throw new ArgumentNullException(nameof(repositorioAssunto));
        }

        public async Task<IEnumerable<Assunto>> Handle(ObterAssuntosQuery request, CancellationToken cancellationToken)
        {
            var areasConhecimento = await repositorioAssunto.ObterAssuntos();
            return areasConhecimento.OrderBy(c => c.Descricao);
        }
    }
}
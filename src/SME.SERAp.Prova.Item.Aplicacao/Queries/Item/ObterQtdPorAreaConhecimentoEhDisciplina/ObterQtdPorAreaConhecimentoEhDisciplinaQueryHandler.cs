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
    public class ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandler : IRequestHandler<ObterQtdPorAreaConhecimentoEhDisciplinaQuery, long?>
    {

        private readonly IRepositorioItem RepositorioItem;

        public ObterQtdPorAreaConhecimentoEhDisciplinaQueryHandler(IRepositorioItem RepositorioItem)
        {
            this.RepositorioItem = RepositorioItem ?? throw new ArgumentNullException(nameof(RepositorioItem));
        }

        public async Task<long?> Handle(ObterQtdPorAreaConhecimentoEhDisciplinaQuery request, CancellationToken cancellationToken)
        {
            return await RepositorioItem.ObterQtdItensAreaConhecimentoEhDisciplina(request.AreaConhecimentoId, request.DisciplinaId);
        }
    }
}

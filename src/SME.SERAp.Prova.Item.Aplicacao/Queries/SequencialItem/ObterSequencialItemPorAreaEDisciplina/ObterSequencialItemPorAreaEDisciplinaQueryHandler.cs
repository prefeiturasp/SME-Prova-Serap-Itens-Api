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
    public class ObterSequencialItemPorAreaEDisciplinaQueryHandler : IRequestHandler<ObterSequencialItemPorAreaEDisciplinaQuery, SequencialItem>
    {

        private readonly IRepositoSequencialItem RepositoSequencialItem;

        public ObterSequencialItemPorAreaEDisciplinaQueryHandler(IRepositoSequencialItem RepositoSequencialItem)
        {
            this.RepositoSequencialItem = RepositoSequencialItem ?? throw new ArgumentNullException(nameof(RepositoSequencialItem));
        }

        public async Task<SequencialItem> Handle(ObterSequencialItemPorAreaEDisciplinaQuery request, CancellationToken cancellationToken)
        {
            return await RepositoSequencialItem.ObterSequencialItemPorCodigoAreaEDisciplina(request.AreaConhecimentoCodigo, request.DisciplinaCodigo);
        }
    }
}
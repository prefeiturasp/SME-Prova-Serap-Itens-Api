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
    public class GeraCodigoItemQueryHandler : IRequestHandler<GeraCodigoItemQuery, string>
    {
        private readonly IRepositoSequencialItem RepositoSequencialItem;
        private readonly IMediator mediator;

        public GeraCodigoItemQueryHandler(IRepositoSequencialItem RepositoSequencialItem, IMediator mediator)
        {
            this.RepositoSequencialItem = RepositoSequencialItem ?? throw new ArgumentNullException(nameof(RepositoSequencialItem));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)) ;
        }

        public async Task<string> Handle(GeraCodigoItemQuery request, CancellationToken cancellationToken)
        {
            var sequencialItem = await mediator.Send(new ObterSequencialItemPorAreaEDisciplinaQuery(request.AreaConhecimento.Codigo , request.Disciplina.Codigo));
            
            var ultimoSequencial = sequencialItem == null ? 1 : sequencialItem.Sequencial + 1;
            var codigoItem = await GeraCodigoItem(request.AreaConhecimento.Codigo, request.Disciplina.Codigo, ultimoSequencial);
            
            await mediator.Send(new SalvarSequencialItemCommand(new SequencialItem(sequencialItem?.Id, request.AreaConhecimento.Codigo, request.Disciplina.Codigo, ultimoSequencial, sequencialItem?.CriadoEm)));

            return codigoItem;
        }

        private async Task<string> GeraCodigoItem(long codigoAreaConhecimento, long codigoDisciplina, long ultimoSequencial)
        {
            var codigoItem = $"{codigoAreaConhecimento}{codigoDisciplina}{ultimoSequencial}";
            return codigoItem;
        }
    }
}

using MediatR;
using System;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public abstract class AbstractUseCase
    {
        protected readonly IMediator mediator;

        public AbstractUseCase(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}

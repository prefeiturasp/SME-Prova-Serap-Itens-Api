using MediatR;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.RemoverAlternativasAusentesDto
{
    public class RemoverAlternativasAusentesDtoCommand : IRequest<bool>
    {
        public RemoverAlternativasAusentesDtoCommand(long itemId, IEnumerable<long> idsAlternativasManter)
        {
            ItemId = itemId;
            IdsAlternativasManter = idsAlternativasManter;
        }

        public long ItemId { get; }
        public IEnumerable<long> IdsAlternativasManter { get; }
    }
}
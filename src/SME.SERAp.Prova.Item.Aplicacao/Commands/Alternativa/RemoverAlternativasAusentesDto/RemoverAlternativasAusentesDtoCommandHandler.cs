using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa.RemoverAlternativasAusentesDto
{
    public class RemoverAlternativasAusentesDtoCommandHandler : IRequestHandler<RemoverAlternativasAusentesDtoCommand, bool>
    {
        private readonly IRepositorioAlternativa repositorioAlternativa;

        public RemoverAlternativasAusentesDtoCommandHandler(IRepositorioAlternativa repositorioAlternativa)
        {
            this.repositorioAlternativa = repositorioAlternativa ?? throw new ArgumentNullException(nameof(repositorioAlternativa));
        }

        public async Task<bool> Handle(RemoverAlternativasAusentesDtoCommand request, CancellationToken cancellationToken)
        {
            return await repositorioAlternativa.RemoverAlternativasAusentesAsync(
                request.ItemId,
                request.IdsAlternativasManter);
        }
    }
}
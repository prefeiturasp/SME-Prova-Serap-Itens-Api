using FluentValidation;
using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.Commands.Alternativa
{
    public class SalvarAlternativaCommandHandler : IRequestHandler<SalvarAlternativaCommand, long>
    {
        private readonly IRepositorioAlternativa repositorioAlternativa;

        public SalvarAlternativaCommandHandler(IRepositorioAlternativa repositorioAlternativa)
        {
            this.repositorioAlternativa = repositorioAlternativa ?? throw new ArgumentNullException(nameof(repositorioAlternativa));
        }

        public async Task<long> Handle(SalvarAlternativaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await repositorioAlternativa.SalvarAsync(request.Alternativa);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
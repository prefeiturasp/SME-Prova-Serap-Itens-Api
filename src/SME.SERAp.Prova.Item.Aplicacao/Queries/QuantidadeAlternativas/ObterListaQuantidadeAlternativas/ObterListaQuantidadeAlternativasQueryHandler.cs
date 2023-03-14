using MediatR;
using SME.SERAp.Prova.Item.Dados.Interfaces;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterListaQuantidadeAlternativasQueryHandler : IRequestHandler<ObterListaQuantidadeAlternativasQuery, IEnumerable<SelectDto>>
    {
        private readonly IRepositorioQuantidadeAlternativas repositorioQuantidadeAlternativas;

        public ObterListaQuantidadeAlternativasQueryHandler(IRepositorioQuantidadeAlternativas repositorioQuantidadeAlternativas)
        {
            this.repositorioQuantidadeAlternativas = repositorioQuantidadeAlternativas ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativas));
        }

        public async Task<IEnumerable<SelectDto>> Handle(ObterListaQuantidadeAlternativasQuery request, CancellationToken cancellationToken)
        {
            return await repositorioQuantidadeAlternativas.ObterListaQuantidadeAlternativas();
        }
    }
}

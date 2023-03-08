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
    public class ObterQuantidadeAlternativasQueryHandler : IRequestHandler<ObterQuantidadeAlternativasQuery, IEnumerable<QuantidadeAlternativas>>
    {

        private readonly IRepositorioQuantidadeAlternativas repositorioQuantidadeAlternativas;

        public ObterQuantidadeAlternativasQueryHandler(IRepositorioQuantidadeAlternativas repositorioQuantidadeAlternativas)
        {
            this.repositorioQuantidadeAlternativas = repositorioQuantidadeAlternativas ?? throw new ArgumentNullException(nameof(repositorioQuantidadeAlternativas));
        }

        public async Task<IEnumerable<QuantidadeAlternativas>> Handle(ObterQuantidadeAlternativasQuery request, CancellationToken cancellationToken)
        {
            var quantidadeAlternativas = await repositorioQuantidadeAlternativas.ObterQuantidadeAlternativas();
            return quantidadeAlternativas.OrderBy(c => c.Descricao);
        }
    }
}
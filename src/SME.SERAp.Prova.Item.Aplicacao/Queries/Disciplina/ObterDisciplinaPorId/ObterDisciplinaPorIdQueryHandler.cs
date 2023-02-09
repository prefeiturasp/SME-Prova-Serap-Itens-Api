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
    public class ObterDisciplinaPorIdQueryHandler : IRequestHandler<ObterDisciplinaPorIdQuery, Disciplina>
    {

        private readonly IRepositorioDisciplina repositorioDisciplina;

        public ObterDisciplinaPorIdQueryHandler(IRepositorioDisciplina repositorioDisciplina)
        {
            this.repositorioDisciplina = repositorioDisciplina ?? throw new ArgumentNullException(nameof(repositorioDisciplina));
        }

        public async Task<Disciplina> Handle(ObterDisciplinaPorIdQuery request, CancellationToken cancellationToken)
        {
            return await repositorioDisciplina.ObterPorIdAsync(request.Id);
        }
    }
}

using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterDisciplinaPorIdQuery : IRequest<Disciplina>
    {
        public ObterDisciplinaPorIdQuery(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterTodosDificuldadeQuery : IRequest<IEnumerable<Dificuldade>>
    {
    }
}

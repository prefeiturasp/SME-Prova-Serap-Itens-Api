using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterMatrizPorIdQuery : IRequest<Matriz>
    {
        public ObterMatrizPorIdQuery(long matrizId)
        {
            MatrizId = matrizId;
        }

        public long MatrizId { get; set; }

    }
}

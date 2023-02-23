using MediatR;
using SME.SERAp.Prova.Item.Dominio.Entities;

namespace SME.SERAp.Prova.Item.Aplicacao
{
    public class ObterAreaConhecimentoPorLegadoIdQuery : IRequest<AreaConhecimento>
    {
        public ObterAreaConhecimentoPorLegadoIdQuery(long legaoId)
        {
            LegadoId = legaoId;
        }

        public long LegadoId { get; set; }
    }
}
using MediatR;
using Nest;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class ObterSituacoesItemUseCase : AbstractUseCase, IObterSituacoesItemUseCase
    {

        public ObterSituacoesItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var listaSituacoesItem = new List<SelectDto>();
            var SituacoesValores = Enum.GetNames(typeof(SituacaoItem));

            foreach (var situacao in SituacoesValores)
            {
                var situacaoSelect = new SelectDto();
                situacaoSelect.Valor = (int)Enum.Parse(typeof(TipoItem), situacao);
                situacaoSelect.Descricao = situacao;
                listaSituacoesItem.Add(situacaoSelect);
            }

            return listaSituacoesItem;
        }
    }
}

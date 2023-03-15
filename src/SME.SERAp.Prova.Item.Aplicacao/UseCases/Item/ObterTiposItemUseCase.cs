using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases

{
    public class ObterTiposItemUseCase : AbstractUseCase, IObterTiposItemUseCase
    {

        public ObterTiposItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelectDto>> Executar()
        {
            var listaTiposItem = new List<SelectDto>();
            var tipos = Enum.GetNames(typeof(TipoItem));

            foreach (var tipo in tipos)
            {
                var situacaoSelect = new SelectDto();
                situacaoSelect.Valor = (int)Enum.Parse(typeof(TipoItem), tipo);
                situacaoSelect.Descricao = tipo;
                listaTiposItem.Add(situacaoSelect);
            }

            return listaTiposItem;
        }
    }
}
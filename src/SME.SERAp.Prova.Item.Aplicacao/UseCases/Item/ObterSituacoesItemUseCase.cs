using MediatR;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Enums;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var situacoesParaExibir = Enum.GetValues(typeof(SituacaoItem))
                                          .Cast<SituacaoItem>()
                                          .Where(s => s != SituacaoItem.Inativo);

            foreach (var situacao in situacoesParaExibir)
            {
                listaSituacoesItem.Add(new SelectDto
                {
                    Valor = (int)situacao,
                    Descricao = situacao.ToString()
                });
            }

            return await Task.FromResult(listaSituacoesItem);
        }
    }
}
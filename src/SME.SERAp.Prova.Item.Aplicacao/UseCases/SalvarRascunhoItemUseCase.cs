using Elastic.Apm.Model;
using MediatR;
using Nest;
using SME.SERAp.Prova.Item.Aplicacao.Interfaces;
using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Aplicacao.UseCases
{
    public class SalvarRascunhoItemUseCase : AbstractUseCase, ISalvarRascunhoItemUseCase
    {
        public SalvarRascunhoItemUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<long> Executar(ItemRascunhoDto itemDto)
        {
            try
            {
                var areaConhecimento = await mediator.Send(new ObterAreaConhecimentoPorLegadoIdQuery(itemDto.AreaConhecimentoLegadoId));
                
                if (areaConhecimento == null)
                    throw new Exception($"A area de conhecimento com o id legado: {itemDto.AreaConhecimentoLegadoId} não foi encontrada.");
               
                var disciplina = await mediator.Send(new ObterDisciplinaPorLegadoIdQuery(itemDto.DisciplinaLegadoId));
                
                if (disciplina == null)
                    throw new Exception($"A disciplina com o id legado: {itemDto.DisciplinaLegadoId} não foi encontrada.");
         

                if (itemDto.Id == null || itemDto.Id <= 0)
                    itemDto.CodigoItem = await mediator.Send(new GeraCodigoItemQuery(areaConhecimento, disciplina));  
                //  if (itemDto.CodigoItem == 0 || itemDto.Id == 0)
                //       itemDto.CodigoItem = await TrataSequencialItem(areaConhecimento, disciplina, ultimoSequencial);

                var item = new Dominio.Entities.Item(
                    itemDto?.Id, itemDto.CodigoItem, itemDto.AreaConhecimentoLegadoId, itemDto.MatrizLegadoId, itemDto.DisciplinaLegadoId);
                return await mediator.Send(new SalvarItemCommand(item));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<long> TrataSequencialItem(AreaConhecimento areaConhecimento, Disciplina disciplina, long ultimoSequencial)
        {
            var sequencialItem = await mediator.Send(new ObterSequencialItemPorAreaEDisciplinaQuery(areaConhecimento.Codigo, disciplina.Codigo));
            ultimoSequencial = sequencialItem == null ? 1 : sequencialItem.Sequencial + 1;
            var codigoItem = await GeraCodigoItem(areaConhecimento.Codigo, disciplina.Codigo, ultimoSequencial);
            await mediator.Send(new SalvarSequencialItemCommand(new SequencialItem(sequencialItem?.Id, areaConhecimento.Codigo, disciplina.Codigo, ultimoSequencial, sequencialItem?.CriadoEm)));
            return codigoItem;
        }

        private async Task<long> GeraCodigoItem(long codigoAreaConhecimento, long codigoDisciplina, long ultimoSequencial)
        {
            var codigoItem = $"{codigoAreaConhecimento}{codigoDisciplina}{ultimoSequencial}";
            return long.Parse(codigoItem);
        }
    }
}


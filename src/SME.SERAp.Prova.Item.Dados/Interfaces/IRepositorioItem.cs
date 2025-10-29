using SME.SERAp.Prova.Item.Dominio.Entities;
using SME.SERAp.Prova.Item.Infra.Dtos;
using SME.SERAp.Prova.Item.Infra.Dtos.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioItem : IRepositorioBase<Dominio.Entities.Item>
    {
        Task<long?> ObterMaiorValorId();

        Task<long?> ObterQtdItensAreaConhecimentoEhDisciplina(long areaConhecimentoLegadoId, long disciplinaLegadoId);

        Task<IEnumerable<CodigoItemDto>> ObterListaCodigosItens(string codigoItem);

        Task<PaginacaoDto<ItemListaDto>> ObterListaItensPorFiltro(FiltroItemsDto filtroDto);



    }
}

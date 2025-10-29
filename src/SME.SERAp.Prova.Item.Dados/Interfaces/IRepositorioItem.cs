using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioItem : IRepositorioBase<SME.SERAp.Prova.Item.Dominio.Entities.Item>
    {
        Task<long?> ObterMaiorValorId();

        Task<long?> ObterQtdItensAreaConhecimentoEhDisciplina(long areaConhecimentoLegadoId, long disciplinaLegadoId);

        Task<Dominio.Entities.Item> ObterPorId(long itemId);
        Task<Dominio.Entities.Item> ObterUltimaVersaoItemPorCodigo(string codigoItem);
        Task<IEnumerable<Alternativa>> ObterAlternativasPorItemId(long itemId);
        Task<IEnumerable<SME.SERAp.Prova.Item.Dominio.Entities.Item>> ObterTodasVersoesPorCodigoItem(string codigoItem);
    }
}
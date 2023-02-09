using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioAreaConhecimento : IRepositorioBase<AreaConhecimento>
    {
        Task<long> ObterCodigoAreaConhecimentoPorId(long id);
        Task<AreaConhecimento> ObterAreaConhecimentoPorLegadoId(long legadoId);
    }
}
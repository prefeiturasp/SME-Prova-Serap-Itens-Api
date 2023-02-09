using SME.SERAp.Prova.Item.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Item.Dados.Interfaces
{
    public interface IRepositorioDisciplina : IRepositorioBase<Disciplina>
    {
        Task<IEnumerable<Disciplina>> ObterPorAreaconhecimentoId(long areaconhecimentoId);
        Task<long> ObterCodigoDisciplinaPorId(long id);
        Task<Disciplina> ObterDisciplinaPorLegadoId(long id);
    }
}
